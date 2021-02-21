import CircularProgress from '@material-ui/core/CircularProgress';
import Fade from '@material-ui/core/Fade';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import Popper from '@material-ui/core/Popper';
import React, { Component } from 'react';
import HttpService from '../Services/HttpService';
import './Dashboard.css';
import PhotoCard from './Functional/PhotoCard';
import PhotoPopup from './Functional/PhotoPopup';
import SideDrawer from './Functional/SideDrawer';
import * as constants from '../Constants/Constants';

const DIMMED = { filter: 'brightness(25%)' };
const DIMMED_DEFAULT = { filter: 'brightness(100%)' }
const SPINNER = <CircularProgress className="CustomCircularProgress" />;
let TAKE = 250;
const DEFAULTIMAGERATIO = 1.5;
const ORDERBY = constants.DATETAKEN;
const ORDERBYDESCENDING = true;

class Dashboard extends Component {
  constructor(props) {
    super(props);
    this.httpService = new HttpService();

    this.state = {
      photos: [],
      isLoading: true,
      isPoppedUp: false,
      loadedFullRes: '',
      loadedFIle: ''
    };
  }

  componentDidMount() {
    this.FetchData();
    TAKE = 100;
}

  render() {
    let photoGrid = null;
    const photos = this.state.photos;
    const dimmer = this.state.isLoading ? DIMMED : DIMMED_DEFAULT;

    if (photos != null && photos.some(p => p)) {
      photoGrid = (
        <GridList cellHeight={120} cols={24} style={dimmer}>
          {photos.map((image) => (
            <GridListTile key={image.id} cols={this.GetImageColumnSize(image)}>
              <PhotoCard
                file={image}
                clicked={() => this.PhotoClickHandler(image.id)}
              />
            </GridListTile>
          ))}
        </GridList>
      );
    }
    return (
      <div id="di" onScroll={this.ScrollHandler} className="OverflowingDiv">
        {/* Side Drawer */}
        <SideDrawer searchClick={this.SearchClickHandler} />

        {/* Loading Spinner */}
        {this.state.isLoading ? SPINNER : null}

        {/* Full resolution image popup */}
        <Popper
          id='popper'
          open={this.state.isPoppedUp}
          transition
          style={{ position: 'absolute', bottom: '50%', right: '50%', width: '90vw' }}
          anchorEl={document.getElementById('di')}
          onClick={this.PopperClickHandler}>
          {({ TransitionProps }) => (
            <Fade {...TransitionProps} timeout={350}>
              <PhotoPopup fullResolutionData={this.state.loadedFullRes} file={this.state.loadedFIle} />
            </Fade>
          )}
        </Popper>

        {/* Image Tiles */}
        {photoGrid}

      </div>
    );
  }

  //#region Generic Methods

  GetImageColumnSize(image) {
    const ratio = image.width / image.height;
    if (ratio <= 1) { 
      return 1;
    }
    if (ratio <= DEFAULTIMAGERATIO) {
      return 2;
    }
    return Math.round((ratio / DEFAULTIMAGERATIO) + 1.5);
  }

  //#endregion

  //#region handlers

  PopperClickHandler = () => {
    this.setState({
      loadedFullRes: '',
      isLoading: false,
      isPoppedUp: false,
      loadedFIle: ''
    });
  }

  ScrollHandler = element => {
    if (!this.state.isLoading) {
      const bottom = element.target.scrollHeight - element.target.scrollTop === element.target.clientHeight;
      if (bottom) {
        this.FetchData()
      }
    }
  }

  SearchClickHandler = prop => {
    this.orderByDescending = prop.orderByDescending;
    this.orderByField = prop.filter;
    this.filters = prop.filters;
    this.ResetList();
  }

  async PhotoClickHandler(id) {
    this.setState({
      loadedFullRes: '',
      isLoading: true,
      isPoppedUp: false,
      loadedFIle: ''
    });

    this.httpService
      .GetFullResolutution(id)
      .then(response => {
        if (response) {
          const img = this.state.photos.find(p => p.id === id);
          this.setState({
            loadedFullRes: response.data,
            isLoading: false,
            isPoppedUp: true,
            loadedFIle: img
          });
        }
      });
  }

  //#endregion


  //#region WebMethods

  async ResetList() {
    this.setState({
      photos: [],
      isLoading: true
    });
    this.skip = 0;
    this.FetchFreshData().then((response) => {
      if (response) {
        const freshData = response.data;
        if (freshData) {
          this.setState({
            photos: freshData,
            isLoading: false
          });
        }
      }
    });
  }

  async FetchFreshData() {
    this.setState({
      isLoading: true
    });
    const newFiles = await this.httpService.GetImages(this.skip, TAKE, this.orderByField, this.orderByDescending, this.filters);
    this.skip += TAKE;
    return newFiles;
  }

  async FetchData() {
    this.FetchFreshData().then((response) => {
      if (response) {
        const freshData = response.data;
        if (freshData) {
          const finalArr = [...this.state.photos].concat(freshData);
          this.setState({
            photos: finalArr,
            isLoading: false
          });
        }
      }
    });
  }

  //#endRegion WebMethods

  //#region Vars

  httpService = null;
  skip = 0;
  orderByField = ORDERBY;
  orderByDescending = ORDERBYDESCENDING;
  filters = [];

  //#endRegion Vars
}

export default Dashboard;
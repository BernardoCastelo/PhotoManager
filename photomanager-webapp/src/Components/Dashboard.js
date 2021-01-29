import CircularProgress from '@material-ui/core/CircularProgress';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import React, { Component } from 'react';
import HttpService from '../Services/HttpService';
import PhotoCard from './PhotoComponent/PhotoCard';
import TopDrawer from './TopDrawerComponent/TopDrawerComponent';
import './Dashboard.css';


const TAKE = 250;

const ORDERBY = 'dateTaken';

class Dashboard extends Component {
  constructor(props) {
    super(props);
    this.httpService = new HttpService();

    this.state = {
      photos: [],
      isLoading: false
    };
    this.FetchData();
  }

  handleScroll = (e) => {
    const bottom = e.target.scrollHeight - e.target.scrollTop === e.target.clientHeight;
    console.log(bottom);
    if (bottom) {
      this.FetchData()
    }
  }

  render() {
    let jsx = null;
    let spinner = null;
    const photos = this.state.photos;
    this.dimmer = {filter: 'brightness(100%)'};
    if (this.state.isLoading) {
      this.dimmer = {filter: 'brightness(25%)'};
      spinner = <CircularProgress className="CustomCircularProgress" />;
    }

    if (photos != null && photos.length > 0) {
      jsx = (
        <div id="di" onScroll={this.handleScroll} className="OverflowingDiv">
          <TopDrawer />
          {spinner}
          <GridList cellHeight={120} cols={24} style={this.dimmer}>
            {photos.map((image) => (
              <GridListTile key={image.id} cols={2}>
                <PhotoCard file={image} />;
              </GridListTile>
            ))}
          </GridList>
        </div>
      );
    }
    return jsx;
  }

  async FetchFreshData() {
    this.setState({
      isLoading: true
    });
    const newFiles = await this.httpService.GetImages(this.skip, TAKE, ORDERBY);
    this.skip += TAKE;
    return newFiles;
  }

  async FetchData() {
    this.FetchFreshData().then((response) => {
      const freshData = response.data;
      if (freshData) {
        const finalArr = [...this.state.photos].concat(freshData);
        this.setState({
          photos: finalArr,
          isLoading: false
        });
      }
    });
  }

  httpService = null;
  skip = 0;
  dimmer = {filter: 'brightness(100%)'};
}

export default Dashboard;
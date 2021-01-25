import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import React, { Component } from 'react';
import PhotoCard from './PhotoComponent/PhotoCard';
import TopDrawer from './TopDrawerComponent/TopDrawerComponent';
import HttpService from '../Services/HttpService';

const TAKE = 250;

class Dashboard extends Component {
  constructor(props) {
    super(props);
    this.state = {
      photos: []
    };

    this.httpService = new HttpService();

    this.Getphotos(0, TAKE);
  }

  render() {
    let jsx = null;
    const photos = this.state.photos;
    if (photos != null && photos.length > 0) {
      console.log(photos);
      jsx = (
        <div>
          <TopDrawer />
          <GridList cellHeight={120} cols={24}>
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

  Getphotos(skip, take) {
    this.httpService
      .GetImages(skip, take)
      .then((response) => {
        this.setState({ photos: response.data });
      });
  }

  httpService = null;
}

export default Dashboard;
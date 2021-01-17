import React, { Component } from 'react'
import CardDeck from 'react-bootstrap/CardDeck'
import PhotoCard from './PhotoComponent/PhotoCard';
import TopDrawer from './TopDrawerComponent/TopDrawerComponent'
import img from '../LinkToPhotos/02.EDITS/2.jpg'

const N = 3;

const PHOTOS = [
  '/LinkToPhotos/02.EDITS/2.jpg',
  '/LinkToPhotos/02.EDITS/2864.jpg',
  '/LinkToPhotos/02.EDITS/4.jpg',
  '/LinkToPhotos/02.EDITS/hrdmontogortox.png',
  '/LinkToPhotos/02.EDITS/IMG_0061_e.jpg',
  '/LinkToPhotos/02.EDITS/IMG_0064_crop.jpg',
  '/LinkToPhotos/02.EDITS/IMG_0064_e.jpg',
  '/LinkToPhotos/02.EDITS/IMG_0149_e.jpg',
  '/LinkToPhotos/02.EDITS/IMG_0157_e.jpg',
  '/LinkToPhotos/02.EDITS/IMG_0189_e.jpg',
  '/LinkToPhotos/02.EDITS/IMG_0505_e.jpg',
  '/LinkToPhotos/02.EDITS/IMG_0561_e.jpg',
  '/LinkToPhotos/02.EDITS/IMG_5488.JPG',
  '/LinkToPhotos/02.EDITS/IMG_5518.JPG',
  '/LinkToPhotos/02.EDITS/IMG_5526.JPG',
  '/LinkToPhotos/02.EDITS/IMG_5555.jpg',
  '/LinkToPhotos/02.EDITS/IMG_5648.jpg',
  '/LinkToPhotos/02.EDITS/IMG_5661.JPG',
  '/LinkToPhotos/02.EDITS/IMG_5705.jpg',
  '/LinkToPhotos/02.EDITS/IMG_5713.JPG',
  '/LinkToPhotos/02.EDITS/IMG_5730.JPG',
  '/LinkToPhotos/02.EDITS/IMG_5748.JPG',
  '/LinkToPhotos/02.EDITS/IMG_5838.JPG',
  '/LinkToPhotos/02.EDITS/IMG_5929.JPG',
  '/LinkToPhotos/02.EDITS/IMG_6205.jpg',
  '/LinkToPhotos/02.EDITS/IMG_6309.jpg',
  '/LinkToPhotos/02.EDITS/IMG_6375.jpg',
  '/LinkToPhotos/02.EDITS/IMG_6456.jpg',
  '/LinkToPhotos/02.EDITS/IMG_6567.jpg',
  '/LinkToPhotos/02.EDITS/IMG_6622.CR2',
  '/LinkToPhotos/02.EDITS/IMG_6624.CR2',
  '/LinkToPhotos/02.EDITS/IMG_6647.jpg',
  '/LinkToPhotos/02.EDITS/IMG_6772.jpg',
  '/LinkToPhotos/02.EDITS/IMG_6791.jpg',
  '/LinkToPhotos/02.EDITS/IMG_7222.jpg',
  '/LinkToPhotos/02.EDITS/IMG_7299.jpg',
  '/LinkToPhotos/02.EDITS/IMG_7341.jpg',
  '/LinkToPhotos/02.EDITS/panorama mgordo edit.jpg',
  '/LinkToPhotos/02.EDITS/panorama mgordo.jpg'
]
class Dashboard extends Component {
  render() {
    console.log(img);

    const photoComponents = PHOTOS.map(p => {
      return <PhotoCard file={p} />;
    });

    let groups = [];
    let j = 0;
    for (var i = 0; i < photoComponents.length - 3; i += N) {
      groups[j++] = (
        <CardDeck key={j}>
          {photoComponents[i]}
          {photoComponents[i + 1]}
          {photoComponents[i + 2]}
        </CardDeck>);
    }
    return (
      <div>
        <TopDrawer />
        {groups}
      </div>
    );
  }
}

export default Dashboard;
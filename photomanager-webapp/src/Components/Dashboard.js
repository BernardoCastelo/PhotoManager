import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import React, { Component } from 'react';
import PhotoCard from './PhotoComponent/PhotoCard';
import TopDrawer from './TopDrawerComponent/TopDrawerComponent';

const PHOTOS = [
  { fileName: '/LinkToPhotos/02.EDITS/2.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/2864.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/4.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/panorama mgordo.jpg', rownr: 2 },
  { fileName: '/LinkToPhotos/02.EDITS/hrdmontogortox.png', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_0061_e.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_0064_crop.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_0064_e.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_0149_e.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_0157_e.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_0189_e.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_0505_e.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_0561_e.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/panorama mgordo edit.jpg', rownr: 2 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_5488.JPG', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_5518.JPG', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_5526.JPG', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_5555.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_5648.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_5661.JPG', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_5705.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_5713.JPG', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_5730.JPG', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_5748.JPG', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_5838.JPG', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_5929.JPG', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_6205.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_6309.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_6375.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_6456.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_6567.jpg', rownr: 2 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_6647.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_6772.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_6791.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_7222.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_7299.jpg', rownr: 1 },
  { fileName: '/LinkToPhotos/02.EDITS/IMG_7341.jpg', rownr: 2 },
]
class Dashboard extends Component {
  render() {
    const photoComponents = PHOTOS.map(p => {
      return <PhotoCard file={p} />;
    });

    return (
      <div>
        <TopDrawer />
        <GridList cellHeight={200} cols={7}>
          {PHOTOS.map((image) => (
            <GridListTile key={image.fileName} cols={image.rownr}>
              {image.fileName}
              <PhotoCard file={image.fileName} />;
            </GridListTile>
          ))}
        </GridList>
      </div>
    );
  }
}

export default Dashboard;
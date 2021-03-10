import Avatar from '@material-ui/core/Avatar';
import Collapse from '@material-ui/core/Collapse';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import React from 'react';
import InfoCard from './InfoCard';
import * as constants from '../../Constants/Constants';

const CategorylistMock = [
  "Abstract",
  "Animal",
  "Architecture",
  "Astrophotography",
  "B&W",
  "Cityscape",
  "Landscape",
  "Macro",
  "Nature",
  "Panorama",
  "Portrait",
  "Random",
  "Rural",
  "Sea and Sand",
  "Travel",
  "Vehicle"
];


const CategoryList = (props) => {
  const [open, setOpen] = React.useState(false);

  const handleClick = () => {
    setOpen(!open);
  };

  const infoCards = CategorylistMock.map(cat =>
  (
    <ListItem key={cat}>
      <InfoCard label={cat} background={constants.DARKSLATEGREY} />
    </ListItem>
  ))

  return (
    <div>
      <Collapse in={open} timeout="auto" unmountOnExit>
        <List component="div" disablePadding>
          {infoCards}
        </List>
      </Collapse>

      <div style={{paddingLeft: '16px', paddingRight: '26px', paddingTop: '8px'}}onClick={handleClick}>
        <InfoCard label="Categories" background={constants.DARKCYAN} />
      </div>
    </div>

  );
};

export default CategoryList;

import Collapse from '@material-ui/core/Collapse';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import React from 'react';
import * as constants from '../../Constants/Constants';
import InfoCard from './InfoCard';

const CategoryList = (props) => {
  const [open, setOpen] = React.useState(false);

  const handleClick = () => {
    setOpen(!open);
  };

  const infoCards = props.categories.map(cat =>
  (
    <ListItem key={cat.id}>
      <InfoCard label={cat.name} background={constants.DARKSLATEGREY} />
    </ListItem>
  ))

  return (
    <div>
      <Collapse in={open} timeout="auto" unmountOnExit>
        <List component="div" disablePadding>
          {infoCards}
        </List>
      </Collapse>

      <div style={{ paddingLeft: '16px', paddingRight: '26px', paddingTop: '8px' }} onClick={handleClick}>
        <InfoCard label="Categories" background={constants.DARKCYAN} />
      </div>
    </div>

  );
};

export default CategoryList;

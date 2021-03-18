import Collapse from '@material-ui/core/Collapse';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import React from 'react';
import * as constants from '../../Constants/Constants';
import InfoCard from './InfoCard';
import CategoryCard from './CategoryCard';

const CategoryList = (props) => {
  const [open, setOpen] = React.useState(false);

  const changedCategoryCard = (categoryId, value) => {
    if (value) {
      const category = props.selectedCategories.find(element => element === categoryId);
      if (!props.selectedCategories.find(element => element === categoryId) && !!category) {
        props.selectedCategories.push(category);
      }
    }
    else {
      const category = props.selectedCategories.find(element => element === categoryId);
      if (!!category) {
        props.selectedCategories.splice(1, 0, category);
      }
    }
  }

  const handleClick = () => {
    setOpen(!open);
  };

  const infoCards = props.categories.map(cat =>
  (
    <ListItem key={cat.id}>
      <CategoryCard id={cat.id} value={!!props.selectedCategories.find(element => element === cat.id)} label={cat.name} changed={changedCategoryCard} />
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

import Paper from '@material-ui/core/Paper';
import Popper from '@material-ui/core/Popper';
import { makeStyles } from '@material-ui/core/styles';
import React from 'react';
import * as constants from '../../Constants/Constants';
import CategoryCard from './CategoryCard';
import InfoCard from './InfoCard';
import Divider from '@material-ui/core/Divider';

const useStyles = makeStyles({
  paper: {
    paddingLeft: '10%',
    paddingRight: '10%',
    marginBottom: '5%',
    minWidth: 'max-content',
    backgroundColor: constants.TRANSPARENTGREY
  }
});

const CategoryList = (props) => {
  const classes = useStyles();

  const [anchorEl, setAnchorEl] = React.useState(null);

  const handleClick = (event) => {
    setAnchorEl(anchorEl ? null : event.currentTarget);
  };

  const open = Boolean(anchorEl);

  const id = open ? 'spring-popper' : undefined;

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

  const categories = props.categories.map(cat =>
  (
    <div>
      <CategoryCard key={cat.id} value={!!props.selectedCategories.find(element => element.id === cat.id)} label={cat.name} changed={changedCategoryCard} />
      <Divider />
    </div>
  ))

  return (
    <div>
      <Popper
        id={id}
        open={open}
        anchorEl={anchorEl}
        placement="top"
      >
        <Paper className={classes.paper}>
          {categories}
        </Paper>
      </Popper>

      <div onClick={handleClick}>
        <InfoCard label="Categories" background={constants.DARKCYAN} />
      </div>
    </div>

  );
};

export default CategoryList;

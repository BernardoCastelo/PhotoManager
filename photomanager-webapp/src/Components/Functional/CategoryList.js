import Collapse from '@material-ui/core/Collapse';
import Paper from '@material-ui/core/Paper';
import { makeStyles } from '@material-ui/core/styles';
import React from 'react';
import * as constants from '../../Constants/Constants';
import CategoryCard from './CategoryCard';
import InfoCard from './InfoCard';

const useStyles = makeStyles({
  paper: {
    paddingLeft: '10%',
    marginBottom: '5%',
    minWidth: 'max-content',
    backgroundColor: constants.TRANSPARENTGREY
  }
});

const CategoryList = (props) => {

  const classes = useStyles();

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

  const categories = props.categories.map(cat =>
  (
    <Paper className={classes.paper}>
      <CategoryCard id={cat.id} value={!!props.selectedCategories.find(element => element.id === cat.id)} label={cat.name} changed={changedCategoryCard} />
    </Paper>
  ))

  return (
    <div>
      <Collapse in={open} timeout="auto" unmountOnExit>
        {categories}
      </Collapse>

      <div style={{ paddingLeft: '16px', paddingRight: '26px', paddingTop: '8px' }} onClick={() => setOpen(!open)}>
        <InfoCard label="Categories" background={constants.DARKCYAN} />
      </div>
    </div>

  );
};

export default CategoryList;

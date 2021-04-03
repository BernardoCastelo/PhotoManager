import FormControlLabel from '@material-ui/core/FormControlLabel';
import FormGroup from '@material-ui/core/FormGroup';
import { makeStyles } from '@material-ui/core/styles';
import Switch from '@material-ui/core/Switch';
import React from 'react';

const useStyles = makeStyles({
  label: {
    color: 'white'
  }
});

const CategoryCard = (props) => {

  const classes = useStyles();

  const [state] = React.useState(props.value);

  const handleChange = (event) => {
    // setState(event.target.checked);
    // props.changed(props.id, event.target.checked);
  };

  return (
    <div>
      <FormGroup row>
        <FormControlLabel className={classes.label}
          control={
            <Switch
              checked={state}
              onChange={handleChange}
              color="default"
              name={props.label}
              inputProps={{ 'aria-label': 'primary checkbox' }}
            />
          }
          label={props.label}
        />
      </FormGroup>
    </div>
  );
}
export default CategoryCard;

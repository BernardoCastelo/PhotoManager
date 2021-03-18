import FormControlLabel from '@material-ui/core/FormControlLabel';
import FormGroup from '@material-ui/core/FormGroup';
import Switch from '@material-ui/core/Switch';
import React from 'react';

const CategoryCard = (props) => {
  const [state, setState] = React.useState(false);

  const handleChange = (event) => {
    setState(event.target.checked);
    props.changed(props.id, event.target.checked);
  };

  return (
    <div>
      <FormGroup row>
        <FormControlLabel
          control={
            <Switch
              checked={state}
              onChange={handleChange}
              color="primary"
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

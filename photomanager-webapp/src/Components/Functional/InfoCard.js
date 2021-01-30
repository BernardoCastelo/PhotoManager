import React from 'react'
import Chip from '@material-ui/core/Chip';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles({
  card: {
    color: "white",
    background: 'cadetblue',
    opacity: '75%',
    fontSize: 'small',
    marginLeft: '10px'
  }
});

const InfoCard = (props) => {
  const classes = useStyles();
  return (
    <Chip className={classes.card}
      variant='outlined'
      size='large'
      label={props.label}
      color='white'
    />
  )
}
export default InfoCard;

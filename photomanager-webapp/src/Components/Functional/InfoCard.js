import React from 'react'
import Chip from '@material-ui/core/Chip';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles({
  card: {
    color: "white",
    opacity: '75%',
    fontSize: 'small',
    marginLeft: '10px'
  }
});

const InfoCard = (props) => {

  const background = props.background ? { background: props.background } : { background: "darkcyan" };

  const classes = useStyles();

  return (
    <Chip className={classes.card} style={background}
      variant='outlined'
      label={props.label}
    />
  )
}
export default InfoCard;

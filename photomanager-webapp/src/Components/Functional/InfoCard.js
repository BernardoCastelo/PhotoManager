import React from 'react'
import Chip from '@material-ui/core/Chip';
import { makeStyles } from '@material-ui/core/styles';
import * as constants from '../../Constants/Constants';

const useStyles = makeStyles({
  card: {
    color: "wheat",
    opacity: '75%',
    fontSize: 'small',
    marginLeft: '10px'
  }
});

const InfoCard = (props) => {

  const background = props.background ? { background: props.background } : { background: constants.DARKSLATEGREY };

  const classes = useStyles();

  return (
    <Chip className={classes.card}
      color="primary"
      style={background}
      variant='default'
      label={props.label}
      avatar={props.avatar}
      icon={props.icon}
    />
  )
}
export default InfoCard;

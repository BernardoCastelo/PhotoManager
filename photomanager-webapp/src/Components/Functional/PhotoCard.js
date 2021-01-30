import { makeStyles } from '@material-ui/core/styles';
import React from 'react';

const useStyles = makeStyles({
  media: {
    height: 120,
    maxWidth: '100%',
    borderRadius: '3%',
    '&:hover': {
      cursor: 'pointer'
   },
  }
});

const PhotoCard = (props) => {
  const classes = useStyles();
  return (
    <div>
      <img 
        className={classes.media} 
        src={`data:image/jpeg;base64,${props.file.thumbnail}`} 
        alt={props.file.name}
        onClick={props.clicked}/>
    </div>
  );
};

export default PhotoCard;

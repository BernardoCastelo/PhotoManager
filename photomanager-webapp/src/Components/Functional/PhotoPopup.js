import { makeStyles } from '@material-ui/core/styles';
import React from 'react';

const useStyles = makeStyles({
  media: {
    maxHeight: '90vh',
    maxWidth: '100%',
    display: 'block',
    margin: 'auto'
  }
});

const PhotoPopup = (props) => {

  console.log(props.file);

  const classes = useStyles();
  return (
    <div>
      <img
        className={classes.media}
        src={`data:image/jpeg;base64,${props.fullResolutionData}`}
        alt={props.file.name} />
      {props.file.id}
      {props.file.dateTaken}
      {props.file.cameraId}
      {props.file.categoryId}
      {props.file.fileId}
      {props.file.focalLength}
      {props.file.height}
      {props.file.iso}
      {props.file.order}
      {props.file.width}
      {props.file.exposure}
      {props.file.fStop}
      {props.file.name}
    </div>
  );
};

export default PhotoPopup;

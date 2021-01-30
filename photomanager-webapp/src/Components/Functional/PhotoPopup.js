import { makeStyles } from '@material-ui/core/styles';
import React from 'react';
import InfoCard from './InfoCard';

const useStyles = makeStyles({
  image: {
    maxHeight: '90vh',
    maxWidth: '100%',
    display: 'block',
    margin: 'auto',
    border: '2px solid white',
    borderRadius: '0.5%',
    boxShadow: '20px 50px 100px #000'
  },
  infoCard: {
    marginTop: '-5vh',
    marginLeft: 'auto',
    marginRight: 'auto',
    display: 'block',
    maxWidth: 'max-content'
  }
});

const PhotoPopup = (props) => {

  const classes = useStyles();

  return (
    <div>
      {/* <div className={classes.infoCard} style={{ marginTop: '7vh' }}>
          <InfoCard label={props.file.name}/>
        </div> */}
      <img
        className={classes.image}
        src={`data:image/jpeg;base64,${props.fullResolutionData}`}
        alt={props.file.name} />
      <div className={classes.infoCard}>
        <InfoCard label={"Date:" + props.file.dateTaken} />
        <InfoCard label={"FLength:" + props.file.focalLength} />
        <InfoCard label={"Size:" + props.file.height + "; " + props.file.width} />
        <InfoCard label={"ISO:" + props.file.iso} />
        <InfoCard label={"Exposure:" + props.file.exposure} />
        <InfoCard label={"FStop:" + props.file.fStop} />
      </div>
    </div>
  );
};

export default PhotoPopup;

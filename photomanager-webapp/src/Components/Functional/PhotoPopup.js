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
    boxShadow: '20px 50px 100px #000',
    marginTop: '-5vh',
  },
  infoCard: {
    marginTop: '-5vh',
    marginLeft: 'auto',
    marginRight: 'auto',
    display: 'block',
    maxWidth: 'max-content'
  },
  nameCard: {
    marginTop: '5vh',
    marginLeft: 'auto',
    marginRight: 'auto',
    display: 'block',
    maxWidth: 'max-content'
  }
});

const PhotoPopup = (props) => {

  const classes = useStyles();

  const datetime = new Date(props.file.dateTaken);

  const datetimeCard = props.file.dateTaken ? <InfoCard label={"Date: " + datetime.toLocaleString('pt-pt')} /> : null;
  const focalLength = props.file.focalLength ? <InfoCard label={"FLength: " + props.file.focalLength} /> : null;
  const dimentionsCard = props.file.width && props.file.height ? <InfoCard label={"Size: " + props.file.width + ":" + props.file.height} /> : null;
  const isoCard = props.file.iso ? <InfoCard label={"ISO: " + props.file.iso} /> : null;
  const exposureCard = props.file.exposure ? <InfoCard label={"Exposure: " + props.file.exposure} /> : null;
  const fstopCard = props.file.fStop ? <InfoCard label={"FStop: " + props.file.fStop} /> : null;

  return (
    <div>
      <div className={classes.nameCard}>
        <InfoCard label={props.file.name} />
      </div>
      <img
        className={classes.image}
        src={`data:image/jpeg;base64,${props.fullResolutionData}`}
        alt={props.file.name} />
      <div className={classes.infoCard}>
        {datetimeCard}
        {focalLength}
        {dimentionsCard}
        {isoCard}
        {exposureCard}
        {fstopCard}
      </div>
    </div>
  );
};

export default PhotoPopup;

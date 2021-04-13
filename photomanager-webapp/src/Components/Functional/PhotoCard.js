import React from 'react';
import './PhotoCard.css';

const PhotoCard = (props) => {
  return (
    <div>
      <img 
        className="photocard"
        src={`data:image/jpeg;base64,${props.file.thumbnail}`} 
        alt={props.file.name}
        onClick={props.clicked}/>
    </div>
  );
};

export default PhotoCard;

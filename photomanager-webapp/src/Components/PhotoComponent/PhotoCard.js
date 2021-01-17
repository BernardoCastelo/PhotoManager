import React from 'react'
import './PhotoCard.css'
import Card from 'react-bootstrap/Card'
import Icon from '../IconComponent/IconComponent'

const PhotoCard = (props) => {
  return (
    <Card border="dark" className="PhotoCard">
      <Card.Img src={props.file} alt=" " />
      <Card.ImgOverlay>
        <Card.Body className="CardBody">
          <footer>
            <Icon type="Map" />
            <Icon type="Camera" />
            <Icon type="Calendar" />
          </footer>
        </Card.Body>
      </Card.ImgOverlay>
    </Card>
  );
};

export default PhotoCard;

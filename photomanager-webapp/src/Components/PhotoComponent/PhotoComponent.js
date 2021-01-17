import Spinner from 'react-bootstrap/Spinner';
import './PhotoComponent.css';

const PhotoComponent = (props) => {
  let img = <img src={props.src} alt="" />;

  try {
    require(props.src);
  } catch (err) {
    img = <Spinner animation="border" />;
  }

  return { img }
};

export default PhotoComponent;

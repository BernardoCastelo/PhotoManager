import Popover from '@material-ui/core/Popover';
import { makeStyles } from '@material-ui/core/styles';
import React from 'react';
import Icon from '../IconComponent/IconComponent'

const ANCHOR = { vertical: 'center', horizontal: 'center' };
const TRANSFORM = { vertical: 'center', horizontal: 'center' };

const useStyles = makeStyles({
  media: {
    height: 120,
    maxWidth: "100%",
    borderRadius: '3%',
  }
});

const PhotoCard = (props) => {
  const classes = useStyles();
  const [anchorEl, setAnchorEl] = React.useState(null);
  const open = Boolean(anchorEl);
  const handlePopoverOpen = (event) => setAnchorEl(event.currentTarget);
  const handlePopoverClose = () => setAnchorEl(null);

  //onMouseEnter={handlePopoverOpen} onMouseLeave={handlePopoverClose} />
  return (
    <div>
      <img className={classes.media} src={`data:image/jpeg;base64,${props.file.thumbnail}`} alt={props.file.name}/> 
      <Popover id="mouse-over-popover" open={open} anchorEl={anchorEl} onClose={handlePopoverClose} disableRestoreFocus>
        <Icon type="Map" />
        <Icon type="Camera" />
        <Icon type="Calendar" />
      </Popover>
    </div>
  );
};

export default PhotoCard;
import Popover from '@material-ui/core/Popover';
import { makeStyles } from '@material-ui/core/styles';
import React from 'react';
import Icon from '../IconComponent/IconComponent'

const ANCHOR = { vertical: 'center', horizontal: 'center' };
const TRANSFORM = { vertical: 'center', horizontal: 'center' };

const useStyles = makeStyles({
  media: {
    maxHeight: "100%",
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

  return (
    <div>
      <img className={classes.media} src={props.file} alt={props.file} onMouseEnter={handlePopoverOpen} onMouseLeave={handlePopoverClose} />
      <Popover id="mouse-over-popover" open={open} anchorEl={anchorEl} onClose={handlePopoverClose} disableRestoreFocus>
        <Icon type="Map" />
        <Icon type="Camera" />
        <Icon type="Calendar" />
      </Popover>
    </div>
  );
};

export default PhotoCard;

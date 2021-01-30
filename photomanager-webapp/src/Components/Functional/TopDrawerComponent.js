import AppBar from '@material-ui/core/AppBar';
import IconButton from '@material-ui/core/IconButton';
import { makeStyles } from '@material-ui/core/styles';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import React, { useState } from 'react';

const useStyles = makeStyles({
  media: {
    backgroundColor: '#383838'
  },
});

const TopDrawer = () => {
  const [startDate, setStartDate] = useState(new Date());
  const [endDate, setEndDate] = useState(new Date());
  const [state, setState] = useState(true);

  const classes = useStyles();

  return (
    <AppBar className={classes.media} position="static">
      <Toolbar>
        <IconButton edge="start" aria-label="menu">
        </IconButton>
        <Typography variant="h6">
          ToDo: App bar seu preguiçoso
        </Typography>
      </Toolbar>
    </AppBar>
  );
}

export default TopDrawer;

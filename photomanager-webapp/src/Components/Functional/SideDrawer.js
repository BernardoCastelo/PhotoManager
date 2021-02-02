import Drawer from '@material-ui/core/Drawer';
import Fab from '@material-ui/core/Fab';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import ArrowBackIosRoundedIcon from '@material-ui/icons/ArrowBackIosRounded';
import React from 'react';
import SearchIcon from '@material-ui/icons/Search';

const useStyles = makeStyles({
  fab: {
    position: 'absolute',
    top: '1.2%',
    right: '1.75%',
    zIndex: '10'
  },
  bottomFab: {
    position: 'absolute',
    top: '90%',
    right: '40%',
    zIndex: '10'
  },
  sideDrawer: {
    padding: '5%',
    width: 350
  },
  container: {
    display: 'flex',
    flexWrap: 'wrap'
  },
  textField: {
    marginLeft: '5%',
    marginRight: '5%',
    width: 200,
  },
  root: {
    '& > *': {
      fontSize: 'medium',
      marginRight: '5%',
      marginBottom: '50px'
    },
  },
});

const SideDrawer = () => {
  const classes = useStyles();

  const [selectedDate, setSelectedDate] = React.useState(new Date());


  const handleDateChange = (date) => {
    setSelectedDate(date);
  };

  const [state, setState] = React.useState({
    right: false,
  });

  const toggleDrawer = (anchor, open) => (event) => {
    if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
      return;
    }
    setState({ ...state, [anchor]: open });
  };


  return (
    <div>
      <Fab
        className={classes.fab}
        color="primary"
        aria-label="add"
        onClick={toggleDrawer('right', true)}>
        <ArrowBackIosRoundedIcon />
      </Fab>
      <Drawer anchor={'right'} open={state['right']} onClose={toggleDrawer('right', false)}>
        <div className={classes.sideDrawer}>
          <form className={[classes.container, classes.root]} noValidate>
            <TextField
              id="date"
              label="From"
              type="date"
              className={[classes.textField, classes.root]}
              InputLabelProps={{
                shrink: true,
              }}
            />
          </form>
          <form className={[classes.container, classes.root]} noValidate>
            <TextField
              id="date"
              label="From"
              type="date"
              className={[classes.textField, classes.root]}
              InputLabelProps={{
                shrink: true,
              }}
            />
          </form>
          <Fab
            className={classes.bottomFab}
            color="primary"
            aria-label="add"
            onClick={toggleDrawer('right', true)}>
            <SearchIcon />
          </Fab>
        </div>
      </Drawer>
    </div>
  );
}

export default SideDrawer;

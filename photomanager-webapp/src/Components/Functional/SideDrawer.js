import Drawer from '@material-ui/core/Drawer';
import Fab from '@material-ui/core/Fab';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import ArrowBackIosRoundedIcon from '@material-ui/icons/ArrowBackIosRounded';
import React from 'react';
import SearchIcon from '@material-ui/icons/Search';
import Paper from '@material-ui/core/Paper';

const useStyles = makeStyles({
  nameDivs: {
    fontSize: 'large',
    color: 'primary',
    marginBottom: '25px'
  },
  divider: {
    marginTop: '50px'
  },
  paper: {
    marginRight: '10px',
    padding: '20px',
  },
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
    width: 500
  },
  container: {
    display: 'flex',
    flexWrap: 'wrap'
  },
  textField: {
    marginLeft: '5%',
    marginRight: '5%',
    width: 150,
  },
  root: {
    '& > *': {
      fontSize: 'medium',
      marginRight: '5%'
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
          <Paper className={classes.paper} elevation={3}>
            <div className={classes.nameDivs}>Filters</div>
            <div style={{ display: 'ruby' }}>
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
                  label="To"
                  type="date"
                  className={[classes.textField, classes.root]}
                  InputLabelProps={{
                    shrink: true,
                  }}
                />
              </form>
            </div>
            <div className={classes.divider}>
              <form className={[classes.container, classes.root]} noValidate autoComplete="off">
                <TextField className={[classes.textField, classes.root]} id="standard-basic" label="Exposure" type="number" InputLabelProps={{ shrink: true, }}  />
                <TextField className={[classes.textField, classes.root]} id="standard-basic" label="Apperture" type="number" InputLabelProps={{ shrink: true, }}  />
                <TextField className={[classes.textField, classes.root]} id="standard-basic" label="Focal Length" type="number" InputLabelProps={{ shrink: true, }} />
                <TextField className={[classes.textField, classes.root]} id="standard-basic" label="ISO" type="number" InputLabelProps={{ shrink: true, }} />
              </form>
            </div>
          </Paper>
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

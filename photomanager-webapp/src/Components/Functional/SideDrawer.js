import Drawer from '@material-ui/core/Drawer';
import Fab from '@material-ui/core/Fab';
import FormControl from '@material-ui/core/FormControl';
import Paper from '@material-ui/core/Paper';
import Select from '@material-ui/core/Select';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import ArrowBackIosRoundedIcon from '@material-ui/icons/ArrowBackIosRounded';
import SearchIcon from '@material-ui/icons/Search';
import React from 'react';
import styled from "styled-components";
import ExpandLessIcon from '@material-ui/icons/ExpandLess';

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
    marginBottom: '10px',
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
  'MuiFab-root':{
    boxShadow:'none'
  },
  root: {
    '& > *': {
      fontSize: 'medium',
      marginRight: '5%'
    },
  },
});

const IconButtonWrapper = styled.div`
  float: right;
  transform: rotate(0deg);
  overflow: hidden;
  transition: all 0.3s ease-out;
  transform: ${props => (props.rotate ? `rotate(180deg)` : "")};`;

const SideDrawer = (props) => {
  const classes = useStyles();

  const searchBtnHandler = () => {
    toggleDrawer('right', false);
    props.searchClick(OrderByState);
  }

  /* Drawer */
  const [drawerState, setDrawerState] = React.useState({
    right: false,
  });

  const toggleDrawer = (anchor, open) => (event) => {
    if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
      return;
    }
    setDrawerState({ ...drawerState, [anchor]: open });
  };

  /* OrderBy Select */
  const [OrderByState, setOrderByState] = React.useState({
    orderByDescending: false,
    filter: '',
    name: 'hai',
  });

  const handleOrderByChange = (event) => {
    const name = event.target.name;
    setOrderByState({
      ...OrderByState,
      [name]: event.target.value,
    });
    console.log(event.target);
  };

  const handleOrderByDirectionChange = () => {
    setOrderByState({
      ...OrderByState,
      'orderByDescending': !OrderByState.orderByDescending,
    });
    console.log(OrderByState);
  };

  return (
    <div>
      <Fab
        className={classes.fab}
        color="primary"
        aria-label="add"
        onClick={toggleDrawer('right', true)}>
        <ArrowBackIosRoundedIcon fontSize="large"/>
      </Fab>
      <Drawer anchor={'right'} open={drawerState['right']} onClose={toggleDrawer('right', false)}>
        <div className={classes.sideDrawer}>

          <Paper className={classes.paper} elevation={3}>
            <div className={classes.nameDivs}>Order By</div>
            <div>
              <FormControl className={classes.formControl + ' ' + classes.textField + ' ' + classes.root}>
                <Select
                  native
                  value={OrderByState.filter}
                  onChange={handleOrderByChange}
                  inputProps={{
                    name: 'filter',
                    id: 'order-by-select',
                  }}
                >
                  <option aria-label="None" value="" />
                  <option value={'dateTaken'}>Date Taken</option>
                  <option value={'focalLength'}>Focal Length</option>
                  <option value={'iso'}>ISO</option>
                  <option value={'exposure'}>Exposure</option>
                  <option value={'fStop'}>F-Stop</option>
                </Select>
              </FormControl>
              <IconButtonWrapper rotate={OrderByState.orderByDescending}>
                <Fab
                  className={classes['MuiFab-root']}
                  color="primary"
                  size="small"
                  onClick={handleOrderByDirectionChange}>
                  <ExpandLessIcon fontSize="large"/>
                </Fab>
              </IconButtonWrapper>
            </div>
          </Paper>

          <Paper className={classes.paper} elevation={3}>
            <div className={classes.nameDivs}>Filters</div>
            <div style={{ display: 'ruby' }}>
              <form className={classes.container + ' ' + classes.root} noValidate>
                <TextField
                  id="date"
                  label="From"
                  type="date"
                  className={classes.textField + ' ' + classes.root}
                  InputLabelProps={{
                    shrink: true,
                  }}
                />
              </form>
              <form className={classes.container + ' ' + classes.root} noValidate>
                <TextField
                  id="date"
                  label="To"
                  type="date"
                  className={classes.textField + ' ' + classes.root}
                  InputLabelProps={{
                    shrink: true,
                  }}
                />
              </form>
            </div>
            <div className={classes.divider}>
              <form className={classes.container + ' ' + classes.root} noValidate autoComplete="off">
                <TextField className={classes.textField + ' ' + classes.root} id="standard-basic" label="Exposure" type="number" InputLabelProps={{ shrink: true, }} />
                <TextField className={classes.textField + ' ' + classes.root} id="standard-basic" label="Apperture" type="number" InputLabelProps={{ shrink: true, }} />
                <TextField className={classes.textField + ' ' + classes.root} id="standard-basic" label="Focal Length" type="number" InputLabelProps={{ shrink: true, }} />
                <TextField className={classes.textField + ' ' + classes.root} id="standard-basic" label="ISO" type="number" InputLabelProps={{ shrink: true, }} />
              </form>
            </div>
          </Paper>
          <Fab
            className={classes.bottomFab}
            color="primary"
            aria-label="add"
            onClick={searchBtnHandler}>
            <SearchIcon fontSize="large"/>
          </Fab>
        </div>
      </Drawer>
    </div>
  );
}

export default SideDrawer;

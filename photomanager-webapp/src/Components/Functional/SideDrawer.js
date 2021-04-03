import Drawer from '@material-ui/core/Drawer';
import Fab from '@material-ui/core/Fab';
import FormControl from '@material-ui/core/FormControl';
import Paper from '@material-ui/core/Paper';
import Select from '@material-ui/core/Select';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import ArrowBackIosRoundedIcon from '@material-ui/icons/ArrowBackIosRounded';
import ExpandLessIcon from '@material-ui/icons/ExpandLess';
import SearchIcon from '@material-ui/icons/Search';
import React from 'react';
import styled from "styled-components";
import FilterSlider from './FilterSlider';
import * as constants from '../../Constants/Constants';

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
    width: 400
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
  'MuiFab-root': {
    boxShadow: 'none'
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

  const [isoFilter, setIsoFilter] = React.useState(null);
  const [exposureFilter, setExposureFilter] = React.useState(null);
  const [fStopFilter, setFStopFilter] = React.useState(null);
  const [focalLengthFilter, setFocalLengthFilter] = React.useState(null);
  const [toDateFilter, setToDateFilter] = React.useState('');
  const [fromDateFilter, setFromDateFilter] = React.useState('');
  const [OrderByState, setOrderByState] = React.useState({ orderByDescending: true, filter: '', name: 'hai' });

  const handleSearchBtnClick = (event) => {
    toggleDrawer(false)(event);
    let appliedFilters = [];

    if (isoFilter) appliedFilters.push(isoFilter);
    if (exposureFilter) appliedFilters.push(exposureFilter);
    if (fStopFilter) appliedFilters.push(fStopFilter);
    if (focalLengthFilter) appliedFilters.push(focalLengthFilter);
    if (toDateFilter || fromDateFilter) {
      appliedFilters.push(
        {
          FieldName: constants.DATETAKEN,
          LowerValue: new Date(fromDateFilter),
          UpperValue: new Date(new Date(toDateFilter).setHours(23, 59, 59))
        });
    } 

    props.searchClick({
      orderByDescending: OrderByState.orderByDescending,
      filter: OrderByState.filter,
      filters: appliedFilters
    });
  }

  const toggleDrawer = (open) => (event) => {
    if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
      return;
    }

    props.changed(open);
  };

  const handleOrderByChange = (event) => {
    const name = event.target.name;
    setOrderByState({
      ...OrderByState,
      [name]: event.target.value,
    });
  };

  const handleOrderByDirectionChange = () => {
    setOrderByState({
      ...OrderByState,
      'orderByDescending': !OrderByState.orderByDescending,
    });
  };

  const handleToDateChange = (event) => {
    setToDateFilter(event.target.value);
  };

  const handleFromDateChange = (event) => {
    setFromDateFilter(event.target.value);
  };

  return (
    <div>
      <Drawer anchor={'left'} open={props.opened} onClose={toggleDrawer(false)}>
        <div className={classes.sideDrawer}>

         {/* SORTING */}
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
                  <option value={constants.DATETAKEN}>Date Taken</option>
                  <option value={constants.EXPOSURE}>Exposure</option>
                  <option value={constants.FOCALLENGTH}>Focal Length</option>
                  <option value={constants.ISO}>ISO</option>
                  <option value={constants.FSTOP}>F-Stop</option>
                </Select>
              </FormControl>
              <IconButtonWrapper rotate={OrderByState.orderByDescending ? 1 : 0}>
                <Fab
                  className={classes['MuiFab-root']}
                  color="primary"
                  size="small"
                  onClick={handleOrderByDirectionChange}>
                  <ExpandLessIcon fontSize="large" />
                </Fab>
              </IconButtonWrapper>
            </div>
          </Paper>

          {/* FILTERING */}
          <Paper className={classes.paper} elevation={3}>
            <div className={classes.nameDivs}>Filters</div>
            <div style={{ display: 'ruby' }}>
              <form className={classes.container + ' ' + classes.root} noValidate>
                <TextField
                  id="dateFrom"
                  value={fromDateFilter}
                  onChange={handleFromDateChange}
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
                  id="dateTo"
                  value={toDateFilter}
                  onChange={handleToDateChange}
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
              <FilterSlider type={constants.ISO} marks={constants.ISOMARKS} setValue={setIsoFilter} initialFilter={isoFilter} />
              <FilterSlider type={constants.EXPOSURE} marks={constants.EXPOSUREMARKS} setValue={setExposureFilter} initialFilter={exposureFilter} />
              <FilterSlider type={constants.FSTOP} marks={constants.FSTOPMARKS} setValue={setFStopFilter} initialFilter={fStopFilter} />
              <FilterSlider type={constants.FOCALLENGTH} marks={constants.FOCALLENGTHMARKS} setValue={setFocalLengthFilter} initialFilter={focalLengthFilter} />
            </div>
          </Paper>

          {/* SEARCH */}
          <Fab
            className={classes.bottomFab}
            color="primary"
            aria-label="add"
            onClick={handleSearchBtnClick}>
            <SearchIcon fontSize="large" />
          </Fab>
        </div>
      </Drawer>
    </div>
  );
}

export default SideDrawer;

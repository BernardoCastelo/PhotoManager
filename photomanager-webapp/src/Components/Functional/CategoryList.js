import React from 'react'
import InfoCard from './InfoCard'
import Menu from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';
import { withStyles } from '@material-ui/core/styles';

const CategorylistMock = [
  "Abstract",
  "Animal",
  "Architecture",
  "Astrophotography",
  "B&W",
  "Cityscape",
  "Landscape",
  "Macro",
  "Nature",
  "Panorama",
  "Portrait",
  "Random",
  "Rural",
  "Sea and Sand",
  "Travel",
  "Vehicle"
];

const StyledMenu = withStyles({
  paper: {
    border: '1px solid #d3d4d5',
  },
})((props) => (
  <Menu
    elevation={0}
    getContentAnchorEl={null}
    style={{ background: 'transparent' }}
    anchorOrigin={{
      vertical: 'top',
      horizontal: 'center',
    }}
    transformOrigin={{
      vertical: 'top',
      horizontal: 'center',
    }}
    {...props}
  />
));

const CategoryList = (props) => {
  const [anchorEl, setAnchorEl] = React.useState(null);

  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const infoCards = CategorylistMock.map(cat => <MenuItem key={cat}><InfoCard label={cat} background="orangered" /></MenuItem>)

  return (
    <div>
      <StyledMenu
        id="simple-menu"
        anchorEl={anchorEl}
        keepMounted
        open={Boolean(anchorEl)}
        onClose={handleClose}
      >
        {infoCards}
      </StyledMenu>
      <div onClick={handleClick}>
        <InfoCard label="Categories" background="orangered" />
      </div>
    </div>

  );
};

export default CategoryList;

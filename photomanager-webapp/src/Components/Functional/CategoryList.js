import React from 'react'
import InfoCard from './InfoCard'

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
const CategoryList = (props) => {

  const infoCards = CategorylistMock.map(cat => <div style={{ position: 'relative' }}><InfoCard key={cat} label={cat} background="orangered" /></div>)

  return (
    <div>
      <div>{infoCards}</div>
      <InfoCard label="Categories" background="orangered" />
    </div>

  );
};

export default CategoryList;

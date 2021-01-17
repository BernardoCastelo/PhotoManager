import React from 'react'
import { FiCalendar } from "react-icons/fi";
import { FiMapPin } from "react-icons/fi";
import { FiCamera } from "react-icons/fi";

const ICON_TYPES = {
  Calendar: <FiCalendar/>,
  Map: <FiMapPin/>,
  Camera: <FiCamera/>
};

const Icon = (props) => {
  const type = ICON_TYPES[props.type];
  return (type);
}

export default Icon;

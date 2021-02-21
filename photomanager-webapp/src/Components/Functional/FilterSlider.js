import React from 'react'
import Typography from '@material-ui/core/Typography';
import Slider from '@material-ui/core/Slider';

const FilterSlider = (props) => {
  const max = props.marks[props.marks.length - 1].index;

  const data = props.marks.map((elem) => { 
    return {
      value: elem.index,
      label: elem.label
    };
    
  });

  const [state, setState] = React.useState([
    props.initialFilter?.LowerValue != null? indexFromValue(props.initialFilter.LowerValue) : 1, 
    props.initialFilter?.UpperValue != null? indexFromValue(props.initialFilter.UpperValue) : max
  ]);

  const handleSliderChange = (event, newIndex) => {
    setState(newIndex);
    const filter = makeFilter();
    props.setValue(filter)
  };

  function valueLabelFormat(index) {
    return props.marks[index - 1].label;
  }

  function valueFormat(index) {
    return props.marks[index - 1].value;
  }

  function indexFromValue(value) {
    return props.marks.find(m => m.value === value)?.index;
  }

  function makeFilter() {
    if (state) {
      if (state[0] === state[1]) {
        return {
          FieldName: props.type,
          Value: valueFormat(state[0])
        };
      }
      return {
        FieldName: props.type,
        LowerValue: valueFormat(state[0]),
        UpperValue: valueFormat(state[1])
      };
    }
    return null;
  }

  return (
    <div>
      <Typography id="slider" gutterBottom>{props.type}</Typography>
      <Slider
        value={state}
        onChange={handleSliderChange}
        valueLabelDisplay="off"
        step={null}
        marks={data}
        min={1}
        max={max}
        getAriaValueText={valueLabelFormat}
        valueLabelFormat={valueLabelFormat}
        aria-labelledby="slider"
      />
    </div>
  );
};

export default FilterSlider;

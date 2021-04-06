import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import Dashboard from './Components/Dashboard'
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import * as constants from './Constants/Constants';

const theme = createMuiTheme({
  palette: {
    primary: {
      main: constants.DARKSLATEGREY
    },
    secondary: {
      main: constants.DARKCYAN
    }
  }
});

function App() {
  return (
    <MuiThemeProvider theme={theme} className="App">
      <Dashboard/>
    </MuiThemeProvider>
  );
}

export default App;

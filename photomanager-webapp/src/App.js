import { createMuiTheme, MuiThemeProvider } from '@material-ui/core/styles';
import 'bootstrap/dist/css/bootstrap.min.css';
import {
  BrowserRouter as Router, Redirect, Route, Switch
} from "react-router-dom";
import './App.css';
import Dashboard from './Components/Dashboard';
import Login from './Components/Login';
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
    <Router>
      <Switch>
        <Route exact path="/">
          <Login />
        </Route>
        <Route path="/dashboard">
          <div className="App">
            <MuiThemeProvider theme={theme} >
              <Dashboard />
            </MuiThemeProvider>
          </div>
        </Route>
        <Route path="*">
        <Redirect
            to={{
              pathname: "/"
            }}
          />
        </Route>
      </Switch>
    </Router>
  );
}

export default App;

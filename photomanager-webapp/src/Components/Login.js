import Button from '@material-ui/core/Button';
import InputAdornment from '@material-ui/core/InputAdornment';
import TextField from '@material-ui/core/TextField';
import AccountCircle from '@material-ui/icons/AccountCircle';
import React, { Component } from 'react';
import HttpService from '../Services/HttpService';
import './Login.css';

class Login extends Component {
  constructor(props) {
    super(props);
    this.httpService = new HttpService();

    this.state = {
      userName: "",
      password: ""
    };

    this.LoginUser = this.LoginUser.bind(this);
  }

  componentDidMount() {
  }

  render() {
    const handleLoginChange = (event) => {
      this.setState({ userName: event.target.value });
    };

    const handlePasswordChange = (event) => {
      this.setState({ password: event.target.value });
    };

    return (
      <div className="parallax">
        <div className="loginDiv">
          <TextField
            className="textField"
            label="Login"
            color="secondary"
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <AccountCircle />
                </InputAdornment>
              ),
            }}
            onChange={handleLoginChange}
          />
          <TextField
            className="textField"
            label="Password"
            color="secondary"
            type="password"
            autoComplete="current-password"
            onChange={handlePasswordChange}
          />
          <div className="buttonDiv">
            <Button className="button" variant="outlined" onClick={this.LoginUser}>Login</Button>
          </div>
        </div>
      </div>
    );
  }

  LoginUser() {
    this.httpService
      .Login(this.state.userName, this.state.password, this.dashboardURL)
      .then(response => {
        // console.log(response);
        if (response) {
          this.setState({
            categories: response.data
          });
        }
      });
  }

  //#endRegion WebMethods

  //#region Vars

  httpService = null;
  dashboardURL = ""

  //#endRegion Vars
}

export default Login;
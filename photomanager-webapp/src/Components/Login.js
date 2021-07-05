import React, { Component } from 'react';
import HttpService from '../Services/HttpService';
import './Dashboard.css';

class Login extends Component {
  constructor(props) {
    super(props);
    this.httpService = new HttpService();

    this.state = {
      userName: "",
      password: ""
    };
  }

  componentDidMount() {
  }

  render() {
    return (
      <div>
        LOGIN
      </div>
    );
  }

  async Login() {
    this.httpService
      .Login(this.state.userName, this.state.password, this.dashboardURL)
      .then(response => {
        if (response) {
          this.setState({
            categories: response.data
          });
        }
      });
  }

  //#endRegion WebMethods

  //#region Vars

  dashboardURL = ""

  //#endRegion Vars
}

export default Login;
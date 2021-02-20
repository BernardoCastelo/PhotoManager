const axios = require('axios').default;

function HandleError(error) {
}


export default class HttpService {
  constructor() {
    this.instance = axios.create({
      baseURL: '',
      timeout: 30000,
      headers: { 'accept': 'text/plain' }
    });
  };

  async GetImages(skip, take, orderBy, orderByDescending, filters) {
    if (!filters) filters = [];
    try {
      return this.instance.post('/Photo/Get/', filters, {
        params: {
          skip: skip,
          take: take,
          orderBy: orderBy,
          orderByDescending: orderByDescending
        }
      }).catch(function (error) {
        HandleError(error);
      });

    } catch (error) {
      console.error(error);
      return null;
    }
  }

  async GetFullResolutution(id) {
    try {
      return this.instance.get('/Photo/GetBytes/', {
        params: {
          id: id
        }
      }).catch(function (error) {
        HandleError(error);
      });
    } catch (error) {
      console.error(error);
      return null;
    }
  }

  instance = null;
}
const axios = require('axios').default;

export default class HttpService {
  constructor() {
    this.instance = axios.create({
      baseURL: '',
      timeout: 30000,
      headers: { 'accept': 'text/plain' }
    });
  };

  async GetImages(skip, take, orderBy, orderByDescending) {
    try {
      return this.instance.get('/Photo/Get/', {
        params: {
          skip: skip,
          take: take,
          orderBy: orderBy,
          orderByDescending: orderByDescending
        }
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
      });
    } catch (error) {
      console.error(error);
      return null;
    }
  }

  instance = null;
}
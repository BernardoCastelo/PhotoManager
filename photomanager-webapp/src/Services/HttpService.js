const axios = require('axios').default;

export default class HttpService {
  constructor() {
    this.instance = axios.create({
      baseURL: '',
      timeout: 5000,
      headers: { 'accept': 'text/plain' }
    });
  };

  async GetImages(skip, take, orderBy) {
    try {
      return this.instance.get('/Photo/Get/', {
        params: {
          skip: skip,
          take: take,
          orderBy: orderBy,
          orderByDescending: true
        }
      });
    } catch (error) {
      console.error(error);
      return null;
    }
  }

  instance = null;
}
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

  async GetCategories() {
    try {
      return this.instance.get('/Category/GetAll/')
        .catch(function (error) {
          HandleError(error);
        });
    } catch (error) {
      console.error(error);
      return null;
    }
  }

  async GetPhotoCategories(photoId) {
    try {
      return this.instance.get('/Photo/GetCategories/', {
        params: {
          Id: photoId
        }
      }).catch(function (error) {
          HandleError(error);
        });
    } catch (error) {
      console.error(error);
      return null;
    }
  }

  async Login(userName, password, returnUrl) {
    try {
      return this.instance.get('/Auth/Login/', {
        params: {
          userName: userName,
          password: password,
          returnUrl: returnUrl
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
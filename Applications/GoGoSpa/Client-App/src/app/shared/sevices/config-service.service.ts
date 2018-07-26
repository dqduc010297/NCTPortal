import { Injectable } from '@angular/core';


// TODO: Remove this service & use APP_INITIALIZER_PROVIDER & ServiceRegistryService
export class ConfigService {

  _apiURI: string;

  constructor() {
    this._apiURI = 'http://localhost:54520/api';
  }

  getApiURI() {
    return this._apiURI;
  }
}

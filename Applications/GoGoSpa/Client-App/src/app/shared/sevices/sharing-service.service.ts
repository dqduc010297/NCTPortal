import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RequestOptions, Headers, Http } from '@angular/http';
import decode from 'jwt-decode';

// TODO: Remove sharing service. State ofr update/creation form should be managed by URL instead of sharing service
@Injectable({
  providedIn: 'root'
})
export class SharingService {

  private array = [];

  constructor() { }

  save(array) {
    this.array = array;
  }

  fetch() {
    return this.array;
  }

  public isNewShipment: boolean;
  public shipmentCode: any;

  DecodeToken(): any {
      const token = localStorage.getItem('tokenKey');

      const tokenPayload = decode(token);

      return tokenPayload;
  }

  getRole(): string {

    var role = this.DecodeToken();
    return role['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
  }

  getName(): string {
    var name = this.DecodeToken();
    return name['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
  }

}

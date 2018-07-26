import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    public jwtHelper: JwtHelperService,
    private router: Router
  ) { }

  public isAuthenticated(): boolean { 
    const token = localStorage.getItem('tokenKey');
    // Check whether the token is expired and return
    // true or false
    if (token != null && token != "undefined") {
      const tokenKey = JSON.parse(token);
      const currentToken = tokenKey.access_token;
      return !this.jwtHelper.isTokenExpired(currentToken);
    }
    return false;
  }
}

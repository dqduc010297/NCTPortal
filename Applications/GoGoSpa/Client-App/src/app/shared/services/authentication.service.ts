import { Injectable } from '@angular/core';
// APP_CONSTRAIN.ts
export const APP_CONSTRAIN = {
  authentication: {
    tokenStoreKey: '.access_token'
  }
};

@Injectable({
  providedIn: 'root'
})


export class AuthenticationService {
  private _userTokenStoreKey: string = ".access_token";

  constructor() { }

  public storeToken(token: string) {
    localStorage.setItem(APP_CONSTRAIN.authentication.tokenStoreKey, token);
  }

}

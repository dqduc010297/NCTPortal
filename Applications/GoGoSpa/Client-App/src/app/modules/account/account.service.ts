import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { AuthHttpService } from 'src/app/shared';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(
    private _apiHttp: AuthHttpService
  ) { }

  login(formData: any): Observable<any> {
    return this._apiHttp.post(`/api/authentication/token`, formData)
  }
}

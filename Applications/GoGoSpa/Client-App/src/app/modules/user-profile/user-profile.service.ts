import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { AuthHttpService } from 'src/app/shared';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {

  constructor(
    private _apiHttp: AuthHttpService
  ) { }

  viewUserProfileData(): Observable<any> {
    return this._apiHttp.get(`/api/user/myprofile`);
  }

  getUserProfileFormData(): Observable<any> {
    return this._apiHttp.get(`/api/user/myprofile`);
  }

  editUserProfile(formData: any): Observable<any> {
    return this._apiHttp.put(`/api/user`, formData);
  }
}

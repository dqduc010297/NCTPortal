import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { UserProfileEdit } from './UserProfileEdit';
import { NotificationService } from 'src/app/shared/component/dialog/notification.service';
//import { UserCreateComponent } from '../user-create/user-create.component';
import { UserProfileService } from '../user-profile.service';

// TODO: Move user-profile to another module, because user profile is not belong to user management or identity management
// Move it to modules/user-profile/user-profile-edit

@Component({
  selector: 'app-user-profile-edit',
  templateUrl: './user-profile-edit.component.html',
  styleUrls: ['./user-profile-edit.component.scss']
})
export class UserProfileEditComponent implements OnInit {
  id: any = {};
  public model: any = {
    email: '',
    phoneNumber: ''
  };
  baseUrlProfileInfoBeEdit: string;
  baseUrlProfileEdit: string;

  public message: string = null;
  public isError: boolean = false;
  public lStorage = localStorage.length;

  constructor(
    private _router: Router,
    private _route: ActivatedRoute,
    private _location: Location,
    private _notificationService: NotificationService,
    private _userProfileService: UserProfileService
  ) { }

  ngOnInit() {

    // TODO: Move all HTTPs request relate to user API into seperated service
    // You need to create UserProfileService in modules/user-profile/user-profile.service.ts
    //
    // then you call _userProfileService.GetMine().subcrible(result=>{
    // })
    //
    // httpOptions, API url... will be managed by API service

    this._userProfileService.getUserProfileFormData().subscribe(result => {
      this.model = result;
    })
  }

  save(id) {
    // TODO: Move all HTTPs request relate to user API into seperated service
    // You need to create UserProfileService in modules/user-profile/user-profile.service.ts
    //
    // then you call _userProfileService.Update().subcrible(result=>{
    // })
    //
    // httpOptions, API url... will be managed by API service
    //
    // You do not need to pass user id to API, because you are updating self profile
    // Backend will get user id via access token 

    this._userProfileService.editUserProfile(this.model).subscribe(result => {
      this._router.navigate(['profile']);
    }, error => {
      this.isError = true;

      let httpError: HttpErrorResponse = error;
      if (httpError.status === 400) {

        this.message = httpError.error.message;
      } else {
        this._notificationService.prompError(httpError.message);
      }
    });
  }

  back() {
    this._location.back();
  }
}

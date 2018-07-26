import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { HttpErrorResponse } from '@angular/common/http';
import { Location } from '@angular/common';
import { NotificationService } from 'src/app/shared/component/dialog/notification.service';
//import { EmailvalidatorDirective } from 'src/app/shared/directives/emailvalidator.directive';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.scss']
})
export class UserCreateComponent implements OnInit {
  public listItems: Array<string> = ["Customer", "Driver", "Coordinator", "Administrator"];
  data: any = {};
  public message: string = null;
  public model = {
    username: '',
    password: '',
    repassword: '',
    firstname: '',
    lastname: '',
    email: '',
    phonenumber: '',
    role: ''
  };
  public isError: boolean = false;
  public lStorage = localStorage.length;

  constructor(
    private _router: Router,
    private _location: Location,
    private _notificationService: NotificationService,
    private _userService: UserService
  ) { }

  ngOnInit() {
  }

  onCreate() {
    // TODO: Move all HTTPs request relate to user API into seperated service
    // You need to create UserService in ../../identity/user/user.service.ts
    //
    // then you call _userService.Create(this.model).subcrible(result=>{
    //  this.data = result
    //  this._router.navigate(['account/detail', this.data.value]);
    // })
    //
    // httpOptions, API url... will be managed by API service
    this._userService.create(this.model).subscribe(result => {
      this.data = result;
      this._router.navigate(['account/detail', this.data.value]);
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

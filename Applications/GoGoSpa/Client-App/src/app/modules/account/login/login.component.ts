import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationService } from 'src/app/shared/component/dialog/notification.service';
import { AccountService } from '../account.service';
import * as toastr from 'toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public message: string = null;
  response;

  public model = {
    username: '',
    password: ''
  };
  public isError: boolean = false;
  constructor(
    private _router: Router,
    private _notificationService: NotificationService,
    private _accountService: AccountService
  ) {
    toastr.options = {
      "closeButton": true,
      "debug": false,
      "newestOnTop": false,
      "progressBar": false,
      "positionClass": "toast-top-right",
      "preventDuplicates": true,
      "onclick": null,
      "showDuration": "300",
      "hideDuration": "300",
      "timeOut": "2000",
      "extendedTimeOut": "1000",
      "showEasing": "swing",
      "hideEasing": "linear",
      "showMethod": "fadeIn",
      "hideMethod": "fadeOut"
    }
  }

  ngOnInit() {
  }

  onSubmit() {
    // TODO: console.log is used to troubleshooting only, It should be removed


    // TODO: Move all HTTPs request relate to user API into seperated service
    // You need to create UserService in modles/account/account.service.ts
    //
    // then you call _accountService.Login(this.userName, this.password).subcrible(result=>{
    // 
    // })
    //
    // httpOptions, API url... will be managed by API service
    this._router.navigate(['home']);
   // this._accountService.login(this.model).subscribe(result => {
     
    //  var key = "tokenKey";
    //  if (result) {
    //    var keyValue = JSON.stringify(result);
    //    localStorage.setItem(key, keyValue);
    //    this._router.navigate(['home']);
    //  }
    //}, error => {
    //  this.isError = true;

    //  let httpError: HttpErrorResponse = error;
    //  if (httpError.status === 400) {

    //    this.message = httpError.error.message;
    //  } else {
    //    this._notificationService.prompError(httpError.message);
    //  }
    //});
  }

  displayToastr() {
    toastr["info"]('This feature is under construction!');
  }
}

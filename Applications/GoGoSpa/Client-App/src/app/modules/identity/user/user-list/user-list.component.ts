import { Component, OnInit } from '@angular/core';
import { UserList } from './UserList';
import { Router } from '@angular/router';
import { HttpParamsOptions, HttpParams } from '@angular/common/http/src/params';
import { NgForm } from '@angular/forms';
import { UserService } from '../user.service';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent, SelectableSettings } from '@progress/kendo-angular-grid';
import { DataSourceRequestState, DataResult } from '@progress/kendo-data-query';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  data: any = {};
  userList: UserList[];
  total: number;
  currentpage: number;
  id: number;
  selectOption: string = "";
  public lStorage = localStorage.length;

  public users: GridDataResult;
  public state: DataSourceRequestState = {
    skip: 0,
    take: 15
  };

  constructor(
    private _router: Router,
    private _userService: UserService
  ) {
    this._userService.fetch(this.state).subscribe(result => {
      this.users = result;
    });
  }

  paginators = [];
  ngOnInit() {
  }

  // TODO: replace this.id by role name
  // TODO: Remove all if/else statement, use only one statement only

  // TODO: Move all HTTPs request relate to user API into seperated service
  // You need to create UserService in ../../identity/user/user.service.ts
  //
  // then you call _userService.GetAll({role:this.role}).subcrible(result=>{
  // })
  //
  // httpOptions, API url... will be managed by API service

  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    this._userService.fetch(state)
      .subscribe(r => this.users = r);
  }

  loadDetail(id) {
    this._router.navigate(['account/detail', id]);
  }

  loadUpdate(id) {
    this._router.navigate(['account/edit', id])
  }

  create() {
    this._router.navigate(['account/create']);
  }
}

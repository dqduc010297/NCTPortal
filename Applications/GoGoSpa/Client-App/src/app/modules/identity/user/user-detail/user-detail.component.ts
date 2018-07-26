import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";
import { UserDetail } from './UserDetail';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent implements OnInit {
  id: string;
  data: any = {};
  public userDetail = new UserDetail();
  public lStorage = localStorage.length;

  constructor(
    private _router: Router,
    private _route: ActivatedRoute,
    private _userService: UserService
  ) { }

  ngOnInit() {

    // TODO: Move all HTTPs request relate to user API into seperated service
    // You need to create UserService in ../../identity/user/user.service.ts
    //
    // then you call _userService.Get(this.id).subcrible(result=>{
    // })
    //
    // httpOptions, API url... will be managed by API service
    this.id = this._route.snapshot.paramMap.get('id');
    this._userService.getFormData(this.id).subscribe(result => {
      this.data = result;
      this.userDetail = this.data;
    });
  }

  update(id) {
    this._router.navigate(['account/edit', id])
  }

  pageBack() {
    this._router.navigate(['account']);
  }

}

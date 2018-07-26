import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserListComponent } from './user-list/user-list.component';
import { FormsModule } from '@angular/forms';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { UserCreateComponent } from './user-create/user-create.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { GridModule } from '@progress/kendo-angular-grid';
import { RouterModule } from '@angular/router';
import { ComboBoxModule, DropDownsModule } from '@progress/kendo-angular-dropdowns';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    GridModule,
    RouterModule,
    ComboBoxModule,
    DropDownsModule,
  ],
  declarations: [UserListComponent, UserDetailComponent, UserCreateComponent, UserEditComponent]
})
export class UserModule { }

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './modules/account/login/login.component';
import { LayoutComponent } from './layout/layout.component';
import { HomeComponent } from './home/home.component';
import { UserListComponent } from './modules/identity/user/user-list/user-list.component';
import { UserProfileComponent } from './modules/user-profile/my-profile/user-profile.component';
import { FormsModule } from '@angular/forms';
import { UserDetailComponent } from './modules/identity/user/user-detail/user-detail.component';
import { UserCreateComponent } from './modules/identity/user/user-create/user-create.component';
import { UserEditComponent } from './modules/identity/user/user-edit/user-edit.component';
import { UserProfileEditComponent } from './modules/user-profile/user-profile-edit/user-profile-edit.component';
import { AuthGuardService as AuthGuard } from './shared/services/authservices/auth-guard.service';
import { LoginGuardService as LoginGuard } from './shared/services/authservices/login-guard.service';
import { RoleGuardService as RoleGuard } from './shared/services/roleguardservice/role-guard.service';

import { RequestModule } from './modules/request/request.module';
import { NotfoundComponent } from './modules/notfound/notfound.component';
import { AccessDeniedComponent } from './modules/access-denied/access-denied.component';
import { ScheduleModule } from './modules/schedule/schedule.module';


const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: '', component: LayoutComponent, children: [
      { path: 'home', component: HomeComponent },
      { path: 'schedule', loadChildren: () => ScheduleModule }, // Remove LazyLoad because current version of angular-cli not support mixing / nested routing https://github.com/angular/angular-cli/issues/9651, https://github.com/angular/angular-cli/issues/9488

    ]
  },
  { path: '404', component: NotfoundComponent },
  { path: '401', component: AccessDeniedComponent },
  { path: '**', redirectTo: '/404' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), FormsModule],
  exports: [
    RouterModule
  ],
  declarations: []
})
export class AppRoutingModule { }

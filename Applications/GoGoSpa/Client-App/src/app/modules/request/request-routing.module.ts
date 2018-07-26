import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RequestListComponent } from './request-list/request-list.component';
import { RequestFormComponent } from './request-form/request-form.component';
import { RoleGuardService as RoleGuard } from '../.././shared/services/roleguardservice/role-guard.service';

const routes: Routes = [
  { path: 'list', component: RequestListComponent },
  //{ path: 'form/update/:id', component: RequestFormComponent, canActivate: [RoleGuard], data: { expectedRole: ['Customer'] } }, // View / Edit
  { path: 'form/:mode/:id', component: RequestFormComponent }, // View / Edit
  { path: 'form/:mode', component: RequestFormComponent, canActivate: [RoleGuard], data: { expectedRole: ['Customer'] } }      // Create
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  declarations: []
})
export class RequestRoutingModule { }

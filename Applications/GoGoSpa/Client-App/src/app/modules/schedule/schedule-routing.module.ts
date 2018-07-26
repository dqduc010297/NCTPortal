import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ScheduleFormComponent } from './schedule-form/schedule-form.component';


const routes: Routes = [
  { path: '', component: ScheduleFormComponent }
];


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    
  ]
})
export class ScheduleRoutingModule { }

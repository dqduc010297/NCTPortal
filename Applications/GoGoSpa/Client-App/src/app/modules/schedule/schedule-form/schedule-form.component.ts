import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-schedule-form',
  templateUrl: './schedule-form.component.html',
  styleUrls: ['./schedule-form.component.scss']
})
export class ScheduleFormComponent implements OnInit {

  eventTime = [ '1', '2', '3', '4', '5', '6', '7', '8', '9', '10'];
  nameEvent = ['2', '3', '4', '5', '6', '7'];
  constructor() { }

  ngOnInit() {
    console.log(this.eventTime);
  }
  onClick() {
    console.log('ABC');
  }
}

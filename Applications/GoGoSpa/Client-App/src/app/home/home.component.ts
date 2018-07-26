import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharingService } from '../shared/sevices/sharing-service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public userName: string;

  constructor(
    private route: Router,
    private shareingService: SharingService
  ) { }

  ngOnInit() {
    var name = this.shareingService.getName();
    this.userName = name;
  }

}

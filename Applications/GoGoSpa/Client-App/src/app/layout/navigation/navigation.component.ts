import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharingService } from '../../shared/sevices/sharing-service.service';
import * as toastr from 'toastr';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {
  public isRole: string;

  isCoordinorOrDriver: boolean;

  constructor(
    private router: Router,
    private shareingService: SharingService
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
    var role = this.shareingService.getRole();

    this.isRole = role;
  }

  displayToastr() {
    toastr["info"]('This feature is under construction!');
  }
  openNav() {
    document.getElementById("mySidenav").style.width = "250px";
  }
  closeNav() {
    document.getElementById("mySidenav").style.width = "0";
  }
}

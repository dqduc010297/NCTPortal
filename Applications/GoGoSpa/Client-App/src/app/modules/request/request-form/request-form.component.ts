import { Component, OnInit } from '@angular/core';
import { FormBaseComponent, FormValidationService } from '../../../shared/component/form';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationService } from '../../../shared/component/dialog/notification.service';
import { RequestService } from '../request.service';
import { DatePipe } from '@angular/common';
import { Observable, BehaviorSubject } from 'rxjs';
import { SharingService } from '../../../shared/sevices/sharing-service.service';
import { RequestStatus } from '../../../shared/models/utilities';


@Component({
  selector: 'app-request-form',
  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.scss']
})

export class RequestFormComponent extends FormBaseComponent implements OnInit {
  public warehouseList: Array<any> = [];
  public vehicleFeatureList: Array<any> = [];
  public requestStatus: string = '';
  private isCustomer: boolean = false;
  private isCoordinator: boolean = false;
  public addDelivery: BehaviorSubject<string> = new BehaviorSubject<string>(this.formData.address);
  public message: string = '';
  public isRoute: boolean;
  public requestStatusGeneral: RequestStatus = new RequestStatus();

  constructor(protected route: ActivatedRoute,
    protected router: Router,
    protected requestService: RequestService,
    protected notificationService: NotificationService,
    protected validationService: FormValidationService,
    private _notificationService: NotificationService,
    private _sharingService: SharingService
  ) {
    super(route, router, notificationService, requestService, validationService);
    this.resetFormData = (formMode) => { this.resetData(formMode) };
    if (this._sharingService.getRole() == "Customer") {
      this.isCustomer = true;
      this.isCoordinator = false;
    }
    else if (this._sharingService.getRole() == "Coordinator") {
      this.isCustomer = false;
      this.isCoordinator = true;
    }
    this.canAccess = this.canAccessUpdate;
    this.formConfiguration.events.onAfterInitFormData = (data) => {
      this.onBeforeInitFormData(data);
    };
    super.formOnInit("Request", {});
    this.isRoute = false;
  }

  public canAccessUpdate(formMode) {
    if (formMode == 'update') {
      if (!this.isCustomer) {
        return false;
      }
      else {
        if (this.formData.status != this.requestStatusGeneral.Inactive && this.formData.status != this.requestStatusGeneral.Rejected) {
          return false;
        }
        return true;
      }
    }
    else {
      return true;
    }
  }

  public onClickStatus(requestId, status) {
    if (status == this.requestStatusGeneral.Accepted || status == this.requestStatusGeneral.Rejected || status == this.requestStatusGeneral.Sending || status == this.requestStatusGeneral.Inactive) {
      console.log(status);
      this.requestService.changeStatus(requestId, status).subscribe(
        result => {
          console.log(result);
          this.requestStatus = result.result;
          this.refreshForm();
        });
    }
  }

  public resetData(data) {
    this.formData.wareHouse = '';
    this.formData.vehicleFeature = '';
    this.requestStatus = '';
  }

  public onBeforeInitFormData(data) {
    // Get status
    this.requestStatus = this.getRequestStatus(data.id);
    // Date parse
    if (data.pickingDate == null || data.pickingDate == undefined || data.expectedDate == null || data.expectedDate == undefined) {
      data.pickingDate = new Date();
      data.expectedDate = new Date();
    }
    else {
      data.pickingDate = new Date(data.pickingDate);
      data.expectedDate = new Date(data.expectedDate);
    }

    // Push warehouse to warehouse list so that it can show in combobox
    this.warehouseList = [];
    this.vehicleFeatureList = [];
    this.warehouseList.push(data.wareHouse);
    this.vehicleFeatureList.push(data.vehicleFeature);
  }

  public getRequestStatus(requestId: any) {
    if (requestId != null && requestId != undefined) {
      this.requestService.getRequestStatus(requestId).subscribe(data => {
        if (data != null && data != undefined) {
          this.requestStatus = data.result;
          if (this.requestStatus == null || this.requestStatus == undefined) {
            this.requestStatus = this.formData.status;
          }
        }
      });
    }
    return this.requestStatus;
  }

  public filterWarehouse(value) {
    if (value != null && value != undefined && value != '') {
      this.requestService.filterWarehouse(value).subscribe(data => {
        if (data != null && data != undefined) {
          this.warehouseList = data;
        }
      });

    }
  }

  public filterVehicleFeature(value) {
    if (value != null && value != undefined && value != '') {
      this.requestService.filterVehicleFeature(value).subscribe(data => {
        if (data != null && data != undefined) {
          this.vehicleFeatureList = data;
        }
      });

    }
  }

  public onChangeAddress(data) {
    console.log(1);
    this.addDelivery.next(data);
    console.log(this.addDelivery)
  }

  public onAddressDeliveryLocated(location) {
    console.log(location);
    this.formData.deliveryLatitude = location.lat;
    this.formData.deliveryLongitude = location.lng;
  }

  //public onSubmitForm(mainForm) {
  //  if (this.formData.pickingDate < Date.now) {
  //    alert('Picking Date can not less than now');
  //  } else if (this.formData.expectedDate < Date.now) {
  //    alert('Expected Date can not less than now');
  //  }
  //  else if (this.formData.expectedDate < this.formData.pickingDate) {
  //    alert('Expected Date can not less than Picking Date');
  //  }
  //  super.onSubmitForm(mainForm);
  //}
  ngOnInit() {
    if (this.formData.address != null && this.formData != undefined) {

    }
  }

}

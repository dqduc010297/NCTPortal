import { Injectable } from '@angular/core';

declare let toastr: any;
@Injectable()
export class NotificationService {
  public prompError(message: string, title: string = "Error message") {
    if (typeof (toastr) !== typeof (undefined)) {
      toastr.error(message, title);
    } else {
      alert(message);
    }
  }
}

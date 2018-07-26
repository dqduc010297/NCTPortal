import { Injectable } from '@angular/core';
import { ICreateFormService, IViewFormService, IUpdateFormService, IDataSourceService } from '../../shared/component/form';
import { Observable, observable } from 'rxjs';
import { AuthHttpService } from '../../shared';
import { HttpClient } from '@angular/common/http';
import { DataSourceRequestState, DataResult, toDataSourceRequestString, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { GridDataResult } from '@progress/kendo-angular-grid';
import 'rxjs/add/operator/map'
import { StringObject } from '../../shared/models/utilities';
@Injectable({
  providedIn: 'root'
})
export class RequestService  implements ICreateFormService, IViewFormService, IUpdateFormService, IDataSourceService  {

  getRequestStatus(requestId: any): Observable<any> {
    return this._apiHttp.get(`/api/shipmentrequest/${requestId}/status`);
  }

  changeStatus(requestId: string, status: string): Observable<any> {
    var stringObject: StringObject = { content: status };
    return this._apiHttp.put(`/api/requests/${requestId}/status`, stringObject);
  }

  getDataSource(): Observable<any> { 
    return this._apiHttp.get(``);
  }
  
  filterWarehouse(displayName: string): Observable<any>{
    return this._apiHttp.get(`/api/masterdata/warehouses/datasource/${displayName}`);
  }

  filterVehicleFeature(displayName: string): Observable<any> {
    return this._apiHttp.get(`/api/masterdata/vehiclefeatures/datasource/${displayName}`);
  }

  edit(id: any, formData: any): Observable<any> {
    formData.warehouseId = formData.wareHouse.value;
    return this._apiHttp.put(`/api/requests/${id}`, formData);
  }

  getFormData(id: any): Observable<any> { // for view
    return this._apiHttp.get(`/api/requests/${id}`);
  }

  create(formData: any): Observable<any> {
    return this._apiHttp.post(`/api/requests`, formData);
  }

  getViewFormUrl(id: any): string {
    return `/request/form/view/${id}`;
  }

  getListPageUrl(): string {
    return `/request/list`;
  }

  fetch(state: DataSourceRequestState): Observable<DataResult> {
    const queryStr = `${toDataSourceRequestString(state)}`; // Serialize the state
    const hasGroups = state.group && state.group.length;

    return this._apiHttp
      .get(`/api/requests/?${queryStr}`) 
      .map(({ data, total}: GridDataResult) => 
        (<GridDataResult>{
          data: hasGroups ? translateDataSourceResultGroups(data) : data,
          total: total,
        })
      )
  }

  constructor(private _apiHttp: AuthHttpService) { }
}

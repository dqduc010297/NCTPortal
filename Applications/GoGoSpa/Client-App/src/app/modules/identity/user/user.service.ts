import { Injectable } from '@angular/core';
import {
  toDataSourceRequestString,
  translateDataSourceResultGroups,
  translateAggregateResults,
  DataResult,
  DataSourceRequestState
} from '@progress/kendo-data-query';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { AuthHttpService } from 'src/app/shared';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  public lStorage = localStorage.length;

  constructor(
    private _apiHttp: AuthHttpService
  ) { }

  public fetch(state: DataSourceRequestState): Observable<DataResult> {
    const queryStr = `${toDataSourceRequestString(state)}`; // Serialize the state
    const hasGroups = state.group && state.group.length;

    return this._apiHttp
      .get(`/api/user?${queryStr}`) // Send the state to the server
      .map(({ data, total/*, aggregateResults*/ }: GridDataResult) => // Process the response
        (<GridDataResult>{
          // If there are groups, convert them to a compatible format
          data: hasGroups ? translateDataSourceResultGroups(data) : data,
          total: total,
          // Convert the aggregates if such exist
          //aggregateResult: translateAggregateResults(aggregateResults)
        })
      )
  }

  edit(id: any, formData: any): Observable<any> {
    return this._apiHttp.put(`/api/user/${id}`, formData);
  }

  getFormData(id: any): Observable<any> {
    return this._apiHttp.get(`/api/user/${id}`);
  }

  create(formData: any): Observable<any> {
    return this._apiHttp.post(`/api/user`, formData);
  }
}

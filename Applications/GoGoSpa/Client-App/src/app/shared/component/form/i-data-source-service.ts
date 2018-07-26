import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface IDataSourceService {
  getDataSource(): Observable<any>;
}

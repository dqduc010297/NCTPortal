import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface IViewFormService {
  getFormData(id: any): Observable<any>;
  getListPageUrl(): string;
}

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface ICreateFormService {
  create(formData: any): Observable<any>;
  getViewFormUrl(id: any): string;
  getListPageUrl(): string;
}

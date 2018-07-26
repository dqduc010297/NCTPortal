import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface IUpdateFormService {
  edit(id: any, formData: any): Observable<any>;
  getFormData(id: any): Observable<any>;
  getViewFormUrl(id: any): string;
  getListPageUrl(): string;
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SaveService {
  private code: any;
  private currentRequest: string;

  constructor(public http: HttpClient) { }


  saveCode(code: any) {
    this.code = code;
  }
  getCode() {
    return this.code;
  }
  saveCurrentRequest(code: string) {
    this.currentRequest = code;
  }
  getCurrentRequest() {
    return this.currentRequest;
  }


}


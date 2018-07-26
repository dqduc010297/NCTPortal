import { Injectable } from '@angular/core';

@Injectable()
export class LocalStorageService {
  constructor() { }

  public get(key: string): any {
    return localStorage.getItem(key);
  }

  public set(key: string, value: any) {
    localStorage.setItem(key, value);
  }
}

import { Injectable } from '@angular/core';
import { LocalStorageService } from './local-storage.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ServiceRegistry } from 'src/app/shared/models/service-registry';

@Injectable()
export class ServiceRegistryService {
  // TODO: Set ConfigurationService as a sigleton
  public registry: ServiceRegistry = null;


  constructor(private http: HttpClient, private storageService: LocalStorageService) {

  }


  load(configurationUrl: string): Observable<ServiceRegistry> {
    return new Observable(observer => {

      // Implement singleton
      if (this.registry !== null) {
        observer.next(this.registry);
        observer.complete();
      }

      // Fetch registry
      this.http.get(configurationUrl).subscribe((response: any) => {
        this.registry = new ServiceRegistry();
        this.registry.apiUrl = response.api;
        observer.next(this.registry);
        observer.complete();

      });

    });
  }
}

import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { ConfigService } from './config-service.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AuthHttpService } from 'src/app/shared';

@Injectable({
  providedIn: 'root'
})
export class MasterDataService extends BehaviorSubject<any> {
  private baseUrl = '';
  constructor(private http: Http, private configService: ConfigService, private https: AuthHttpService) {
    super(null);

  }


  ///api/Vehicles filter Api
  public getVehicleDataSouce(licensePlate): Observable<any>{
    return this.https.get(`/api/Vehicles?licensePlate=${licensePlate}`);
  }

  public getVehicleDetail(id: string): Observable<any> {
    
    return this.https.get(`/api/Vehicles/${id}`);
  }

  //Driver filter Api
 
  public getDriverDataSource(driverName): Observable<any> {
    return this.https.get(`/api/MasterData/Drivers?driverName=${driverName}`);
  }

  public getDriverDetail(id: string): Observable<any> {
 
    return this.https.get(`/api/MasterData/Drivers/${id}`) ;
  }

  //Warehouse filter Api
  public getWarehouseDataSource(value): Observable<any> {
    return this.https.get(`/api/MasterData/Warehouses?value=${value}`);
  }

  public getWarehouseDetail(id: string): Observable<any> {
   
    return this.https.get(`/api/MasterData/Warehouses/${id}`);
  }


}

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable} from 'rxjs';
import { tap, map} from'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private http : HttpClient) { }
  getFeatures() : Observable<any[]>
  {
    return this.http.get<any[]>("/api/features").pipe(tap(res=> JSON.stringify(res)));
  }
  getMakes() : Observable<any[]>
  {
    return this.http.get<any[]>("/api/makes").pipe(tap(data=>JSON.stringify(data)));
  }
  create(vehicle){
    
      return this.http.post<any>("/api/vehicles", vehicle);   
  }

}

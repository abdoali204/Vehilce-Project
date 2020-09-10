import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable} from 'rxjs';
import { tap, map} from'rxjs/operators';
import { SaveVehicle, Vehicle } from '../Models/Vehicle';
@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  private readonly vehiclesEndPoint : string = '/api/vehicles';
  constructor(private http : HttpClient) { }
  getFeatures() : Observable<any[]>
  {
    return this.http.get<any[]>("/api/features").pipe(tap(res=> JSON.stringify(res)));
  }
  getMakes() : Observable<any[]>
  {
    return this.http.get<any[]>("/api/makes").pipe(tap(data=>JSON.stringify(data)));
  }
    create(vehicle) {

        return this.http.post(this.vehiclesEndPoint,vehicle).pipe(map(res => JSON.stringify(res)));   
  }
  getVehicle(id) : Observable<Vehicle>
  {
       return this.http.get<Vehicle>(this.vehiclesEndPoint + '/'+ id);
  }
  update(vehicle : SaveVehicle)
  {
      return this.http.put(this.vehiclesEndPoint + '/'+ vehicle.id, vehicle);
  }
  delete(id : number)
  {
    return this.http.delete(this.vehiclesEndPoint+'/'+ id);
  }
  getVehicles(filter) : Observable<any[]>
  {

    return this.http.get<any[]>(this.vehiclesEndPoint+ '?'+ this.toQueryString(filter)).pipe(tap(data=> JSON.stringify(data)));
  }
  toQueryString(obj)
  {
    var parts= [];
    for(var prop in obj){
      var value = obj[prop];
      if(value != null && value != undefined)
        parts.push(encodeURIComponent(prop)+ '=' +  encodeURIComponent(value));
    }
    return parts.join('&');
  }

}

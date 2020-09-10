import * as _ from 'underscore';

import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { forkJoin} from 'rxjs';
import { Vehicle, SaveVehicle } from '../Models/Vehicle';
@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes : any[];
  vehicle : SaveVehicle = {
    id : 0,
    makeId : 0,
    modelId : 0,
    isRegistered : false,
    features : [],
    contact : {
      name : '',
      email: '',
      phone: '',
    }
  };
  models: any[];
  features : any[];
  constructor(private vehicleService : VehicleService
              ,private router : Router
              ,private route : ActivatedRoute) {
                //getting route params
                route.params.subscribe(p=>{
                  this.vehicle.id =  +p['id'] || 0;
                })
               }

  ngOnInit() {
      var sources  : any[]= [
        this.vehicleService.getMakes(),
        this.vehicleService.getFeatures()
      ];
      if(this.vehicle.id)
        sources.push(this.vehicleService.getVehicle(this.vehicle.id));
      forkJoin(sources).subscribe((data : any[])=> {
        console.log(data);
        this.makes = data[0];
        this.features = data[1];

          if(this.vehicle.id)
          {
            this.setVehicle(data[2]);
            this.populateModels();
          }
      },err=> {
        if(err.status == 404)
        {
          this.router.navigate(['']);            
        }
      });
      //this.vehicleService.getVehicle(this.vehicle.id)
 //       .subscribe(v => {this.vehicle = v;},err=> {
  //
  //      });
   //   this.vehicleService.getMakes().subscribe(makes =>
  //        this.makes = makes
  //    );
  //    this.vehicleService.getFeatures().subscribe(features => this.features = features);
  }
  onMakeChange()
  {
      console.log("VEHICLE",this.vehicle);
      this.populateModels();
      delete this.vehicle.modelId;

  }
  private populateModels()
  {
    var selectedMake = this.makes.find( m => m.id == this.vehicle.makeId);
    this.models = selectedMake? selectedMake.models: [];
  
  }
  onFeatureToggle(featureId,$event)
  {
      if($event.target.checked)
        this.vehicle.features.push(featureId);
      else
      {
        var index = this.vehicle.features.indexOf(featureId);
        this.vehicle.features.splice(index,1);
      }
  }
  submit()
  {
    var result$ = (this.vehicle.id) ? this.vehicleService.update(this.vehicle) : this.vehicleService.create(this.vehicle);
    result$.subscribe(res=>
      {
        console.log("Data Was Saved Succesfully");
      });
      this.router.navigate(['/vehicles/', this.vehicle.id]);
  }
  setVehicle(v : Vehicle)
  {
    this.vehicle.id  = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    this.vehicle.features = _.pluck(v.features,'id');
  }
  delete()
  {
    if(confirm("Are you sure?"))
      this.vehicleService.delete(this.vehicle.id).subscribe(x=>{
        this.router.navigate(['']);
      });
  }
}

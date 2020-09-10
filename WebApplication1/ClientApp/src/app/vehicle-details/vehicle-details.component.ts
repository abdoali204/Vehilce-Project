import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Vehicle } from '../Models/Vehicle';
import { VehicleService } from '../services/vehicle.service';
import { PhotoService } from '../services/photo.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.css']
})
export class VehicleDetailsComponent implements OnInit {
  @ViewChild('fileInput',{static: false}) fileInput : ElementRef;
  vehicle : any;
  vehicleId : number;
  photos : any[]= [];
  constructor(private router : Router,
              private route : ActivatedRoute,
              private vehicleService : VehicleService,
              private photoService : PhotoService,
              private auth : AuthService) {
              this.route.params.subscribe(p=>
                {
                  this.vehicleId= +p['id'];
                  if(isNaN(this.vehicleId) || this.vehicleId <= 0)
                    router.navigate(['/vehicles']);
                    return;
                })
                

  }

  ngOnInit() {
    this.vehicleService.getVehicle(this.vehicleId)
    .subscribe(v => this.vehicle = v,
      err => {
        if(err.status == 404)
          this.router.navigate(['/vehicles']);
          return;
      }
      
    );
    this.getPhotos();
    
  }
  delete(){
    if(confirm("Are You Sure?"))
      this.vehicleService.delete(this.vehicleId)
      .subscribe(x=> {
        this.router.navigate(['/vehicles']);
      })
  }
    uploadPhoto()
  {
    var nativeElement : HTMLInputElement = this.fileInput.nativeElement;
    this.photoService.upload(this.vehicleId,nativeElement.files[0])
    .subscribe(photo=> this.photos.push(photo));
  }
  getPhotos()
  {
    this.photoService.getPhotos(this.vehicleId).subscribe(photos=>
    {
      this.photos = photos;
      console.log(this.photos);
    });
  }
  
}

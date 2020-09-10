import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { Vehicle } from '../Models/Vehicle';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
  queryResult : any = {};
  makes: any[];
  query : any = {
    pageSize : this.PAGE_SIZE
  };
  columns = [
    { title : 'ID'},
    { title : 'Make', key : 'make' , isSortable : true},
    { title : 'Model', key : 'model' , isSortable : true},
    { title : 'Contact Name', key : 'contactName' , isSortable : true},
    {}
  ]

  constructor(private vehicleService: VehicleService,
              private auth : AuthService) { }

  ngOnInit() {
    this.populateVehicles();
    this.vehicleService.getMakes().subscribe(makes=> this.makes = makes);
  }
  onFilterChange()
  {
    this.query.page = 1;
    this.query.pageSize = this.PAGE_SIZE;
    this.populateVehicles();
  }
  private populateVehicles(){
    this.vehicleService.getVehicles(this.query)
    .subscribe(result => {
      this.queryResult = result;
    });
  }
  resetFilters()
  {
    this.query = {
      page : 1,
      pageSize : this.PAGE_SIZE
    };
    this.populateVehicles();
  }
  sortBy(columnName){
    if(this.query.sortBy === columnName){
      this.query.isSortAscending = !this.query.isSortAscending;
    }else{
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateVehicles();
  }
  onPageChange(page)
  {
    this.query.page = page;
    this.populateVehicles()  
  }
}

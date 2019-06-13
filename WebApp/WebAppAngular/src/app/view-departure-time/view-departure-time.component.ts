import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MapService } from '../services/http/map.service';
import { JwtService } from '../services/jwt.service';

@Component({
  selector: 'app-view-departure-time',
  templateUrl: './view-departure-time.component.html',
  styleUrls: ['./view-departure-time.component.css'],
  providers: [MapService, JwtService]
})
export class ViewDepartureTimeComponent implements OnInit {

  routeNames: string[] = [];
  departureTimes: string[] = [];


  viewRoutesForm = this.fb.group({
    area: ['',
      [Validators.required]]
  });

  viewDepTimesForm = this.fb.group({
    id: ['',
      [Validators.required]],
    dayType: ['',
      [Validators.required]]
  });


  get vDTForm() { return this.viewDepTimesForm.controls; }

  constructor(private fb: FormBuilder, private router: Router, private mapService: MapService, private jwt: JwtService) { }

  ngOnInit() {
  }

  selectArea(name: string) {

    this.routeNames = [];

    this.mapService.getAllRoutes(name).subscribe(data => {
      console.log(data);

      data.forEach(element => {

        this.routeNames.push(element.Name);

      });
    });
  }

  viewDepTimes() {

    this.departureTimes = [];

    this.mapService.sendRoutForDepartureTimes(this.viewDepTimesForm.value).subscribe(data => {
      console.log(data);

      let times = data.split(",");
      times.forEach(element => {
        if(element != "") {
          this.departureTimes.push(element);
        }
      });
    });
  }
}

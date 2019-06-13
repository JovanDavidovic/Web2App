import { Component, OnInit } from '@angular/core';
import { JwtService } from '../services/jwt.service';
import { MapService } from '../services/http/map.service';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { GetDepartureTimeModel } from '../models/get-departure-time-model';
import { DeleteDepartureTimeModel } from '../models/delete-departure-time-model';

@Component({
  selector: 'app-delete-departure-time',
  templateUrl: './delete-departure-time.component.html',
  styleUrls: ['./delete-departure-time.component.css'],
  providers: [MapService, JwtService]
})
export class DeleteDepartureTimeComponent implements OnInit {

  routeNames: string[] = [];
  departureTimes: string[] = [];
  currentDayType: string;
  currentId: number;

  viewRoutesForm = this.fb.group({
    area: ['',
      [Validators.required]]
  });

  viewDepTimesForm = this.fb.group({
    id: ['',
      [Validators.required]],
    dayType: ['',
      [Validators.required]],
    time: ['',
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

    let model = new GetDepartureTimeModel();

    model.dayType = this.viewDepTimesForm.get('dayType').value;
    model.id = this.viewDepTimesForm.get('id').value;

    this.currentDayType = this.viewDepTimesForm.get('dayType').value;
    this.currentId = this.viewDepTimesForm.get('id').value;

    this.mapService.sendRoutForDepartureTimes(model).subscribe(data => {
      console.log(data);

      let times = data.split(",");
      times.forEach(element => {
        if(element != "") {
          this.departureTimes.push(element);
        }
      });
    });
  }

  selectTime(time: string){

    let model = new DeleteDepartureTimeModel();

    model.dayType = this.currentDayType;
    model.id = this.currentId;
    model.time = time;

    this.mapService.deleteDepartureTime(model).subscribe(data => {
      this.router.navigate(["home"]);
    });
  }

}

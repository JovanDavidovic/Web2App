import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MapService } from '../services/http/map.service';

@Component({
  selector: 'app-create-departure-time',
  templateUrl: './create-departure-time.component.html',
  styleUrls: ['./create-departure-time.component.css']
})
export class CreateDepartureTimeComponent implements OnInit {

  routeNames: string[] = [];


  createDepartureTtimeForm = this.fb.group({
    hour: ['',
      [Validators.required,
        Validators.min(0),
        Validators.max(23)]],
    min: ['',
      [Validators.required,
          Validators.min(0),
          Validators.max(59)]],
    dayType: [''],
    routeName: ['']
  })

  constructor(private fb: FormBuilder, private mapService: MapService, private router: Router) { }

  ngOnInit() {
    this.mapService.getAllRoutes().subscribe(data => {
      console.log(data);

      data.forEach(element => {

        this.routeNames.push(element.Name);

      });
      console.log(this.routeNames);
    });
  }

  createDT() {
    this.mapService.createDepartureTime(this.createDepartureTtimeForm.value).subscribe(data => {
      this.router.navigate(["home"]);
    });
  }
}

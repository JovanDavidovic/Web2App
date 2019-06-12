import { Component, OnInit, Input, NgZone } from '@angular/core';
import { MarkerInfo } from './model/marker-info.model';
import { GeoLocation } from './model/geolocation';
import { Polyline } from './model/polyline';
import { RouteModel } from '../models/route-model';
import { MapService } from '../services/http/map.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtService } from '../services/jwt.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}'], //postavljamo sirinu i visinu mape
  providers: [MapService, JwtService]
})
export class MapComponent implements OnInit {

  markerInfo: MarkerInfo;
  public polyline: Polyline;
  public zoom: number;
  stations: RouteModel = new RouteModel();

  addNameForm = this.fb.group({
    name: ['',
      [Validators.required]]
  });

  get routeForm() { return this.addNameForm.controls; }

  ngOnInit() {
    this.markerInfo = new MarkerInfo(new GeoLocation(45.242268, 19.842954),
      "assets/ftn.png",
      "Jugodrvo", "", "http://ftn.uns.ac.rs/691618389/fakultet-tehnickih-nauka");

    this.polyline = new Polyline([], 'blue', { url: "assets/busicon.png", scaledSize: { width: 50, height: 50 } });
  }

  constructor(private ngZone: NgZone, private fb: FormBuilder, private router: Router, private mapService: MapService, private jwt: JwtService) {
  }

  placeMarker($event) {
    if (this.jwt.getRole() == 'Admin') {
      this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
      this.stations.routeStations += "-" + $event.coords.lat.toString() + ":" + $event.coords.lng.toString();
      console.log(this.polyline)
    }
  }

  createRoute() {
    this.stations.name = this.addNameForm.get('name').value;
    this.mapService.createRoute(this.stations).subscribe(data => {
      console.log("poslata ruta");
    });
    this.router.navigate(["home"]);
  }
}

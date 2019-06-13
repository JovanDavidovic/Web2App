import { Component, OnInit, Input, NgZone } from '@angular/core';
import { JwtService } from '../services/jwt.service';
import { MapService } from '../services/http/map.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { Polyline } from '../map/model/polyline';
import { GeoLocation } from '../map/model/geolocation';
import { MarkerInfo } from '../map/model/marker-info.model';

@Component({
  selector: 'app-delete-route',
  templateUrl: './delete-route.component.html',
  styleUrls: ['./delete-route.component.css'],
  providers: [MapService, JwtService]
})
export class DeleteRouteComponent implements OnInit {

  
  markerInfo: MarkerInfo;
  public polyline: Polyline;
  public zoom: number;
  routeNames: string[] = [];
  routeName: string;
  tmp: number;

  selectRouteForm = this.fb.group({
    name: ['']
  });

  constructor(private ngZone: NgZone, private fb: FormBuilder, private router: Router, private mapService: MapService, private jwt: JwtService) { }

  ngOnInit() {
    this.markerInfo = new MarkerInfo(new GeoLocation(45.242268, 19.842954),
      "assets/ftn.png",
      "Jugodrvo", "", "http://ftn.uns.ac.rs/691618389/fakultet-tehnickih-nauka");

    this.polyline = new Polyline([], 'blue', { url: "assets/busicon.png", scaledSize: { width: 50, height: 50 } });
  
    this.mapService.getAllRoutes("Urban").subscribe(data => {
      console.log(data);

      data.forEach(element => {

        this.routeNames.push(element.Name);

      });
    });

    this.mapService.getAllRoutes("Suburban").subscribe(data => {
      console.log(data);

      data.forEach(element => {

        this.routeNames.push(element.Name);

      });
    });
  }

  selectRoute(name: number) {
    console.log(name);
    this.tmp = name
    this.mapService.sendSelectedRoute(name).subscribe(data => {
      console.log(data);

      this.polyline = new Polyline([], 'blue', { url: "assets/busicon.png", scaledSize: { width: 50, height: 50 } });

      let coordinates = data.RouteStations.split("-");
      coordinates.forEach(element => {
        if(element != "") {
          this.polyline.addLocation(new GeoLocation(+(element.split(":")[0]), +(element.split(":")[1])));
        }
      });
      
    });
  }

  deleteRoute() {
    console.log(this.tmp);
    this.mapService.deleteRoute(this.tmp).subscribe(data => {
      this.router.navigate(["home"]);
    });
  }
}

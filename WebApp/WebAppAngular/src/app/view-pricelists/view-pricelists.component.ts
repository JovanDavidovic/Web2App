import { Component, OnInit } from '@angular/core';
import { PricelistService } from '../services/http/pricelist.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { PricelistModel } from '../models/pricelist-model';

@Component({
  selector: 'app-view-pricelists',
  templateUrl: './view-pricelists.component.html',
  styleUrls: ['./view-pricelists.component.css'],
  providers: [PricelistService]
})
export class ViewPricelistsComponent implements OnInit {

  pricelists: PricelistModel[] = [];

  constructor(private fb: FormBuilder, private vp: PricelistService, private router: Router) { }

  ngOnInit() {
    this.vp.getAllPricelists().subscribe(data => {
      console.log(data);

      data.forEach(element => {

        this.pricelists.push({ from: element.From, to: element.To, hour: element.Hour, day: element.Day, month: element.Month, year: element.Year });

      });
    });
  }

}

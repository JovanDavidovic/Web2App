import { Component, OnInit } from '@angular/core';
import { PricelistService } from '../services/http/pricelist.service';
import { PricelistModel } from '../models/pricelist-model';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { PricelistModificationService } from '../services/pricelist-modification.service';

@Component({
  selector: 'app-view-future-pricelists',
  templateUrl: './view-future-pricelists.component.html',
  styleUrls: ['./view-future-pricelists.component.css'],
  providers: [PricelistService, PricelistModificationService]
})
export class ViewFuturePricelistsComponent implements OnInit {

  futurePricelists: PricelistModel[] = [];
  selectedFuturePricelist: PricelistModel;

  constructor(private fb: FormBuilder, private vp: PricelistService, private router: Router, private pserv: PricelistModificationService) { }

  ngOnInit() {
    this.vp.getAllFuturePricelists().subscribe(data => {
      console.log(data);

      data.forEach(element => {

        this.futurePricelists.push({ from: element.From, to: element.To, hour: element.Hour, day: element.Day, month: element.Month, year: element.Year });

      });
    });
  }

  onSelect(fpl: PricelistModel): void {
    console.log("selected");

    this.selectedFuturePricelist = fpl;

    localStorage.modify = fpl.from;

    this.router.navigate(["modifyPricelist"]);
  }

}

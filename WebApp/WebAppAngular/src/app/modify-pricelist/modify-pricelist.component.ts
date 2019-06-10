import { Component, OnInit } from '@angular/core';
import { PricelistService } from '../services/http/pricelist.service';
import { Validators, FormBuilder } from '@angular/forms';
import { PricelistModificationService } from '../services/pricelist-modification.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-modify-pricelist',
  templateUrl: './modify-pricelist.component.html',
  styleUrls: ['./modify-pricelist.component.css'],
  providers: [PricelistService, PricelistModificationService]
})
export class ModifyPricelistComponent implements OnInit {

  pricelistForm = this.fb.group({
    id: [''],
    from: ['',
      [Validators.required]],
    to: ['',
      [Validators.required]],
    hour: ['',
      [Validators.required]],
    day: ['',
      [Validators.required]],
    month: ['',
      Validators.required],
    year: ['',
      Validators.required],
  });

  get prcForm() { return this.pricelistForm.controls; }


  constructor(private fb: FormBuilder, private prc: PricelistService, private router: Router, private pserv: PricelistModificationService) { }

  ngOnInit() {


    this.prc.getPricelistByDate(localStorage.getItem("modify")).subscribe(data => {
      console.log(data);

      this.pricelistForm.patchValue({ id: data.Id});
      this.pricelistForm.patchValue({ from: data.From });
      this.pricelistForm.patchValue({ to: data.To });
      this.pricelistForm.patchValue({ hour: data.Hour });
      this.pricelistForm.patchValue({ day: data.Day });
      this.pricelistForm.patchValue({ month: data.Month });
      this.pricelistForm.patchValue({ year: data.Year });
    });


  }

  modifyPricelist() {

    this.prc.modifyPricelist(this.pricelistForm.value).subscribe(data => {
      localStorage.modify = undefined;
      this.router.navigate(["home"]);
    },
      err => {
        console.log(err);
        console.log("AAAAAAAAAAAAAAAA");
        this.router.navigate(["errorPricelist"]);
      })
  }

}

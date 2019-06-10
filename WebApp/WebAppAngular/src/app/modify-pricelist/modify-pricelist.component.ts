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

  constructor(private fb: FormBuilder, private prc: PricelistService, private router: Router, private pserv: PricelistModificationService) { }

  ngOnInit() {
    let data = localStorage.getItem("modify");
    console.log(data);

    this.prc.getPricelistByDate(localStorage.getItem("modify")).subscribe(data => {
      this.pricelistForm.patchValue({ from: data.from });
      this.pricelistForm.patchValue({ to: data.to });
      this.pricelistForm.patchValue({ hour: data.hour });
      this.pricelistForm.patchValue({ day: data.day });
      this.pricelistForm.patchValue({ month: data.month });
      this.pricelistForm.patchValue({ year: data.year });
    });


  }

  formPricelist() {

    this.prc.formPricelist(this.pricelistForm.value).subscribe(data => {
      this.router.navigate(["home"]);
    },
      err => {
        console.log(err);
      })
  }

}

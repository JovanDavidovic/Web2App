import { Component, OnInit } from '@angular/core';
import { PricelistService } from '../services/http/pricelist.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pricelist',
  templateUrl: './pricelist.component.html',
  styleUrls: ['./pricelist.component.css'],
  providers: [PricelistService]
})
export class PricelistComponent implements OnInit {

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

  get prcForm() { return this.pricelistForm.controls; }

  constructor(private fb: FormBuilder, private prc: PricelistService, private router: Router) { }

  ngOnInit() {
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

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
    hourPrice: ['',
      [Validators.required]],
    dayPrice: ['',
      [Validators.required]],
    monthPrice: ['',
      Validators.required],
    yearPrice: ['',
      Validators.required],
  });

  get prcForm() { return this.pricelistForm.controls; }

  constructor(private fb: FormBuilder, private prc: PricelistService, private router: Router) { }

  ngOnInit() {
  }

  formPricelist() {

    this.prc.register(this.pricelistForm.value).subscribe(data => {
      
    },
      err => {
        console.log(err);
      })
  }
}

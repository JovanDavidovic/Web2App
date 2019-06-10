import { Component, OnInit } from '@angular/core';
import { BuyTicketService } from '../services/http/buy-ticket.service';
import { JwtService } from '../services/jwt.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { BuyTicketModel } from '../models/buy-ticket-model';

@Component({
  selector: 'app-buy-ticket',
  templateUrl: './buy-ticket.component.html',
  styleUrls: ['./buy-ticket.component.css'],
  providers: [BuyTicketService, JwtService]
})
export class BuyTicketComponent implements OnInit {

  prices: BuyTicketModel[] = [];
  selectedPrice: BuyTicketModel = new BuyTicketModel();

  constructor(private fb: FormBuilder, private bt: BuyTicketService, private router: Router, private jwt: JwtService) { }


  ngOnInit() {
    this.bt.getPricelist(localStorage.getItem('name')).subscribe(data => {
      console.log(data);

      this.selectedPrice.hour = data.Hour;
      this.selectedPrice.day = data.Day;
      this.selectedPrice.month = data.Month;
      this.selectedPrice.year = data.Year;
    });
  }

}

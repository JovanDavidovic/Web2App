import { Component, OnInit } from '@angular/core';
import { BuyTicketService } from '../services/http/buy-ticket.service';
import { JwtService } from '../services/jwt.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { BuyTicketModel } from '../models/buy-ticket-model';
import { TicketPriceModel } from '../models/ticket-price-model';

@Component({
  selector: 'app-buy-ticket',
  templateUrl: './buy-ticket.component.html',
  styleUrls: ['./buy-ticket.component.css'],
  providers: [BuyTicketService, JwtService]
})
export class BuyTicketComponent implements OnInit {

  prices: TicketPriceModel[] = [];
  selectedPrice: TicketPriceModel = new TicketPriceModel();

  constructor(private fb: FormBuilder, private bt: BuyTicketService, private router: Router, private jwt: JwtService) { }


  ngOnInit() {
    this.bt.getPricelist(localStorage.getItem('name')).subscribe(data => {
      console.log(data);

      this.prices.push({ticketType: "Hour", price: data.Hour});
      this.prices.push({ticketType: "Day", price: data.Day});
      this.prices.push({ticketType: "Month", price: data.Month});
      this.prices.push({ticketType: "Year", price: data.Year});
    });
  }

  onSelect(tp: TicketPriceModel): void {
    console.log("selected");

    this.selectedPrice = tp;

    this.router.navigate(["home"]);
  }
}

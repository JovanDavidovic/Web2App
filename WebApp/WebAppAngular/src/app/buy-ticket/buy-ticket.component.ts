import { Component, OnInit } from '@angular/core';
import { BuyTicketService } from '../services/http/buy-ticket.service';
import { JwtService } from '../services/jwt.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { BuyTicketModel } from '../models/buy-ticket-model';
import { TicketPriceModel } from '../models/ticket-price-model';
import { ValidateUsersService } from '../services/http/validate-users.service';
import { BoughtTicketModel } from '../models/bought-ticket-model';

@Component({
  selector: 'app-buy-ticket',
  templateUrl: './buy-ticket.component.html',
  styleUrls: ['./buy-ticket.component.css'],
  providers: [BuyTicketService, JwtService, ValidateUsersService]
})
export class BuyTicketComponent implements OnInit {

  prices: TicketPriceModel[] = [];
  selectedPrice: TicketPriceModel = new TicketPriceModel();

  constructor(private fb: FormBuilder, private bt: BuyTicketService, private vu: ValidateUsersService, private router: Router, private jwt: JwtService) { }


  ngOnInit() {
    this.bt.getPricelist(localStorage.getItem('name')).subscribe(data => {
      console.log(data);

      this.prices.push({ ticketType: "Hour", price: data.Hour });
      if(this.jwt.isTokenValid()) {
        this.prices.push({ ticketType: "Day", price: data.Day });
        this.prices.push({ ticketType: "Month", price: data.Month });
        this.prices.push({ ticketType: "Year", price: data.Year });
      }
    });
  }

  onSelect(tp: TicketPriceModel): void {

    if (localStorage.getItem('name') == "undefined" || localStorage.getItem('name') == null) {
      console.log("not registered");
      this.router.navigate(["inputMail"]);
    }
    else {
      this.vu.getUserByUsername(localStorage.getItem('name')).subscribe(data => {
        console.log("registered");

        let model = new BoughtTicketModel();
        model.username = localStorage.getItem('name');
        model.ticketType = tp.ticketType;
        model.price = tp.price;

        this.bt.sendTicket(model).subscribe(data2 => {
          this.router.navigate(["home"]);
        });
      },
        err => {
          console.log("not registered");
          this.router.navigate(["inputMail"]);
        });
    }
  }
}

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BuyTicketModel } from 'src/app/models/buy-ticket-model';
import { BaseHttpService } from './base-http.service';

@Injectable()
export class BuyTicketService extends BaseHttpService<any>{

    getPricelist(data: any) : Observable<any>{
        this.specificUrl = "/api/Tickets";

        let model = new BuyTicketModel();
        model.username = data;

        return super.post(model);
    }

    sendTicket(data: any) : Observable<any>{
        this.specificUrl = "/api/Ticket/BoughtTicket";

        return super.post(data);
    }

}
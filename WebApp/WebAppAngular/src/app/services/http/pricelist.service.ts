import { Injectable } from '@angular/core';
import { BaseHttpService } from './base-http.service';
import { RegistrationModel } from 'src/app/models/registration-model';
import { Observable } from 'rxjs';
import { PricelistModel } from 'src/app/models/pricelist-model';

@Injectable()
export class PricelistService extends BaseHttpService<any>{
    
    formPricelist(pricelistModel: PricelistModel) : Observable<any>{
        this.specificUrl = "/api/TicketPrices";

        return super.post(pricelistModel);
    }

    getAllPricelists() : Observable<any>{
        this.specificUrl = "/api/TicketPrices";

        return super.getAll();
    }

    getAllFuturePricelists() : Observable<any>{
        this.specificUrl = "/api/TicketPrice/GetTicketPrices";

        return super.getAll();
    }

    getPricelistByDate(id: string): Observable<any>
    {
        this.specificUrl = "/api/TicketPrice/GetPricelist/";

        return super.getByIdString(id);
    }
}
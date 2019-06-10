import { Injectable } from '@angular/core';
import { RegistrationModel } from 'src/app/models/registration-model';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { PricelistModel } from '../models/pricelist-model';

@Injectable()
export class PricelistModificationService {
    price: PricelistModel = new PricelistModel();
    private pricelist = new BehaviorSubject(this.price);
    currentPricelist = this.pricelist.asObservable();
    
    setPricelist(pricelistSet: PricelistModel): Observable<any>{
        return of(this.pricelist.next(pricelistSet));
    }
}
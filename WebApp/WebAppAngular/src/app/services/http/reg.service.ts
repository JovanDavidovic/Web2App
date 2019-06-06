import { Injectable } from '@angular/core';
import { BaseHttpService } from './base-http.service';
import { RegistrationModel } from 'src/app/models/registration-model';
import { Observable } from 'rxjs';

@Injectable()
export class RegHttpService extends BaseHttpService<any>{
    
    register(registrationModel: RegistrationModel) : Observable<any>{
        this.specificUrl = "/api/Account/Register";
        //this.specificUrl = "/api/Passengers";  

        return super.post(registrationModel);
    }

}
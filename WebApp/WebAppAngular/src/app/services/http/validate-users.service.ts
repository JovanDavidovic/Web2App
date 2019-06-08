import { Injectable } from '@angular/core';
import { BaseHttpService } from './base-http.service';
import { RegistrationModel } from 'src/app/models/registration-model';
import { Observable } from 'rxjs';
import { JwtService } from '../jwt.service';

@Injectable()
export class ValidateUsersService extends BaseHttpService<any>{

    getAllUsers() : Observable<any>{
        this.specificUrl = "/api/Passengers";

        return super.getAll();
    }

    getUserByUsername(username: string) : Observable<any> {
        this.specificUrl = "/api/Passengers";

        let model = new RegistrationModel();
        model.username = username;

        return super.post(model);
    }

    DenyValidateToBackend (username: string) : Observable<any> {
        this.specificUrl = "/api/Passenger/Deny";

        let model = new RegistrationModel();
        model.username = username;

        return super.post(model);
    }

    AcceptValidateToBackend (username: string) : Observable<any> {
        this.specificUrl = "/api/Passenger/Accept";

        let model = new RegistrationModel();
        model.username = username;

        return super.post(model);
    }
}
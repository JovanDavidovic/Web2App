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

}
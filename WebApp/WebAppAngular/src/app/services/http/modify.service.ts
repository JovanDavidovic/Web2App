import { Injectable } from '@angular/core';
import { BaseHttpService } from './base-http.service';
import { RegistrationModel } from 'src/app/models/registration-model';
import { Observable } from 'rxjs';
import { JwtService } from '../jwt.service';

@Injectable()
export class ModifyHttpService extends BaseHttpService<any>{



    modify(registrationModel: RegistrationModel) : Observable<any>{
        this.specificUrl = "/api/Passengers/5";

        return super.put(registrationModel);
    }

    beforeModify(data: any) : Observable<any>{
        this.specificUrl = "/api/Passengers";

        let model = new RegistrationModel();
        model.email = data;

        return super.post(model);
    }
}
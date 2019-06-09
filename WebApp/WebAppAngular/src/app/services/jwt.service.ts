import { Injectable } from '@angular/core';
import { RegistrationModel } from 'src/app/models/registration-model';
import { Observable } from 'rxjs';

@Injectable()
export class JwtService {

    getRole(): string {
        let retData = localStorage.getItem('jwt');
        let jwtData = retData.split('.')[1];
        let decodedJwtJsonData = window.atob(jwtData);
        let decodedJwtData = JSON.parse(decodedJwtJsonData);

        return decodedJwtData.role;
    }

    getMail(): string {
        let retData = localStorage.getItem('jwt');
        let jwtData = retData.split('.')[1];
        let decodedJwtJsonData = window.atob(jwtData);
        let decodedJwtData = JSON.parse(decodedJwtJsonData);

        return decodedJwtData.nameid;
    }

    isTokenValid(): boolean {
        if (localStorage.getItem('jwt') != undefined) {
            let retData = localStorage.getItem('jwt');
            let jwtData = retData.split('.')[1];
            let decodedJwtJsonData = window.atob(jwtData);
            let decodedJwtData = JSON.parse(decodedJwtJsonData)

            if ((new Date().getTime() / 1000) < decodedJwtData.exp) {
                return true;
            }
            else {
                return false;
            }
        }
    }

}
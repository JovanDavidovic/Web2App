import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AuthHttpService{
    baseUrl = "http://localhost:52295"

    constructor(private http: HttpClient){

    }

    logIn(username: string, password: string){

        let data = `username=${username}&password=${password}&grant_type=password`;
        let httpOptions = {
            headers: {
                "Content-type": "application/x-www-form-urlencoded"
            }
        }

        return this.http.post<any>(this.baseUrl + "/oauth/token", data, httpOptions);
    }
}
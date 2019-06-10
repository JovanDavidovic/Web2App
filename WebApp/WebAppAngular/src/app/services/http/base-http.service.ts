import { Injectable } from "@angular/core";
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable()
export class BaseHttpService<T>{

    baseUrl = "http://localhost:52295"
    specificUrl = ""

    constructor(private http: HttpClient){

    }

    getAll(): Observable<any>{
        return this.http.get<any>(this.baseUrl + this.specificUrl);
    }

    getById(id: number): Observable<any>{
        return this.http.get<any>(this.baseUrl + this.specificUrl + `/${id}`);
    }

    getByIdString(id: string): Observable<any>{
        return this.http.get<any>(this.baseUrl + this.specificUrl + id);
    }

    post(data:any, options?:any): Observable<any> {
        return this.http.post(this.baseUrl + this.specificUrl, data, options);
    }
    
    put(data:any, options?:any) : Observable<any> {
        return this.http.put(this.baseUrl + this.specificUrl, data, options);
    }
}
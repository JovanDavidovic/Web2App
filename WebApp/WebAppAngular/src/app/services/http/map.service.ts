import { BaseHttpService } from './base-http.service';
import { Injectable } from '@angular/core';
import { RouteModel } from 'src/app/models/route-model';
import { Observable } from 'rxjs';

@Injectable()
export class MapService extends BaseHttpService<any>{

    createRoute(stations: RouteModel) : Observable<any>{
        this.specificUrl = "/api/DepartureTime/AddRoute"
    
        return super.post(stations);
    }

    getAllRoutes() : Observable<any>{
        this.specificUrl = "/api/DepartureTime/GetRoutes";

        return super.getAll();
    }

    sendSelectedRoute(routeName: number) {
        this.specificUrl = "/api/DepartureTime/GetRoute";

        return super.getById(routeName);
    }
}

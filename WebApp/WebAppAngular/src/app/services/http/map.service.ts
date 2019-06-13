import { BaseHttpService } from './base-http.service';
import { Injectable } from '@angular/core';
import { RouteModel } from 'src/app/models/route-model';
import { Observable } from 'rxjs';
import { DepartureTimeModel } from 'src/app/models/departure-time-model';
import { GetDepartureTimeModel } from 'src/app/models/get-departure-time-model';

@Injectable()
export class MapService extends BaseHttpService<any>{

    createRoute(stations: RouteModel) : Observable<any>{
        this.specificUrl = "/api/DepartureTime/AddRoute"
    
        return super.post(stations);
    }

    getAllRoutes(area: string) : Observable<any>{
        this.specificUrl = "/api/DepartureTime/GetRoutes/" + area;

        return super.getAll();
    }

    sendSelectedRoute(routeName: number) {
        this.specificUrl = "/api/DepartureTime/GetRoute";

        return super.getById(routeName);
    }

    createDepartureTime(departureTime: DepartureTimeModel) : Observable<any> {
        this.specificUrl = "/api/DepartureTimes";

        return super.post(departureTime);

    }

    sendRoutForDepartureTimes(model: GetDepartureTimeModel): Observable<any> {
        this.specificUrl = "/api/DepartureTime/GetExactDepartureTime";

        return super.post(model);
    }
}

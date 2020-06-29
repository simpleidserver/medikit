import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Observable, throwError } from "rxjs";
import { catchError, map } from 'rxjs/operators';
import { SearchMedicinalProduct } from "../models/SearchMedicinalProduct";

@Injectable({
    providedIn: 'root'
})
export class MedicinalProductService {
    constructor(private http: HttpClient) { }

    search(searchText: string, startIndex: number, count: number, isCommercialised : boolean, deliveryEnvironment: string): Observable<SearchMedicinalProduct> {
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/medicinalproducts/packages/.search?search_text=" + searchText + "&start_index=" + startIndex + "&count=" + count;
        targetUrl += "&is_commercialised=" + isCommercialised;
        if (deliveryEnvironment) {
            targetUrl += "&delivery_environment=" + deliveryEnvironment;
        }

        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(
            map((res: any) => {
                return SearchMedicinalProduct.fromJson(res);
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }
}
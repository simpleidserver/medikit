import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Observable, throwError } from "rxjs";
import { catchError, map } from 'rxjs/operators';
import { ReferenceTable } from "../models/reference-table";

@Injectable({
    providedIn: 'root'
})
export class ReferenceTableService {
    constructor(private http: HttpClient) { }

    get(code: string): Observable<ReferenceTable> {
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/referencetables/" + code;
        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(
            map((res: any) => {
                return ReferenceTable.fromJson(res);
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }
}
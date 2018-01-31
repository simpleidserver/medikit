import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Observable, throwError } from "rxjs";
import { catchError, map } from 'rxjs/operators';
import { Patient } from "../models/patient";

@Injectable({
    providedIn: 'root'
})
export class PatientService {
    constructor(private http: HttpClient) { }

    get(niss: string): Observable<Patient> {
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/patients/" + niss;
        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(
            map((res: any) => {
                return Patient.fromJson(res);
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }
}
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Observable, throwError } from "rxjs";
import { catchError, map } from 'rxjs/operators';
import { Patient } from "../models/patient";
import { SearchPatientResult } from "../models/search-patient-result";

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

    search(firstName: string, lastName: string, niss: string, startIndex : number, count : number) : Observable<SearchPatientResult> {
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/patients/.search";
        if (startIndex <= 0) {
            startIndex = 0;
        }

        if (count <= 0) {
            count = 5;
        }

        targetUrl += "?start_index=" + startIndex + "&count=" + count;
        if (firstName) {
            targetUrl += "&firstname=" + firstName;
        }

        if (lastName) {
            targetUrl += "&lastname=" + lastName;
        }

        if (niss) {
            targetUrl += "&niss=" + niss;
        }

        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(
            map((res: any) => {
                return SearchPatientResult.fromJson(res);
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }
}
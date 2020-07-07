import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { Medicalfile } from "../models/medicalfile";
import { SearchMedicalfileResult } from "../models/search-medicalfile";

@Injectable({
    providedIn: 'root'
})
export class MedicalfileService {
    constructor(private http: HttpClient) { }


    search(firstName: string, lastName: string, niss: string, startIndex: number, count: number, active: string = null, direction: string = null): Observable<SearchMedicalfileResult> {
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/medicalfiles/.search";
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

        if (active) {
            targetUrl = targetUrl + "&order_by=" + active;
        }

        if (direction) {
            targetUrl = targetUrl + "&order=" + direction;
        }

        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(
            map((res: any) => {
                return SearchMedicalfileResult.fromJson(res);
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }

    add(patientId: string) {
        const request = JSON.stringify({ patient_id : patientId});
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/medicalfiles";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(
            map((res: any) => {
                return Medicalfile.fromJson(res);
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }
}
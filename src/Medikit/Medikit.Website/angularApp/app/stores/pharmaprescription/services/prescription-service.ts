import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PharmaPrescription } from "../models/pharma-prescription";

@Injectable({
    providedIn: 'root'
})
export class PharmaPrescriptionService {
    constructor(private http: HttpClient) { }

    getOpenedPrescriptions(patientNiss : string, page : number, samlAssertion : string): Observable<Array<string>> {
        let headers = new HttpHeaders();
        const request = JSON.stringify({ assertion_token: samlAssertion, page_number: page, patient_niss: patientNiss });
        let targetUrl = process.env.API_URL + "/prescriptions/opened";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post<Array<string>>(targetUrl, request, { headers: headers });
    }

    getPrescription(prescriptionId: string, samlAssertion: string): Observable<PharmaPrescription> {
        const request = JSON.stringify({ assertion_token: samlAssertion });
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/prescriptions/" + prescriptionId;
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(map((res: any) => {
            return PharmaPrescription.fromJson(res);
        }));
    }
}
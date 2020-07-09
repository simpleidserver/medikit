import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { Medicalfile } from "../models/medicalfile";
import { PharmaDrugPrescription } from "../models/pharma-drug-prescription";
import { PharmaPosologyFreeText } from "../models/pharma-posology";
import { PharmaPrescription } from "../models/pharma-prescription";
import { SearchMedicalfileResult } from "../models/search-medicalfile";
import { SearchPrescriptionResult } from "../models/search-prescription-result";

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

    get(medicalfileId: string) {
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/medicalfiles/" + medicalfileId;
        headers = headers.set('Accept', 'application/json');
        return this.http.get(targetUrl, { headers: headers }).pipe(
            map((res: any) => {
                return Medicalfile.fromJson(res);
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }

    getPrescriptionMetadata(medicalfileId: string) {
        let targetUrl = process.env.API_URL + "/medicalfiles/" + medicalfileId + '/prescriptions/metadata';
        return this.http.get<any>(targetUrl);
    }

    getPrescriptions(medicalfileid: string, samlAssertion : string, page : number) {
        const request = JSON.stringify({ assertion_token: samlAssertion, page : page });
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/medicalfiles/" + medicalfileid + '/prescriptions';
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(
            map((res: any) => {
                return SearchPrescriptionResult.fromJson(res);
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }

    getOpenedPrescriptions(medicalfileid: string, samlAssertion: string, page: number) {
        const request = JSON.stringify({ assertion_token: samlAssertion, page: page });
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/medicalfiles/" + medicalfileid + '/prescriptions/opened';
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(
            map((res: any) => {
                return SearchPrescriptionResult.fromJson(res);
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }

    getPrescription(medicalfileId: string, prescriptionId: string, samlAssertion: string) {
        const request = JSON.stringify({ assertion_token: samlAssertion });
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/medicalfiles/" + medicalfileId + '/prescriptions/' + prescriptionId;
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers }).pipe(
            map((res: any) => {
                return PharmaPrescription.fromJson(res);
            }),
            catchError(err => {
                return throwError(err);
            })
        );
    }

    revokePrescription(medicalfileId: string, prescriptionId: string, reason: string, samlAssertion: string) {
        const request = JSON.stringify({ assertion_token: samlAssertion, reason: reason });
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/medicalfiles/" + medicalfileId + '/prescriptions/' + prescriptionId + '/revoke';
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post(targetUrl, request, { headers: headers });
    }

    addPrescription(medicalfileId: string, prescription: PharmaPrescription, samlAssertion: string) {
        var json: any = {
            assertion_token: samlAssertion,
            create_datetime: prescription.CreateDateTime,
            expiration_datetime: prescription.EndExecutionDate,
            prescription_type: prescription.Type,
            medications: []
        };
        if (prescription.DrugsPrescribed) {
            prescription.DrugsPrescribed.forEach((_: PharmaDrugPrescription) => {
                var record: any = {
                    package_code: _.PackageCode
                };
                if (_.InstructionForPatient) {
                    record['instruction_for_patient'] = _.InstructionForPatient;
                }

                if (_.InstructionForReimbursement) {
                    record['instruction_for_reimbursement'] = _.InstructionForReimbursement;
                }

                if (_.BeginMoment) {
                    record['begin_moment'] = _.BeginMoment;
                }

                if (_.Posology.Type === 'freetext') {
                    var freeText: PharmaPosologyFreeText = _.Posology as PharmaPosologyFreeText;
                    record['posology'] = {
                        type: 'freetext',
                        value: freeText.Content
                    };
                }

                json.medications.push(record);
            });
        }

        const request = JSON.stringify(json);
        let headers = new HttpHeaders();
        let targetUrl = process.env.API_URL + "/medicalfiles/" + medicalfileId + '/prescriptions/add';
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post<string>(targetUrl, request, { headers: headers });
    }
}
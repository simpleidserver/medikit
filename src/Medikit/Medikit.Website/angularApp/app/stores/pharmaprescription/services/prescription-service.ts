import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PharmaDrugPrescription } from "../models/pharma-drug-prescription";
import { PharmaPosologyFreeText } from "../models/pharma-posology";
import { PharmaPrescription } from "../models/pharma-prescription";

@Injectable({
    providedIn: 'root'
})
export class PharmaPrescriptionService {
    constructor(private http: HttpClient) { }

    getMetadata() {
        let targetUrl = process.env.API_URL + "/prescriptions/metadata";
        return this.http.get<any>(targetUrl);
    }

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

    addPrescription(prescription: PharmaPrescription, samlAssertion: string): Observable<string> {
        var json : any = {
            assertion_token: samlAssertion,
            niss: prescription.Patient.niss,
            create_datetime: prescription.CreateDateTime,
            expiration_datetime: prescription.EndExecutionDate,
            prescription_type: prescription.Type,
            medications: []
        };
        if (prescription.DrugsPrescribed) {
            prescription.DrugsPrescribed.forEach((_: PharmaDrugPrescription) => {
                var record  : any = {
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
                    var freeText: PharmaPosologyFreeText= _.Posology as PharmaPosologyFreeText;
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
        let targetUrl = process.env.API_URL + "/prescriptions";
        headers = headers.set('Accept', 'application/json');
        headers = headers.set('Content-Type', 'application/json');
        return this.http.post<string>(targetUrl, request, { headers: headers });
    }
}
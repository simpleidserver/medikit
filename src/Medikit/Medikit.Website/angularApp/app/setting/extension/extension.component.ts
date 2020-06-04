import { Component, OnInit } from '@angular/core';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import { FormGroup, FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';

class MedicalProfession {
    constructor(public displayName: string, public namespace: string) { }
}

@Component({
    selector: 'extension-component',
    templateUrl: './extension.component.html',
    styleUrls: ['./extension.component.scss']
})
export class ExtensionComponent implements OnInit {
    isExtensionInstalled: boolean;
    medicalProfessionForm: FormGroup = new FormGroup({
        profession : new FormControl()
    });
    identityCertificateForm: FormGroup = new FormGroup({
        certificate: new FormControl(),
        password : new FormControl()
    });
    medicalProfessions: Array<MedicalProfession> = [];
    certificates: Array<string> = [];

    constructor(private medikitExtensionService: MedikitExtensionService, private snackBar: MatSnackBar, private translateService : TranslateService) { }

    ngOnInit(): void {
        const self = this;
        if (this.medikitExtensionService.isExtensionInstalled()) {
            this.isExtensionInstalled = true;
        } else {
            this.isExtensionInstalled = false;
        }

        this.medikitExtensionService.getMedicalProfessions().subscribe(function (e: any) {
            self.medicalProfessionForm.controls['profession'].setValue(e.content['current_profession']);
            e.content['professions'].forEach((m : any) => {
                self.medicalProfessions.push(new MedicalProfession(m['display_name'], m['namespace']));
            });
            self.medikitExtensionService.getIdentityCertificates().subscribe(function (e: any) {
                self.identityCertificateForm.controls['certificate'].setValue(e.content['current_certificate']);
                self.certificates = e.content['certificates'];
            });
        });
    }

    onSubmitMedicalProfession(form: any) {
        const self = this;
        this.medikitExtensionService.chooseMedicalProfession(form.profession).subscribe(function (e: any) {
            if (e.type === 'error') {
                self.snackBar.open(self.translateService.instant('profession-not-updated'), self.translateService.instant('undo'), {
                    duration: 2000,
                });
            } else {
                self.snackBar.open(self.translateService.instant('profession-updated'), self.translateService.instant('undo'), {
                    duration: 2000,
                });
            }
        });
    }

    onSubmitIdentityCertificate(form: any) {
        const self = this;
        this.medikitExtensionService.chooseIdentityCertificate(form.certificate, form.password).subscribe(function (e: any) {
            if (e.type === 'error') {
                self.snackBar.open(self.translateService.instant('identity-certificate-not-updated'), self.translateService.instant('undo'), {
                    duration: 2000,
                });
            } else {
                self.snackBar.open(self.translateService.instant('identity-certificate-updated'), self.translateService.instant('undo'), {
                    duration: 2000,
                });
            }
        });
    }
}
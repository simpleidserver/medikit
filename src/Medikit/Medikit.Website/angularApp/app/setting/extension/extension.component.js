var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import { FormGroup, FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
var MedicalProfession = (function () {
    function MedicalProfession(displayName, namespace) {
        this.displayName = displayName;
        this.namespace = namespace;
    }
    return MedicalProfession;
}());
var ExtensionComponent = (function () {
    function ExtensionComponent(medikitExtensionService, snackBar, translateService) {
        this.medikitExtensionService = medikitExtensionService;
        this.snackBar = snackBar;
        this.translateService = translateService;
        this.medicalProfessionForm = new FormGroup({
            profession: new FormControl()
        });
        this.identityCertificateForm = new FormGroup({
            certificate: new FormControl(),
            password: new FormControl()
        });
        this.medicalProfessions = [];
        this.certificates = [];
    }
    ExtensionComponent.prototype.ngOnInit = function () {
        var self = this;
        if (this.medikitExtensionService.isExtensionInstalled()) {
            this.isExtensionInstalled = true;
        }
        else {
            this.isExtensionInstalled = false;
        }
        this.medikitExtensionService.getMedicalProfessions().subscribe(function (e) {
            self.medicalProfessionForm.controls['profession'].setValue(e.content['current_profession']);
            e.content['professions'].forEach(function (m) {
                self.medicalProfessions.push(new MedicalProfession(m['display_name'], m['namespace']));
            });
            self.medikitExtensionService.getIdentityCertificates().subscribe(function (e) {
                self.identityCertificateForm.controls['certificate'].setValue(e.content['current_certificate']);
                self.certificates = e.content['certificates'];
            });
        });
    };
    ExtensionComponent.prototype.onSubmitMedicalProfession = function (form) {
        var self = this;
        this.medikitExtensionService.chooseMedicalProfession(form.profession).subscribe(function (e) {
            if (e.type === 'error') {
                self.snackBar.open(self.translateService.instant('profession-not-updated'), self.translateService.instant('undo'), {
                    duration: 2000,
                });
            }
            else {
                self.snackBar.open(self.translateService.instant('profession-updated'), self.translateService.instant('undo'), {
                    duration: 2000,
                });
            }
        });
    };
    ExtensionComponent.prototype.onSubmitIdentityCertificate = function (form) {
        var self = this;
        this.medikitExtensionService.chooseIdentityCertificate(form.certificate, form.password).subscribe(function (e) {
            if (e.type === 'error') {
                self.snackBar.open(self.translateService.instant('identity-certificate-not-updated'), self.translateService.instant('undo'), {
                    duration: 2000,
                });
            }
            else {
                self.snackBar.open(self.translateService.instant('identity-certificate-updated'), self.translateService.instant('undo'), {
                    duration: 2000,
                });
            }
        });
    };
    ExtensionComponent = __decorate([
        Component({
            selector: 'extension-component',
            templateUrl: './extension.component.html',
            styleUrls: ['./extension.component.scss']
        }),
        __metadata("design:paramtypes", [MedikitExtensionService, MatSnackBar, TranslateService])
    ], ExtensionComponent);
    return ExtensionComponent;
}());
export { ExtensionComponent };
//# sourceMappingURL=extension.component.js.map
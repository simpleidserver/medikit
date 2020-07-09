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
import { MatSnackBar } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { MedikitExtensionService } from '@app/infrastructure/services/medikitextension.service';
import { LoadPharmaPrescription } from '@app/stores/pharmaprescription/prescription-actions';
import * as fromAppState from '@app/stores/appstate';
import { select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormControl } from '@angular/forms';
var ViewPrescriptionComponent = (function () {
    function ViewPrescriptionComponent(translateService, snackBar, store, route, medikitExtensionService) {
        this.translateService = translateService;
        this.snackBar = snackBar;
        this.store = store;
        this.route = route;
        this.medikitExtensionService = medikitExtensionService;
        this.isLoading = false;
        this.nbPrescriptions = 1;
        this.updateNbPrescriptionsForm = new FormGroup({
            nbPrescriptions: new FormControl(1)
        });
    }
    ViewPrescriptionComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.store.pipe(select(fromAppState.selectPharmaPrescriptionResult)).subscribe(function (st) {
            if (!st) {
                return;
            }
            if (_this.isLoading) {
                _this.prescription = st;
                _this.isLoading = false;
            }
        });
        this.refresh();
    };
    ViewPrescriptionComponent.prototype.refresh = function () {
        var undo = this.translateService.instant('undo');
        if (!this.medikitExtensionService.isExtensionInstalled()) {
            this.snackBar.open(this.translateService.instant('extension-not-installed'), undo, {
                duration: 2000,
            });
            return;
        }
        var session = this.medikitExtensionService.getEhealthSession();
        if (session == null) {
            this.snackBar.open(this.translateService.instant('no-active-session'), undo, {
                duration: 2000,
            });
            return;
        }
        var id = this.route.snapshot.params['id'];
        this.isLoading = true;
        var loadPharmaPrescriptions = new LoadPharmaPrescription(id, session['assertion_token']);
        this.store.dispatch(loadPharmaPrescriptions);
    };
    ViewPrescriptionComponent.prototype.updateNbPrescriptions = function (evt, form) {
        evt.preventDefault();
        this.nbPrescriptions = form.nbPrescriptions;
    };
    ViewPrescriptionComponent = __decorate([
        Component({
            selector: 'view-prescription-component',
            templateUrl: './view-prescription.component.html'
        }),
        __metadata("design:paramtypes", [TranslateService, MatSnackBar, Store, ActivatedRoute, MedikitExtensionService])
    ], ViewPrescriptionComponent);
    return ViewPrescriptionComponent;
}());
export { ViewPrescriptionComponent };
//# sourceMappingURL=view-prescription.component.js.map
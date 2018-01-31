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
import { Store, select } from '@ngrx/store';
import { EhealthSessionService } from '../../services/ehealthsession.service';
var ListPrescriptionComponent = (function () {
    function ListPrescriptionComponent(store, ehealthSessionService) {
        this.store = store;
        this.ehealthSessionService = ehealthSessionService;
        this.prescriptionIds = [];
    }
    ListPrescriptionComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isExtensionInstalled = this.ehealthSessionService.isExtensionInstalled();
        console.log(this.isExtensionInstalled);
        var session = this.ehealthSessionService.getEhealthSession();
        if (session) {
            this.isEhealthSessionValid = true;
        }
        else {
            this.isEhealthSessionValid = false;
        }
        this.store.pipe(select('pharmaPrescriptionLst')).subscribe(function (st) {
            _this.prescriptionIds = st.prescriptionIds;
        });
        this.refresh();
    };
    ListPrescriptionComponent.prototype.refresh = function () {
    };
    ListPrescriptionComponent.prototype.authenticateEHEALTH = function () {
        if (!this.ehealthSessionService.isExtensionInstalled()) {
            return;
        }
        this.ehealthSessionService.getEHEALTHCertificateAuth().then(function () {
            console.log("coucou");
            this.isEhealthSessionValid = true;
        }).catch(function () {
        });
    };
    ListPrescriptionComponent.prototype.disconnectEHEALTH = function () {
    };
    ListPrescriptionComponent = __decorate([
        Component({
            selector: 'list-prescription-component',
            templateUrl: './list-prescription.component.html',
            styleUrls: ['./list-prescription.component.scss']
        }),
        __metadata("design:paramtypes", [Store, EhealthSessionService])
    ], ListPrescriptionComponent);
    return ListPrescriptionComponent;
}());
export { ListPrescriptionComponent };
//# sourceMappingURL=list-prescription.component.js.map
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
import { ActivatedRoute } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { LoadPharmaPrescription } from './actions/pharma-prescription';
var ViewPrescriptionComponent = (function () {
    function ViewPrescriptionComponent(store, route) {
        this.store = store;
        this.route = route;
    }
    ViewPrescriptionComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.store.pipe(select('pharmaPrescriptionView')).subscribe(function (st) {
            if (!st.prescription) {
                return;
            }
            _this.prescription = st.prescription;
        });
        this.refresh();
    };
    ViewPrescriptionComponent.prototype.refresh = function () {
        var id = this.route.snapshot.params['id'];
        var loadPharmaPrescriptions = new LoadPharmaPrescription(id);
        this.store.dispatch(loadPharmaPrescriptions);
    };
    ViewPrescriptionComponent = __decorate([
        Component({
            selector: 'view-prescription-component',
            templateUrl: './view-prescription.component.html'
        }),
        __metadata("design:paramtypes", [Store, ActivatedRoute])
    ], ViewPrescriptionComponent);
    return ViewPrescriptionComponent;
}());
export { ViewPrescriptionComponent };
//# sourceMappingURL=view-prescription.component.js.map
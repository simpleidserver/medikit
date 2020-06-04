var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
import { PrescriptionService } from '@app/prescription/services/prescription-service';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes } from '../actions/pharma-prescription';
var ListPharmaPrescriptionEffects = (function () {
    function ListPharmaPrescriptionEffects(actions$, prescriptionService) {
        var _this = this;
        this.actions$ = actions$;
        this.prescriptionService = prescriptionService;
        this.getOpenedPrescriptions$ = this.actions$
            .pipe(ofType(ActionTypes.LOAD_PHARMA_PRESCRIPTIONS), mergeMap(function (evt) {
            return _this.prescriptionService.getOpenedPrescriptions(evt.patientNiss, evt.page, evt.samlAssertion)
                .pipe(map(function (prescriptionIds) { return { type: ActionTypes.PHARMA_PRESCRIPTIONS_LOADED, prescriptionIds: prescriptionIds }; }), catchError(function () { return of({ type: ActionTypes.ERROR_LOAD_PHARMA_PRESCRIPTIONS }); }));
        }));
    }
    __decorate([
        Effect(),
        __metadata("design:type", Object)
    ], ListPharmaPrescriptionEffects.prototype, "getOpenedPrescriptions$", void 0);
    ListPharmaPrescriptionEffects = __decorate([
        Injectable(),
        __metadata("design:paramtypes", [Actions,
            PrescriptionService])
    ], ListPharmaPrescriptionEffects);
    return ListPharmaPrescriptionEffects;
}());
export { ListPharmaPrescriptionEffects };
//# sourceMappingURL=pharma-prescription.js.map
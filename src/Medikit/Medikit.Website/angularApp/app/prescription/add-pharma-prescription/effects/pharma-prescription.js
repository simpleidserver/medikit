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
import { PatientService } from '@app/patient/services/patient-service';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes } from '../actions/pharma-prescription';
var PharmaPrescriptionFormEffects = (function () {
    function PharmaPrescriptionFormEffects(actions$, patientService) {
        var _this = this;
        this.actions$ = actions$;
        this.patientService = patientService;
        this.checkNiss$ = this.actions$
            .pipe(ofType(ActionTypes.CHECK_NISS), mergeMap(function (evt) {
            return _this.patientService.get(evt.niss)
                .pipe(map(function (patient) { return { type: ActionTypes.NISS_CHECKED, patient: patient }; }), catchError(function () { return of({ type: ActionTypes.NISS_UNKNOWN, niss: evt.niss }); }));
        }));
    }
    __decorate([
        Effect(),
        __metadata("design:type", Object)
    ], PharmaPrescriptionFormEffects.prototype, "checkNiss$", void 0);
    PharmaPrescriptionFormEffects = __decorate([
        Injectable(),
        __metadata("design:paramtypes", [Actions,
            PatientService])
    ], PharmaPrescriptionFormEffects);
    return PharmaPrescriptionFormEffects;
}());
export { PharmaPrescriptionFormEffects };
//# sourceMappingURL=pharma-prescription.js.map
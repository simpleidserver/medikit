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
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes } from './patient-actions';
import { PatientService } from './services/patient-service';
var PatientEffects = (function () {
    function PatientEffects(actions$, patientService) {
        var _this = this;
        this.actions$ = actions$;
        this.patientService = patientService;
        this.searchPatients$ = this.actions$
            .pipe(ofType(ActionTypes.SEARCH_PATIENTS), mergeMap(function (evt) {
            return _this.patientService.search(evt.firstName, evt.lastName, evt.niss)
                .pipe(map(function (patients) { return { type: ActionTypes.PATIENTS_LOADED, patients: patients }; }), catchError(function () { return of({ type: ActionTypes.ERROR_SEARCH_PATIENTS }); }));
        }));
        this.getPatient$ = this.actions$
            .pipe(ofType(ActionTypes.GET_PATIENT), mergeMap(function (evt) {
            return _this.patientService.get(evt.niss)
                .pipe(map(function (patient) { return { type: ActionTypes.PATIENT_LOADED, patient: patient }; }), catchError(function () { return of({ type: ActionTypes.ERROR_GET_PATIENT }); }));
        }));
    }
    __decorate([
        Effect(),
        __metadata("design:type", Object)
    ], PatientEffects.prototype, "searchPatients$", void 0);
    __decorate([
        Effect(),
        __metadata("design:type", Object)
    ], PatientEffects.prototype, "getPatient$", void 0);
    PatientEffects = __decorate([
        Injectable(),
        __metadata("design:paramtypes", [Actions,
            PatientService])
    ], PatientEffects);
    return PatientEffects;
}());
export { PatientEffects };
//# sourceMappingURL=patient-effects.js.map
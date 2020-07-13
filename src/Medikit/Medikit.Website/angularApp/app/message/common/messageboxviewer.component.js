var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ViewChild } from '@angular/core';
import { Store, select } from '@ngrx/store';
import * as fromAppState from '@app/stores/appstate';
import * as fromPatientActions from '@app/stores/patient/patient-actions';
import { MatSort, MatPaginator } from '@angular/material';
import { merge } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';
var ListPatientComponent = (function () {
    function ListPatientComponent(store) {
        this.store = store;
        this.displayedColumns = ['logo', 'niss', 'firstname', 'lastname', 'updateDateTime', 'actions'];
        this.patients$ = [];
        this.searchInsuredFormGroup = new FormGroup({
            niss: new FormControl(),
            firstname: new FormControl(),
            lastname: new FormControl()
        });
    }
    ListPatientComponent.prototype.ngAfterViewInit = function () {
        var _this = this;
        merge(this.sort.sortChange, this.paginator.page).subscribe(function () { return _this.refresh(); });
    };
    ListPatientComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.store.pipe(select(fromAppState.selectPatientsResult)).subscribe(function (searchPatientResult) {
            if (!searchPatientResult) {
                return;
            }
            _this.patients$ = searchPatientResult.content;
            _this.length = searchPatientResult.totalLength;
        });
        this.refresh();
    };
    ListPatientComponent.prototype.ngOnDestroy = function () {
    };
    ListPatientComponent.prototype.onSubmitSearchInsuredForm = function () {
        this.refresh();
    };
    ListPatientComponent.prototype.refresh = function () {
        var startIndex = 0;
        var count = 5;
        var active = "create_datetime";
        var direction = "desc";
        var niss = this.searchInsuredFormGroup.controls['niss'].value;
        var firstname = this.searchInsuredFormGroup.controls['firstname'].value;
        var lastname = this.searchInsuredFormGroup.controls['lastname'].value;
        if (this.sort.active) {
            active = this.sort.active;
        }
        if (this.sort.direction) {
            direction = this.sort.direction;
        }
        if (this.paginator.pageIndex && this.paginator.pageSize) {
            startIndex = this.paginator.pageIndex * this.paginator.pageSize;
        }
        if (this.paginator.pageSize) {
            count = this.paginator.pageSize;
        }
        this.store.dispatch(new fromPatientActions.SearchPatients(niss, firstname, lastname, startIndex, count, active, direction));
    };
    __decorate([
        ViewChild(MatPaginator),
        __metadata("design:type", MatPaginator)
    ], ListPatientComponent.prototype, "paginator", void 0);
    __decorate([
        ViewChild(MatSort),
        __metadata("design:type", MatSort)
    ], ListPatientComponent.prototype, "sort", void 0);
    ListPatientComponent = __decorate([
        Component({
            selector: 'list-patient-component',
            templateUrl: './list-patient.component.html',
            styleUrls: ['./list-patient.component.scss']
        }),
        __metadata("design:paramtypes", [Store])
    ], ListPatientComponent);
    return ListPatientComponent;
}());
export { ListPatientComponent };
//# sourceMappingURL=list-patient.component.js.map
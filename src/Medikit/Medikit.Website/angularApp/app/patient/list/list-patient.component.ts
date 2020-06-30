import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Store, select } from '@ngrx/store';
import * as fromAppState from '@app/stores/appstate';
import * as fromPatientActions from '@app/stores/patient/patient-actions';
import { MatSort, MatPaginator } from '@angular/material';
import { merge } from 'rxjs';
import { Patient } from '@app/stores/patient/models/patient';
import { SearchPatientResult } from '@app/stores/patient/models/search-patient-result';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
    selector: 'list-patient-component',
    templateUrl: './list-patient.component.html',
    styleUrls: ['./list-patient.component.scss']
})
export class ListPatientComponent implements OnInit, OnDestroy {
    displayedColumns: string[] = ['logo', 'niss', 'firstname', 'lastname', 'updateDateTime', 'actions'];
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    patients$: Patient[] = [];
    searchInsuredFormGroup: FormGroup = new FormGroup(
    {
        niss: new FormControl(),
        firstname: new FormControl(),
        lastname: new FormControl()
    });
    length: number;

    constructor(private store: Store<fromAppState.AppState>) {

    }

    ngAfterViewInit() {
        merge(this.sort.sortChange, this.paginator.page).subscribe(() => this.refresh());
    }

    ngOnInit(): void {
        this.store.pipe(select(fromAppState.selectPatientsResult)).subscribe((searchPatientResult: SearchPatientResult) => {
            if (!searchPatientResult) {
                return;
            }

            this.patients$ = searchPatientResult.content;
            this.length = searchPatientResult.totalLength;
        });
        this.refresh();
    }

    ngOnDestroy(): void {

    }

    onSubmitSearchInsuredForm() {
        this.refresh();
    }

    refresh() {
        let startIndex: number = 0;
        let count: number = 5;
        let active = "create_datetime";
        let direction = "desc";
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
    }
}
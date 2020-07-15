import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MatPaginator, MatSnackBar, MatSort } from '@angular/material';
import * as fromAppState from '@app/stores/appstate';
import * as fromMedicalfileActions from '@app/stores/medicalfile/medicalfile-actions';
import { Medicalfile } from '@app/stores/medicalfile/models/medicalfile';
import { SearchMedicalfileResult } from '@app/stores/medicalfile/models/search-medicalfile';
import { Patient } from '@app/stores/patient/models/patient';
import { SearchPatientResult } from '@app/stores/patient/models/search-patient-result';
import * as fromPatientActions from '@app/stores/patient/patient-actions';
import { ScannedActionsSubject, select, Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { merge } from 'rxjs';
import { filter } from 'rxjs/operators';

@Component({
    selector: 'list-medicalfile-component',
    templateUrl: './list-medicalfile.component.html',
    styleUrls: ['./list-medicalfile.component.scss']
})
export class ListMedicalfileComponent implements OnInit {
    displayedColumns: string[] = [ 'niss', 'firstname', 'lastname', 'updateDateTime', 'actions'];
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;
    medicalfiles$: Medicalfile[] = [];
    searchInsuredFormGroup: FormGroup = new FormGroup(
    {
        niss: new FormControl(),
        firstname: new FormControl(),
        lastname: new FormControl()
    });
    length: number;


    constructor(private store: Store<fromAppState.AppState>,
        private dialog: MatDialog,
        private actions$: ScannedActionsSubject,
        private snackBar: MatSnackBar,
        private translateService: TranslateService) { }

    ngOnInit(): void {
        this.actions$.pipe(
            filter((action: any) => action.type == fromMedicalfileActions.ActionTypes.ERROR_ADD_MEDICALFILE))
            .subscribe((e: any) => {
                var arr  : string[] = [];
                for (var obj in e.error.errors) {
                    e.error.errors[obj].forEach(function (_: string) {
                        arr.push(_);
                    });
                }

                this.snackBar.open(arr.join(','), this.translateService.instant('undo'), {
                    duration: 2000
                });
            });
        this.actions$.pipe(
            filter((action: any) => action.type == fromMedicalfileActions.ActionTypes.MEDICALFILE_ADDED))
            .subscribe(() => {
                this.snackBar.open(this.translateService.instant('medicalfile-added'), this.translateService.instant('undo'), {
                    duration: 2000
                });
                this.refresh();
            });

        this.store.pipe(select(fromAppState.selectMedicalfilesResult)).subscribe((searchMedicalfileResult: SearchMedicalfileResult) => {
            if (!searchMedicalfileResult) {
                return;
            }

            this.medicalfiles$ = searchMedicalfileResult.content;
            this.length = searchMedicalfileResult.totalLength;
        });
        this.refresh();
    }

    ngAfterViewInit() {
        merge(this.sort.sortChange, this.paginator.page).subscribe(() => this.refresh());
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

        this.store.dispatch(new fromMedicalfileActions.SearchMedicalfiles(niss, firstname, lastname, startIndex, count, active, direction));
    }

    addMedicalfile() {
        const dialogRef = this.dialog.open(AddMedicalfileDialog, {
            width: '400px'
        });
        dialogRef.afterClosed().subscribe((result : Patient) => {
            if (!result) {
                return;
            }

            this.store.dispatch(new fromMedicalfileActions.AddMedicalfile(result.id));
        });
    }
}

@Component({
    selector: 'add-medicalfile-dialog',
    templateUrl: 'add-medicalfile-dialog.html'
})
export class AddMedicalfileDialog implements OnInit {
    filteredPatientsByNiss: Patient[] = [];
    selectedPatient: Patient;
    patientFormGroup: FormGroup = new FormGroup({
        niss: new FormControl('', [
            Validators.required,
            Validators.pattern('^[0-9]{2}[0-9]{2}[0-9]{2}[0-9]{3}[0-9]{2}$')
        ])
    });
    constructor(public dialogRef: MatDialogRef<AddMedicalfileDialog>, private appStore: Store<fromAppState.AppState>) { }

    ngOnInit() {
        const self = this;
        this.selectedPatient = null;
        this.appStore.pipe(select(fromAppState.selectPatientsByNissResult)).subscribe((st: SearchPatientResult) => {
            if (!st) {
                return;
            }

            this.filteredPatientsByNiss = st.content;
        });
        this.patientFormGroup.controls['niss'].valueChanges.subscribe((_) => {
            self.appStore.dispatch(new fromPatientActions.SearchPatientsByNiss(_));
        });
    }

    selectPatient(evt: any) {
        this.selectedPatient = this.filteredPatientsByNiss.filter(_ => _.niss == evt.option.value)[0];
    }

    confirm() {
        if (this.patientFormGroup.invalid) {
            return;
        }

        this.dialogRef.close(this.selectedPatient);
    }

    cancel(): void {
        this.dialogRef.close();
    }
}
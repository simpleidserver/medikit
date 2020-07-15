import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { MatDialog, MatDialogRef, MatSnackBar } from "@angular/material";
import { MedikitExtensionService } from "@app/infrastructure/services/medikitextension.service";
import * as fromAppState from '@app/stores/appstate';
import * as fromMessageActions from '@app/stores/message/message-actions';
import { Message } from "@app/stores/message/models/message";
import { ScannedActionsSubject, select, Store } from "@ngrx/store";
import { TranslateService } from "@ngx-translate/core";
import { filter } from "rxjs/operators";

@Component({
    selector: 'messageboxviewer',
    templateUrl: './messageboxviewer.component.html',
    styleUrls: ['./messageboxviewer.component.scss']
})
export class MessageboxViewerComponent implements OnInit, OnDestroy {
    displayedColumns: Array<string> = ['select', 'hasAnnex', 'isImportant', 'sender', 'destination', 'description']; 
    isDeleteEnabled: boolean = false;
    isLoading: boolean = false;
    isSessionActive: boolean = false;
    subscribeSessionCreated: any;
    subscribeSessionDropped: any;
    @Input() boxType: string;
    messages$: Array<Message> = [];
    constructor(private store: Store<fromAppState.AppState>,
        private medikitExtensionService: MedikitExtensionService,
        private dialog: MatDialog,
        private actions$: ScannedActionsSubject,
        private snackBar: MatSnackBar,
        private translateService: TranslateService) { }

    ngOnInit(): void {
        if (this.medikitExtensionService.getEhealthSession() !== null) {
            this.isSessionActive = true;
        }

        this.actions$.pipe(
            filter((action: any) => action.type == fromMessageActions.ActionTypes.MESSAGE_DELETED))
            .subscribe(() => {
                this.snackBar.open(this.translateService.instant('messages-removed'), this.translateService.instant('undo'), {
                    duration: 2000
                });
                this.refresh();
            });
        this.actions$.pipe(
            filter((action: any) => action.type == fromMessageActions.ActionTypes.ERROR_DELETE_MESSAGES))
            .subscribe(() => {
                this.snackBar.open(this.translateService.instant('messages-cannot-be-removed'), this.translateService.instant('undo'), {
                    duration: 2000
                });
            });
        this.subscribeSessionCreated = this.medikitExtensionService.sessionCreated.subscribe(() => {
            this.isSessionActive = true;
            this.refresh();
        });
        this.subscribeSessionDropped = this.medikitExtensionService.sessionDropped.subscribe(() => {
            this.isSessionActive = false;
        });
        this.store.pipe(select(fromAppState.selectMessagesResult)).subscribe((messages: Array<Message>) => {
            if (!messages) {
                return;
            }

            this.messages$ = messages;
            this.isLoading = false;
        });

        this.refresh();
    }

    toggleSelection(message: Message) {
        message.isSelected = !message.isSelected;
        this.isDeleteEnabled = this.messages$.filter((_) => _.isSelected == true).length > 0;
    }

    ngOnDestroy(): void {
        this.subscribeSessionCreated.unsubscribe();
        this.subscribeSessionDropped.unsubscribe();
    }

    removeSelectedMessages() {
        if (!this.isSessionActive) {
            return;
        }

        const dialogRef = this.dialog.open(ConfirmDeleteMessagesDialog, {
            width: '400px'
        });
        dialogRef.afterClosed().subscribe((result: boolean) => {
            if (!result) {
                return;
            }

            var messageIds = this.messages$.filter((_) => _.isSelected == true).map((_) => _.id);
            var session: any = this.medikitExtensionService.getEhealthSession();
            this.store.dispatch(new fromMessageActions.DeleteMessages(messageIds, this.boxType, session['assertion_token']));
        });
    }

    refresh() {
        if (!this.isSessionActive) {
            return;
        }

        this.isLoading = true;
        var session: any = this.medikitExtensionService.getEhealthSession();
        this.store.dispatch(new fromMessageActions.SearchMessages(1, 100, this.boxType, session['assertion_token']));
    }
}

@Component({
    selector: 'confirm-delete-messages-dialog',
    templateUrl: 'confirm-delete-messages-dialog.html'
})
export class ConfirmDeleteMessagesDialog {
    constructor(public dialogRef: MatDialogRef<ConfirmDeleteMessagesDialog>) { }

    confirm() {
        this.dialogRef.close(true);
    }

    cancel(): void {
        this.dialogRef.close();
    }
}
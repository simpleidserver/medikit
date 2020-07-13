import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { MedikitExtensionService } from "@app/infrastructure/services/medikitextension.service";
import * as fromAppState from '@app/stores/appstate';
import * as fromMessageActions from '@app/stores/message/message-actions';
import { Message } from "@app/stores/message/models/message";
import { select, Store } from "@ngrx/store";

@Component({
    selector: 'messageboxviewer',
    templateUrl: './messageboxviewer.component.html',
    styleUrls: ['./messageboxviewer.component.scss']
})
export class MessageboxViewerComponent implements OnInit, OnDestroy {
    displayedColumns: Array<string> = ['hasAnnex', 'isImportant', 'sender', 'destination', 'description']; 
    isLoading: boolean = false;
    isSessionActive: boolean = false;
    subscribeSessionCreated: any;
    subscribeSessionDropped: any;
    @Input() boxType: string;
    messages$: Array<Message>;
    constructor(private store: Store<fromAppState.AppState>,
        private medikitExtensionService: MedikitExtensionService) { }

    ngOnInit(): void {
        if (this.medikitExtensionService.getEhealthSession() !== null) {
            this.isSessionActive = true;
        }

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

    ngOnDestroy(): void {
        this.subscribeSessionCreated.unsubscribe();
        this.subscribeSessionDropped.unsubscribe();
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
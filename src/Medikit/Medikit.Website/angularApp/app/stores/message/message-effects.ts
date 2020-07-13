import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { ActionTypes, SearchMessages } from './message-actions';
import { MessageService } from './services/message.service';

@Injectable()
export class MessageEffects {
    constructor(
        private actions$: Actions,
        private messageService: MessageService,
    ) { }

    @Effect()
    searchMessages$ = this.actions$
        .pipe(
            ofType(ActionTypes.SEARCH_MESSAGES),
            mergeMap((evt: SearchMessages) => {
                if (evt.boxType === 'inbox') {
                    return this.messageService.searchInboxMessages(evt.startIndex, evt.endIndex, evt.samlAssertion)
                        .pipe(
                            map(messages => { return { type: ActionTypes.MESSAGES_LOADED, messages: messages }; }),
                            catchError(() => of({ type: ActionTypes.ERROR_SEARCH_MESSAGES }))
                        );
                }

                return this.messageService.searchSentboxMessages(evt.startIndex, evt.endIndex, evt.samlAssertion)
                    .pipe(
                        map(messages => { return { type: ActionTypes.MESSAGES_LOADED, messages: messages }; }),
                        catchError(() => of({ type: ActionTypes.ERROR_SEARCH_MESSAGES }))
                    );
            }
            )
    );
}
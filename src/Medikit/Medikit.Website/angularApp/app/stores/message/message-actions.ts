import { Action } from '@ngrx/store';
import { Message } from './models/message';

export enum ActionTypes {
    SEARCH_MESSAGES = "[Message] SEARCH_MESSAGES",
    MESSAGES_LOADED = "[Patient] MESSAGES_LOADED",
    ERROR_SEARCH_MESSAGES = "[Patient] ERROR_SEARCH_MESSAGES"

}

export class SearchMessages implements Action {
    readonly type = ActionTypes.SEARCH_MESSAGES;
    constructor(public startIndex: number, public endIndex: number, public boxType: string, public samlAssertion: string) { }
}

export class MessagesLoaded implements Action {
    readonly type = ActionTypes.MESSAGES_LOADED;
    constructor(public messages: Array<Message>) { }
}

export type ActionsUnion = SearchMessages | MessagesLoaded;
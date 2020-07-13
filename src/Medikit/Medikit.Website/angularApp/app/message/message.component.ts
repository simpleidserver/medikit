import { Component, OnDestroy, OnInit, ViewEncapsulation } from "@angular/core";

@Component({
    selector: 'message-component',
    templateUrl: './message.component.html',
    styleUrls: [ './message.component.scss' ],
    encapsulation: ViewEncapsulation.None
})
export class MessageComponent implements OnInit, OnDestroy {
    ngOnInit(): void {
    }

    ngOnDestroy(): void {
    }
}

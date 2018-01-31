import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-component',
    templateUrl: './app.component.html',
    styleUrls: [
        './app.component.scss'
    ],
    encapsulation: ViewEncapsulation.None,
    animations: [
        trigger('indicatorRotate', [
            state('collapsed', style({ transform: 'rotate(0deg)' })),
            state('expanded', style({ transform: 'rotate(180deg)' })),
            transition('expanded <=> collapsed',
                animate('225ms cubic-bezier(0.4,0.0,0.2,1)')
            ),
        ])
    ]
})

export class AppComponent implements OnInit {
    expanded: boolean = false;

    constructor(private translate: TranslateService, private router: Router) {
        translate.setDefaultLang('fr');
        translate.use('fr');
    }

    ngOnInit(): void {
        this.router.events.subscribe((opt: any) => {
            var url = opt.urlAfterRedirects;
            if (!url || this.expanded) {
                return;
            }

            if (url.startsWith('/prescription')) {
                this.expanded = true;
            }
        });
    }

    chooseLanguage(lng: string) {
        this.translate.use(lng);
    }

    togglePrescriptions() {
        this.expanded = !this.expanded;
    }
}

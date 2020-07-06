import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ViewEncapsulation, OnInit, OnDestroy } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { authConfig } from './auth.config';
import { MedikitExtensionService } from './infrastructure/services/medikitextension.service';
import { MatSnackBar, MatDialogRef, MatDialog } from '@angular/material';
import { FormGroup, FormControl, Validators } from '@angular/forms';

const medikitLanguageName: string = "medikitLanguage";

@Component({
    selector: 'auth-pin',
    templateUrl: './auth.pin.html'
})
export class AuthPinDialog {
    authenticateFormGroup: FormGroup = new FormGroup({
        pin: new FormControl('', [
            Validators.required,
            Validators.pattern('^[0-9]{4}$')    
        ])
    });
    constructor(public dialogRef: MatDialogRef<AuthPinDialog>) { }

    close(): void {
        this.dialogRef.close();
    }

    authenticate() {
        if (this.authenticateFormGroup.invalid) {
            return;
        }

        var pin = this.authenticateFormGroup.controls['pin'].value;
        this.dialogRef.close({ pin : pin});
    }
}

@Component({
    selector: 'install-extension-help',
    templateUrl: './install-extension-help.html',
    styleUrls: [
        './install-extension-help.scss'
    ],
})
export class InstallExtensionHelpDialog {
    windowsUrl: string = process.env.REDIRECT_URL + "/assets/images/windows-os.svg";
    chromeUrl: string = process.env.REDIRECT_URL + "/assets/images/chrome.svg";
    constructor(public dialogRef: MatDialogRef<InstallExtensionHelpDialog>) { }

    close(): void {
        this.dialogRef.close();
    }
}

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
export class AppComponent implements OnInit, OnDestroy {
    logoUrl: string = process.env.REDIRECT_URL + "/assets/images/logo-no-text.svg";
    prescriptionsLogoUrl: string = process.env.REDIRECT_URL + "/assets/images/medical-prescription.png";
    medicalFilesLogoUrl: string = process.env.REDIRECT_URL + "/assets/images/patient-folder.png";
    sessionValidityHour: number = 0;
    isExtensionInstalled: boolean = false;
    isEhealthSessionActive: boolean = false;
    sessionCheckTimer: any;
    extensionCheckTimer: any;
    isConnected: boolean = false;
    name: string;
    roles: any;
    expanded: boolean = false;
    activeLanguage: string = 'en';

    constructor(private route: Router,
        private translate: TranslateService,
        private router: Router,
        private oauthService: OAuthService,
        private medikitExtensionService: MedikitExtensionService,
        private snackBar: MatSnackBar,
        private dialog: MatDialog) {
        this.activeLanguage = sessionStorage.getItem(medikitLanguageName);
        if (!this.activeLanguage) {
            this.activeLanguage = 'en';
        }

        translate.setDefaultLang(this.activeLanguage);
        translate.use(this.activeLanguage);
        this.configureOAuth();
    }

    openHelpDialog() {
        this.dialog.open(InstallExtensionHelpDialog, {
            width: '600px'
        });        
    }

    configureOAuth() {
        this.oauthService.configure(authConfig);
        this.oauthService.tokenValidationHandler = new JwksValidationHandler();
        let self = this;
        this.oauthService.loadDiscoveryDocumentAndTryLogin({
            disableOAuth2StateCheck: true
        });
        this.sessionCheckTimer = setInterval(function () {
            if (!self.oauthService.hasValidIdToken()) {
                self.oauthService.logOut();
                self.route.navigate(["/"]);
            }
        }, 3000);
    }

    login() {
        this.oauthService.customQueryParams = {
            'prompt': 'login'
        };
        this.oauthService.initImplicitFlow();
        return false;        
    }

    chooseSession() {
        this.oauthService.customQueryParams = {
            'prompt': 'select_account'
        };
        this.oauthService.initImplicitFlow();
        return false;
    }

    disconnect() {
        this.oauthService.logOut();
        this.router.navigate(['/home']);
        return false;
    }

    init() {
        var claims: any = this.oauthService.getIdentityClaims();
        if (!claims) {
            this.isConnected = false;;
            return;
        }

        this.name = claims.given_name;
        this.roles = claims.role;
        this.isConnected = true;
    }

    createEHealthSessionWithCertificate() {
        this.medikitExtensionService.createEhealthSessionWithCertificate().subscribe(_ => {
            if (_) {
                this.refreshEHealthSession();
                this.snackBar.open(this.translate.instant('ehealth-session-created'), this.translate.instant('undo'), {
                    duration: 2000
                });
            } else {
                this.snackBar.open(this.translate.instant('ehealth-session-not-created'), this.translate.instant('undo'), {
                    duration: 2000
                });
            }
        });
    }

    createEHealthSessionWithEID() {
        const self = this;
        const dialogRef = this.dialog.open(AuthPinDialog, {
            width: '400px'
        });
        dialogRef.afterClosed().subscribe(_ => {
            if (!_) {
                return;
            }

            self.medikitExtensionService.createEhealthSessionWithEID(_.pin).subscribe(_ => {
                if (_) {
                    self.refreshEHealthSession();
                    self.snackBar.open(this.translate.instant('ehealth-session-created'), this.translate.instant('undo'), {
                        duration: 2000
                    });
                } else {
                    self.snackBar.open(this.translate.instant('ehealth-session-not-created'), this.translate.instant('undo'), {
                        duration: 2000
                    });
                }
            });
        });
    }

    dropEhealthSession() {
        this.medikitExtensionService.disconnect();
        this.isEhealthSessionActive = false;
    }

    refreshEHealthSession() {
        const self = this;
        self.medikitExtensionService.isExtensionInstalled().subscribe(_ => {
            self.isExtensionInstalled = _;
            if (self.isExtensionInstalled) {
                var session = self.medikitExtensionService.getEhealthSession();
                if (session === null) {
                    self.isEhealthSessionActive = false;
                } else {
                    self.isEhealthSessionActive = true;
                    var notOnOrAfter: Date =  new Date(session['not_onorafter']);
                    var notBefore: Date = new Date();
                    var diff = Math.round(((notOnOrAfter.getTime() - notBefore.getTime()) / 36e5) * 100) / 100;
                    self.sessionValidityHour = diff;
                }
            }
        });
    }

    chooseLanguage(lng: string) {
        this.translate.use(lng);
        sessionStorage.setItem(medikitLanguageName, lng);
        this.activeLanguage = lng;
    }

    ngOnInit(): void {
        const self = this;
        this.init();
        this.oauthService.events.subscribe((e: any) => {
            if (e.type === "logout") {
                this.isConnected = false;
            } else if (e.type === "token_received") {
                this.init();
            }
        });

        this.router.events.subscribe((opt: any) => {
            var url = opt.urlAfterRedirects;
            if (!url || this.expanded) {
                return;
            }

            if (url.startsWith('/prescription')) {
                this.expanded = true;
            }
        });

        this.refreshEHealthSession();
        this.extensionCheckTimer = setInterval(function () {
            self.refreshEHealthSession();
        }, 3000);
    }

    ngOnDestroy(): void {
        if (this.extensionCheckTimer) {
            clearInterval(this.extensionCheckTimer);
        }

        if (this.sessionCheckTimer) {
            clearInterval(this.sessionCheckTimer);
        }
    }
}

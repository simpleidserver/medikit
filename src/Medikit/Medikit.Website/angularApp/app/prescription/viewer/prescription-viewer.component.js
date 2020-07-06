var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Input, ViewChild, ElementRef } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { formatDate } from "@angular/common";
import { PharmaPrescription } from "@app/stores/pharmaprescription/models/pharma-prescription";
var PDFObject = require('pdfobject');
var jsPDF = require('jspdf');
var JsBarCode = require('jsbarcode');
var PrescriptionViewerComponent = (function () {
    function PrescriptionViewerComponent(translateService) {
        this.translateService = translateService;
        this._nbPrescriptions = 1;
        this._jsPdf = null;
    }
    PrescriptionViewerComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.translateService.onLangChange.subscribe(function () {
            _this.refresh();
        });
    };
    Object.defineProperty(PrescriptionViewerComponent.prototype, "nbPrescriptions", {
        get: function () {
            return this._nbPrescriptions;
        },
        set: function (nb) {
            this._nbPrescriptions = nb;
            if (this._prescription) {
                this.refresh();
            }
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(PrescriptionViewerComponent.prototype, "pharmaPrescription", {
        get: function () {
            return this._prescription;
        },
        set: function (prescription) {
            if (!prescription) {
                return;
            }
            this._prescription = prescription;
            this.refresh();
        },
        enumerable: true,
        configurable: true
    });
    PrescriptionViewerComponent.prototype.refresh = function () {
        var margin = 10;
        var barCodeWidth = 150;
        var barCodeHeight = 60;
        var heightRowMedicalPrescription = 40;
        var medicalPrescriptionRowMargin = 5;
        var medicalPrescriptionPaddingBottom = 10;
        var medicalPrescriptionCellWidth = 30;
        var txtPaddingLeft = 5;
        var txtPaddingTop = 10;
        JsBarCode(this.barCodeCanvas.nativeElement, "BE" + this._prescription.Type + this._prescription.Id);
        var url = this.barCodeCanvas.nativeElement.toDataURL("image/jpeg");
        this._jsPdf = new jsPDF('p', 'px');
        var pageWidth = this._jsPdf.internal.pageSize.getWidth();
        var widthPrescription = (pageWidth / 2) - margin;
        this._jsPdf.setFontSize(10);
        for (var i = 1; i <= this._nbPrescriptions; i++) {
            var leftOffset = margin;
            var topOffset = margin;
            if (i % 2 === 0) {
                leftOffset += widthPrescription;
            }
            else if (i >= 2) {
                this._jsPdf.addPage();
            }
            var barCodeLeftOffset = ((widthPrescription - barCodeWidth) / 2) + leftOffset;
            var barCodeTopOffset = topOffset + 5;
            this._jsPdf.rect(leftOffset, topOffset, widthPrescription, 70);
            this._jsPdf.addImage(url, 'JPEG', barCodeLeftOffset, barCodeTopOffset, barCodeWidth, barCodeHeight);
            topOffset += 70;
            this._jsPdf.rect(leftOffset, topOffset, widthPrescription, 30);
            this._jsPdf.setFontStyle("bold");
            this._jsPdf.text(this.translateService.instant("electronic-prescription-proof"), leftOffset + 30, topOffset + 18);
            topOffset += 30;
            this._jsPdf.rect(leftOffset, topOffset, widthPrescription, 50);
            this._jsPdf.setFontStyle("normal");
            var strArr = this._jsPdf.splitTextToSize(this.translateService.instant("electronic-prescription-instruction"), widthPrescription - txtPaddingLeft);
            this._jsPdf.text(strArr, leftOffset + txtPaddingLeft, topOffset + txtPaddingTop);
            topOffset += 50;
            this._jsPdf.rect(leftOffset, topOffset, widthPrescription, 30);
            if (this._prescription.Prescriber) {
                this._jsPdf.text([
                    this.translateService.instant("prescriber") + " : " + this._prescription.Prescriber.Firstname + " " + this._prescription.Prescriber.Lastname,
                    this.translateService.instant("inami-number") + " : " + this._prescription.Prescriber.RizivNumber
                ], leftOffset + txtPaddingLeft, topOffset + txtPaddingTop);
            }
            topOffset += 30;
            this._jsPdf.rect(leftOffset, topOffset, widthPrescription, 30);
            this._jsPdf.text([
                this.translateService.instant("beneficiary") + " : " + this._prescription.Patient.firstname + " " + this._prescription.Patient.lastname,
                this.translateService.instant("niss") + " : " + this._prescription.Patient.niss
            ], leftOffset + txtPaddingLeft, topOffset + txtPaddingTop);
            topOffset += 30;
            this._jsPdf.rect(leftOffset, topOffset, widthPrescription, 30);
            this._jsPdf.setFontStyle("bold");
            this._jsPdf.text(this.translateService.instant("electronic-prescription-content"), leftOffset + 30, topOffset + 18);
            topOffset += 30;
            this._jsPdf.rect(leftOffset, topOffset, widthPrescription, heightRowMedicalPrescription * medicalPrescriptionRowMargin + medicalPrescriptionPaddingBottom);
            for (var index = 1; index <= 5; index++) {
                this._jsPdf.rect(leftOffset + medicalPrescriptionRowMargin, topOffset, widthPrescription - (medicalPrescriptionRowMargin * 2), heightRowMedicalPrescription);
                this._jsPdf.line(leftOffset + medicalPrescriptionRowMargin + medicalPrescriptionCellWidth, topOffset, leftOffset + medicalPrescriptionRowMargin + medicalPrescriptionCellWidth, topOffset + heightRowMedicalPrescription);
                this._jsPdf.setFontStyle("bold");
                this._jsPdf.text("" + index + "", leftOffset + medicalPrescriptionRowMargin + 10, topOffset + 20);
                this._jsPdf.setFontStyle("normal");
                if (index <= this.pharmaPrescription.DrugsPrescribed.length) {
                    var drugPrescribed = this.pharmaPrescription.DrugsPrescribed[index - 1];
                    this._jsPdf.text(this.translate(drugPrescribed.PackageNames), leftOffset + medicalPrescriptionRowMargin + medicalPrescriptionCellWidth + txtPaddingLeft, topOffset + txtPaddingTop);
                    if (drugPrescribed.Posology) {
                        this._jsPdf.text(this.convertPosologyToDisplayedText(drugPrescribed.Posology), leftOffset + medicalPrescriptionRowMargin + medicalPrescriptionCellWidth + txtPaddingLeft, topOffset + txtPaddingTop + txtPaddingTop);
                    }
                }
                topOffset += heightRowMedicalPrescription;
            }
            topOffset += medicalPrescriptionPaddingBottom;
            this._jsPdf.rect(leftOffset, topOffset, widthPrescription, 40);
            strArr = this._jsPdf.splitTextToSize(this.translateService.instant("electronic-prescription-warning"), widthPrescription - txtPaddingLeft);
            this._jsPdf.text(strArr, leftOffset + txtPaddingLeft, topOffset + txtPaddingTop);
            topOffset += 40;
            this._jsPdf.rect(leftOffset, topOffset, widthPrescription, 30);
            this._jsPdf.text(this.translateService.instant("electronic-prescription-date", { date: formatDate(this._prescription.CreateDateTime, 'dd/MM/yyyy', 'en-US') }), leftOffset + txtPaddingLeft, topOffset + txtPaddingTop);
            topOffset += 30;
            this._jsPdf.rect(leftOffset, topOffset, widthPrescription, 30);
            this._jsPdf.text(this.translateService.instant("electronic-prescription-end-execution-date", { date: formatDate(this._prescription.EndExecutionDate, 'dd/MM/yyyy', 'en-US') }), leftOffset + txtPaddingLeft, topOffset + txtPaddingTop);
        }
        PDFObject.embed(this._jsPdf.output("bloburl"), '#preview-pane');
    };
    PrescriptionViewerComponent.prototype.translate = function (translations) {
        var _this = this;
        var filteredTranslations = translations.filter(function (t) { return t.Language === _this.translateService.currentLang; });
        if (filteredTranslations.length === 0) {
            return this.translateService.instant('unknown');
        }
        return filteredTranslations[0].Value;
    };
    PrescriptionViewerComponent.prototype.convertPosologyToDisplayedText = function (posology) {
        var freeText = posology;
        if (freeText != null) {
            return freeText.Content;
        }
        return "";
    };
    __decorate([
        ViewChild('barcode'),
        __metadata("design:type", ElementRef)
    ], PrescriptionViewerComponent.prototype, "barCodeCanvas", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Number),
        __metadata("design:paramtypes", [Number])
    ], PrescriptionViewerComponent.prototype, "nbPrescriptions", null);
    __decorate([
        Input(),
        __metadata("design:type", PharmaPrescription),
        __metadata("design:paramtypes", [PharmaPrescription])
    ], PrescriptionViewerComponent.prototype, "pharmaPrescription", null);
    PrescriptionViewerComponent = __decorate([
        Component({
            selector: 'prescription-viewer',
            templateUrl: './prescription-viewer.component.html'
        }),
        __metadata("design:paramtypes", [TranslateService])
    ], PrescriptionViewerComponent);
    return PrescriptionViewerComponent;
}());
export { PrescriptionViewerComponent };
//# sourceMappingURL=prescription-viewer.component.js.map
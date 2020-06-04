﻿import { Component, Input, OnInit, ViewChild, ElementRef } from "@angular/core";
import { PharmaPrescription } from "@app/prescription/models/pharma-prescription";
import { TranslateService } from "@ngx-translate/core";
import { Translation } from "@app/infrastructure/Translation";
import { PharmaPosology, PharmaPosologyFreeText } from "../models/pharma-posology";
import { formatDate } from "@angular/common";
var PDFObject = require('pdfobject');
var jsPDF = require('jspdf');
var JsBarCode = require('jsbarcode');

@Component({
    selector: 'prescription-viewer',
    templateUrl: './prescription-viewer.component.html'
})
export class PrescriptionViewerComponent implements OnInit {
    private _prescription: PharmaPrescription;
    private _nbPrescriptions: number = 1;
    @ViewChild('barcode')
    barCodeCanvas: ElementRef<HTMLCanvasElement>;

    constructor(private translateService: TranslateService) { }

    ngOnInit(): void {
        this.translateService.onLangChange.subscribe(() => {
            this.refresh();
        });
    }

    @Input()
    set nbPrescriptions(nb: number) {
        this._nbPrescriptions = nb;
    }


    get nbPrescriptions(): number {
        return this._nbPrescriptions;
    }

    @Input()
    set pharmaPrescription(prescription: PharmaPrescription) {
        if (!prescription) {
            return;
        }

        this._prescription = prescription;
        this.refresh();
    }

    get pharmaPrescription(): PharmaPrescription {
        return this._prescription;
    }

    private refresh() : void {
        const margin: number = 10;
        const barCodeWidth: number = 150;
        const barCodeHeight: number = 60;
        const heightRowMedicalPrescription: number = 40;
        const medicalPrescriptionRowMargin: number = 5;
        const medicalPrescriptionPaddingBottom: number = 10;
        const medicalPrescriptionCellWidth: number = 30;
        const txtPaddingLeft: number = 5;
        const txtPaddingTop: number = 10;
        JsBarCode(this.barCodeCanvas.nativeElement, "BE" + this._prescription.Type + this._prescription.Id);
        var url = this.barCodeCanvas.nativeElement.toDataURL("image/jpeg");
        var doc = new jsPDF('p', 'px');
        var pageWidth = doc.internal.pageSize.getWidth();
        var widthPrescription = (pageWidth / 2) - margin;
        doc.setFontSize(10);
        for (var i = 1; i <= this._nbPrescriptions; i++) {
            var leftOffset = margin;
            var topOffset = margin;
            if (i % 2 === 0) {
                leftOffset += widthPrescription;
            }
            else if (i >= 2) {
                doc.addPage();
            }

            var barCodeLeftOffset = ((widthPrescription - barCodeWidth) / 2) + leftOffset;
            var barCodeTopOffset = topOffset + 5;
            doc.rect(leftOffset, topOffset, widthPrescription, 70);
            doc.addImage(url, 'JPEG', barCodeLeftOffset, barCodeTopOffset, barCodeWidth, barCodeHeight);
            topOffset += 70;
            doc.rect(leftOffset, topOffset, widthPrescription, 30);
            doc.setFontStyle("bold");
            doc.text(this.translateService.instant("electronic-prescription-proof"), leftOffset + 30, topOffset + 18);
            topOffset += 30;
            doc.rect(leftOffset, topOffset, widthPrescription, 50);
            doc.setFontStyle("normal");
            var strArr = doc.splitTextToSize(this.translateService.instant("electronic-prescription-instruction"), widthPrescription - txtPaddingLeft)
            doc.text(strArr, leftOffset + txtPaddingLeft, topOffset + txtPaddingTop);
            topOffset += 50;
            doc.rect(leftOffset, topOffset, widthPrescription, 30);
            if (this._prescription.Prescriber) {
                doc.text([
                    this.translateService.instant("prescriber") + " : " + this._prescription.Prescriber.Firstname + " " + this._prescription.Prescriber.Lastname,
                    this.translateService.instant("inami-number") + " : " + this._prescription.Prescriber.RizivNumber
                ], leftOffset + txtPaddingLeft, topOffset + txtPaddingTop);
            }

            topOffset += 30;
            doc.rect(leftOffset, topOffset, widthPrescription, 30);
            doc.text([
                this.translateService.instant("beneficiary") + " : " + this._prescription.Patient.firstname + " " + this._prescription.Patient.lastname,
                this.translateService.instant("niss") + " : " + this._prescription.Patient.niss
            ], leftOffset + txtPaddingLeft, topOffset + txtPaddingTop);
            topOffset += 30;
            doc.rect(leftOffset, topOffset, widthPrescription, 30);
            doc.setFontStyle("bold");
            doc.text(this.translateService.instant("electronic-prescription-content"), leftOffset + 30, topOffset + 18);
            topOffset += 30;
            doc.rect(leftOffset, topOffset, widthPrescription, heightRowMedicalPrescription * medicalPrescriptionRowMargin + medicalPrescriptionPaddingBottom);
            for (var index = 1; index <= 5; index++) {
                doc.rect(leftOffset + medicalPrescriptionRowMargin, topOffset, widthPrescription - (medicalPrescriptionRowMargin * 2), heightRowMedicalPrescription);
                doc.line(leftOffset + medicalPrescriptionRowMargin + medicalPrescriptionCellWidth, topOffset, leftOffset + medicalPrescriptionRowMargin + medicalPrescriptionCellWidth, topOffset + heightRowMedicalPrescription);
                doc.setFontStyle("bold");
                doc.text("" + index + "", leftOffset + medicalPrescriptionRowMargin + 10, topOffset + 20);
                doc.setFontStyle("normal");
                if (index <= this.pharmaPrescription.DrugsPrescribed.length) {
                    const drugPrescribed = this.pharmaPrescription.DrugsPrescribed[index - 1];
                    doc.text(this.translate(drugPrescribed.PackageNames), leftOffset + medicalPrescriptionRowMargin + medicalPrescriptionCellWidth + txtPaddingLeft, topOffset + txtPaddingTop);
                    if (drugPrescribed.Posology) {
                        doc.text(this.convertPosologyToDisplayedText(drugPrescribed.Posology), leftOffset + medicalPrescriptionRowMargin + medicalPrescriptionCellWidth + txtPaddingLeft, topOffset + txtPaddingTop + txtPaddingTop);
                    }
                }

                topOffset += heightRowMedicalPrescription;
            }

            topOffset += medicalPrescriptionPaddingBottom;
            doc.rect(leftOffset, topOffset, widthPrescription, 40);
            strArr = doc.splitTextToSize(this.translateService.instant("electronic-prescription-warning"), widthPrescription - txtPaddingLeft);
            doc.text(strArr, leftOffset + txtPaddingLeft, topOffset + txtPaddingTop);
            topOffset += 40;
            doc.rect(leftOffset, topOffset, widthPrescription, 30);
            doc.text(this.translateService.instant("electronic-prescription-date", { date: formatDate(this._prescription.CreateDateTime, 'dd/MM/yyyy', 'en-US') }), leftOffset + txtPaddingLeft, topOffset + txtPaddingTop);
            topOffset += 30;
            doc.rect(leftOffset, topOffset, widthPrescription, 30);
            doc.text(this.translateService.instant("electronic-prescription-end-execution-date", { date: formatDate(this._prescription.EndExecutionDate, 'dd/MM/yyyy', 'en-US') }), leftOffset + txtPaddingLeft, topOffset + txtPaddingTop);
        }

        PDFObject.embed(doc.output("bloburl"), '#preview-pane');
    }

    private translate(translations: Translation[]): String {
        var filteredTranslations = translations.filter((t) => t.Language === this.translateService.currentLang);
        if (filteredTranslations.length === 0) {
            return this.translateService.instant('unknown');
        }

        return filteredTranslations[0].Value;
    }

    private convertPosologyToDisplayedText(posology: PharmaPosology): String {
        var freeText = posology as PharmaPosologyFreeText;
        if (freeText != null) {
            return freeText.Content;
        }

        return "";
    }
}
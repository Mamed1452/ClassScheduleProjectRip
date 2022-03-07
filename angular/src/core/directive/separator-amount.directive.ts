import { Directive, ElementRef, HostListener, OnInit } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
    selector: '[SeparatorAmount]'
})
export class SeparatorAmountDirective implements OnInit {

    private el: HTMLInputElement;

    constructor(elementRef: ElementRef, private formControl: NgControl) {
        this.el = elementRef.nativeElement;
    }

    ngOnInit() {
        this.formControl.control.setValue(
            this.formatNumber(this.formControl.control.value)
        );
    }

    @HostListener('keyup', ['$event.target.value'])
    onKeyUp(value: string) {
        this.formControl.control.setValue(this.formatNumber(value));
    }

    formatNumber(s: string): string {
        if (s) {
            let numberOnly = s.replace(/[^0-9.]/g, '');
            return new Intl.NumberFormat('en-En', { style: 'decimal' }).format(
                Number(numberOnly)
            );
        }
    }
}

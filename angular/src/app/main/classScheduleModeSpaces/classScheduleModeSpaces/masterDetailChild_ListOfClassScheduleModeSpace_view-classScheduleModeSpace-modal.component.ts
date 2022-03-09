import {AppConsts} from "@shared/AppConsts";
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetClassScheduleModeSpaceForViewDto, ClassScheduleModeSpaceDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'masterDetailChild_ListOfClassScheduleModeSpace_viewClassScheduleModeSpaceModal',
    templateUrl: './masterDetailChild_ListOfClassScheduleModeSpace_view-classScheduleModeSpace-modal.component.html'
})
export class MasterDetailChild_ListOfClassScheduleModeSpace_ViewClassScheduleModeSpaceModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetClassScheduleModeSpaceForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetClassScheduleModeSpaceForViewDto();
        this.item.classScheduleModeSpace = new ClassScheduleModeSpaceDto();
    }

    show(item: GetClassScheduleModeSpaceForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }
    
    

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}

import { Component, EventEmitter, Injector, Input, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

import { UploaderOptions, UploadFile, UploadInput, UploadOutput, humanizeBytes } from 'ngx-uploader';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent extends AppComponentBase implements OnInit {
  @Input() uploadUrl = '';
  @Input() data: any;
  @Input() isMultiple: any;
  @Output() uploadComplete: EventEmitter<any> = new EventEmitter<any>();
  @Output() file: EventEmitter<any> = new EventEmitter<any>();
  @Output() fileRemove: EventEmitter<any> = new EventEmitter<any>(); 

  options: UploaderOptions;
  formData: FormData;
  files: UploadFile[]=[];
  uploadInput: EventEmitter<UploadInput>;
  humanizeBytes: Function; //فکر کنم بایت به مگابایت تبدیل می کرده که به کاربر نشون بدیم
  dragOver: boolean;
  acceptedInputFile: string;


  constructor(
    private injector: Injector,
    private domSanitizer: DomSanitizer
  ) {
    super(injector);
    // allowedContentTypes: ['image/png', 'image/jpg', 'image/gif', 'image/jpeg', 'application/pdf','application/vnd.openxmlformats-officedocument.spreadsheetml.sheet','application/vnd.ms-excel','application/msword','application/vnd.openxmlformats-officedocument.wordprocessingml.document']
    this.options = {
      concurrency: 1, maxFileSize: 1024 * 1024 * 5, allowedContentTypes:
        ['image/jpeg',
          'image/png',
          'application/pdf',
          'image/jpg',
          'application/msword',
          'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
          'application/vnd.ms-excel',
          'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet']
    };
    this.files = []; // local uploading files array
    this.uploadInput = new EventEmitter<UploadInput>(); // input events, we use this to emit data to ngx-uploader
    this.humanizeBytes = humanizeBytes;
    // با این دستور می تونی کاری کنی کاربر نتونه جز فایل های مجاز انتخاب کنه
    this.acceptedInputFile = this.options.allowedContentTypes.join(',');
  }

  onUploadOutput(output: UploadOutput): void {
    switch (output.type) {
      case 'allAddedToQueue':
        if (this.files.length > 1) {
          // this.notify.warn('فقط یک فایل مجاز به ارسال می باشد.');
          // this.removeFile(this.files[this.files.length - 1].id);
        } else if (this.files.length === 1) {
          // اگر خواستیم بلافاصله بعد از انتخاب فایل آپلود شود
          // this.startUpload();
        }
        //  هر جایی که داری داخل
        //file
        //پوش میکنی باید به پدرش هم خبر بدی
        // در واقه هر جایی که files تغییر کرد
        // اضاف شد - حذف شد یا هر چیزی
        break;
      case 'addedToQueue':
        if (typeof output.file !== 'undefined') {
          //برای وقتی که اخرین فایل رو پوش میکنه
          //برای این که چند تا پریویو ایمیج داشته باشیم
          this.files.push(output.file);
          this.previewImage(this.files[this.files.length - 1]);
          this.file.emit(this.files);
        }
        break;
      case 'uploading':
        if (typeof output.file !== 'undefined') {
          // update current data in files array for uploading file
          const index = this.files.findIndex(
            file => typeof output.file !== 'undefined'
              && file.id === output.file.id);
          this.files[index] = output.file;
        }
        break;
      case 'removed': //وقتی کاربر دستی حذف میکنه
        // remove file from array when removed
        this.files = this.files.filter((file: UploadFile) => file !== output.file);
        break;
      case 'rejected':
        if (this.options.allowedContentTypes.indexOf(output.file.type) < 0) {
          this.notify.error(this.l('FileTypeError'));
        } else if (output.file.size > this.options.maxFileSize) {
          this.notify.error(this.l('NotAllowed'));
        }
        break;
      case 'dragOver':
        this.dragOver = true;
        break;
      case 'dragOut':
        this.dragOver = false;
        break;
      case 'drop':
        this.dragOver = false;
        break;
      case 'done':
        if (output.file.responseStatus === 200 && output.file.response.success) {
          this.notify.success('Successfully');
          this.uploadComplete.emit(output);
          //اینی که نوشتی وقتی آپلود میشه داری خبر میدی به پدرش
          //من گفتم وقتی فایل انتخاب میکنه خبر بده

        } else if (output.file.responseStatus === 200 && output.file.response.success === false) {
          let msg = output.file.response.messages ? output.file.response.messages : this.l('ErrorMes');
          this.notify.error(msg);
        } else {
          this.notify.error(this.l('ErrorForUpload'));
        }
        // setTimeout(() => {
        //   this.removeAllFiles();
        // }, 3000);
        // this.files = [];
        break;
    }
  }

  startUpload(formData = null): void {
    const event: UploadInput = {
      type: 'uploadAll',
      url: this.uploadUrl,
      method: 'POST',
      data: formData ? formData : this.data,
      fieldName: 'file'
    };
    this.uploadInput.emit(event);
  }

  cancelUpload(id: string): void {
    this.uploadInput.emit({ type: 'cancel', id: id });
    this.file.emit(this.files);
  }

  removeFile(id: string): void {
    this.uploadInput.emit({ type: 'remove', id: id });
    this.file.emit(this.files);
  }

  removeAllFiles(): void {
    this.uploadInput.emit({ type: 'removeAll' });
    this.file.emit(this.files);
  }

  ngOnInit(): void {
  }

  previewImage(file) {
    const fileType = file.name.split('.').slice(-1)[0];
    switch (fileType) {
      case 'pdf' :
        file.filePath = 'assets/images/pdf.png';
        break;
      case 'jpg' :
        file.filePath = 'assets/images/jpg.png';
        break;
      case 'png' :
        file.filePath = 'assets/images/png.png';
        break;
      case 'jpeg' :
        file.filePath = 'assets/images/jpeg.png';
        break;
      case 'doc' :
        file.filePath = 'assets/images/doc.png';
        break;
      case 'docx' :
        file.filePath = 'assets/images/docx.png';
        break;
      case 'xls' :
        file.filePath = 'assets/images/xls.png';
        break;
      case 'xlsx' :
        file.filePath = 'assets/images/xlsx.png';
        break;
      default :
        const reader = new FileReader();
        reader.onload = () => {
          file.filePath = this.domSanitizer.bypassSecurityTrustUrl(reader.result as string);
        };
        //نیتیو فایل ما یه ارایس که فایل اصلیمون توشه
        reader.readAsDataURL(file.nativeFile);
    }
    //فایل ریدر یک ابجکته
  }

  delete(file) {
    this.removeFile(file.id);
    //  this.files.splice(i,1)
    this.file.emit(this.files);
  }
}

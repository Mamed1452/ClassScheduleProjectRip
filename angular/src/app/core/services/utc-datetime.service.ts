import { DatePipe } from '@angular/common';
import { Injectable } from '@angular/core';
import * as moment from 'moment';

//هرجا تاریخ و ساعت دارم با این به یوتی سی تبدیلش میکنمو میدمش به بک اند
@Injectable({
    providedIn: 'root'
})
export class UtcDateTimeService {


    constructor() {

    }

    /**
     * تبدیل تاریخ و ساعت دریافتی از کاربر به فرمت یو تی سی
     * @param localDate تاریخ
     * @param localTime ساعت
     * @returns تاریخ و ساعت به صورت UTC
     */
    convertLocalDateTimeToUTC(localDate, localTime): UtcDateTime {
        let dateTime = moment.utc(localDate).startOf('day').format('YYYY-MM-DD') + ' ' + localTime;
        let utcDate = new DatePipe('en').transform(dateTime, 'YYYY-MM-dd', 'UTC');
        let utcTime = new DatePipe('en').transform(dateTime, 'HH:mm', 'UTC');
        return {
            utcDate: utcDate,
            utcTime: utcTime,
            utcDateTimeString: moment.utc(utcDate + ' ' + utcTime).toISOString()
        };
    }


    /**
     * تبدیل تاریخ و ساعت یو تی سی به لوکال کاربر
     * @param datetime  UTC date time
     * @returns تاریخ و ساعت به لوکال کاربر
     */
    convertUtcToLocalDateTime(datetime): LocalDateTime {
        const theDate = new Date(Date.parse(datetime));
        let local = moment.utc(theDate).local();
        let localTime = this.persianArabicNumberToEn(local.format('HH:mm'));
        let localDate = this.persianArabicNumberToEn(local.startOf('day').format('YYYY-MM-DD'));
        return {
            localDate: localDate,
            localTime: localTime,
            localDateTimeString: local.toISOString()
        };
    }



    /**
     * تبدیل تاریخ و ساعت یو تی سی به لوکال کاربر
     * @param datetime  UTC date time
     * @returns تاریخ و ساعت به لوکال کاربر
     */
    convertUtcToDateTime(datetime): any {
        const theDate = new Date(Date.parse(datetime));
        let local = moment.utc(theDate).local();
        let localTime = this.persianArabicNumberToEn(local.format('HH:mm'));
        let localDate = this.persianArabicNumberToEn(local.startOf('day').format('YYYY-MM-DD'));
        return local;
    }


    // برای تبدیل اعداد فارسی (یا عربی) به انگلیسی
    persianArabicNumberToEn(number): string {
        let persianNumbers = [/۰/g, /۱/g, /۲/g, /۳/g, /۴/g, /۵/g, /۶/g, /۷/g, /۸/g, /۹/g];
        let arabicNumbers = [/٠/g, /١/g, /٢/g, /٣/g, /٤/g, /٥/g, /٦/g, /٧/g, /٨/g, /٩/g];
        let str = number.toString();
        for (let i = 0; i < 10; i++) {
            str = str.replace(persianNumbers[i], i).replace(arabicNumbers[i], i);
        }
        return str;
    }

}


export class UtcDateTime {
    utcDate: string;
    utcTime: string;
    utcDateTimeString: string;
}


export class LocalDateTime {
    localDate: string;
    localTime: string;
    localDateTimeString: string;
}

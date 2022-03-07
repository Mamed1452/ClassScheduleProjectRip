import { BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapLocaleMappingService } from 'assets/ngx-bootstrap/ngx-bootstrap-locale-mapping.service';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ThemeHelper } from '@app/shared/layout/themes/ThemeHelper';
import FA from 'assets/ngx-bootstrap/fa-ir.js';

export class NgxBootstrapDatePickerConfigService {

    static getDaterangepickerConfig(): BsDaterangepickerConfig {
        return Object.assign(new BsDaterangepickerConfig(), {
            containerClass: 'theme-' + NgxBootstrapDatePickerConfigService.getTheme()
        });
    }

    static getDatepickerConfig(): BsDatepickerConfig {
        return Object.assign(new BsDatepickerConfig(), {
            containerClass: 'theme-' + NgxBootstrapDatePickerConfigService.getTheme()
        });
    }

    static getTheme(): string {
        return ThemeHelper.getTheme();
    }

    static getDatepickerLocale(): BsLocaleService {
        let localeService = new BsLocaleService();
        localeService.use(abp.localization.currentLanguage.name);
        return localeService;
    }

    static registerNgxBootstrapDatePickerLocales(): Promise<boolean> {
        if (abp.localization.currentLanguage.name === 'en') {
            return Promise.resolve(true);
        }

        let supportedLocale = new NgxBootstrapLocaleMappingService().map(abp.localization.currentLanguage.name).toLowerCase();
        let moduleLocaleName = new NgxBootstrapLocaleMappingService().getModuleName(abp.localization.currentLanguage.name);


        if (supportedLocale === 'fa-ir') {
            return new Promise<any>((resolve, reject) => {
                import(`../../assets/ngx-bootstrap/fa-ir.js`)
                    .then(module => {
                        defineLocale('fa-ir', module[`faIRLocale`]);
                        resolve(true);
                    }, reject);
            });
        } else {
            return new Promise<any>((resolve, reject) => {
                import(`ngx-bootstrap/chronos/esm5/i18n/${supportedLocale}.js`)
                    .then(module => {
                        defineLocale(abp.localization.currentLanguage.name.toLowerCase(),
                            module[`${moduleLocaleName}Locale`]);
                        resolve(true);
                    }, reject);
            });
        }
    }
}

import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {HttpClient, HttpHeaders, HttpErrorResponse} from '@angular/common/http';
import {Observable, of, throwError} from 'rxjs';
import {map, catchError} from 'rxjs/operators';

import {ToastrService} from 'ngx-toastr';

export class ApiResult<T> {
  isSuccess: boolean;
  data: T;
  messages: string;
  statusCode: number;
}

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(
    private http: HttpClient,
    private router: Router,
    private toastrService: ToastrService
  ) {
  }

  /**
   * post data to api Rest
   * @param url
   * @param dataToPost
   */
  post(url: string, dataToPost: any = ''): Observable<ApiResult<any>> {
    return this.http.post<ApiResult<any>>(url, dataToPost, httpOptions)
      .pipe(
        map(response => {
          return response;
        })
      );
  }

  /**
   * post form with multipart/form data
   * auto convert json to form data
   * @param url
   * @param formData as json
   */
  postFormData(url: string, formData: any = ''): Observable<ApiResult<any>> {
    const postForm = new FormData();
    for (const prop in formData) {
      if (formData[prop].name) {
        postForm.append(prop, formData[prop], formData[prop].name);
      } else {
        postForm.append(prop, formData[prop]);
      }

    }
    return this.http.post<ApiResult<any>>(url, postForm)
      .pipe(
        map(response => {
          return response;
        })
      );
  }

  /**
   * get data from api Rest
   * @param url
   * @param params
   */
  get(url: string, params: any = ''): Observable<ApiResult<any>> {
    return this.http.get<ApiResult<any>>(url, {params: params})
      .pipe(
        // retry(3), // retry a failed request up to 3 times
        map(response => {
          return response;
        }),
        //  tap(data => console.log('fetched http successfully')),
        //  catchError(this.handleError)
      );
  }

  /**
   * delete from server
   * @param url
   * @param params
   */
  delete(url: string, params: any = ''): Observable<ApiResult<any>> {
    // const urlParam = url + JSON.stringify(params);
    return this.http.delete<ApiResult<any>>(url, {params: params})
      .pipe(
        // retry(3), // retry a failed request up to 3 times
        map(response => {
          return response;
        })
      );
  }

  /**
   * دریافت تاریخ جاری از سرور
   * worldtimeapi
   * */
  // getCurrentDateTime(): Observable<any> {
  //   return this.http.get<any>(ServerApis.getCurrentDateTime)
  //     .pipe(
  //       // retry(3), // retry a failed request up to 3 times
  //       map(response => {
  //         return response;
  //       })
  //     );
  // }
  //
  // getCaptchaImage(param: any): Observable<any> {
  //   return this.http.get(ServerApis.getCaptchaImage, {params: param, responseType: 'blob'});
  // }

  /**
   * تبدیل تاریخ به فرمت
   * YYYY/MM/DD
   */
  formatDate(date) {
    // tslint:disable-next-line:prefer-const
    let d = new Date(date),
      month = '' + (d.getMonth() + 1),
      day = '' + d.getDate(),
      // tslint:disable-next-line:prefer-const
      year = d.getFullYear();

    if (month.length < 2) {
      month = '0' + month;
    }
    if (day.length < 2) {
      day = '0' + day;
    }

    return [year, month, day].join('-');
  }

  /**
   * دانلود فرم درخواست ثبت نام کاریابی
   */
  // downloadPlacementRegisterRequestFile() {
  //   let url = 'assets/register-request.pdf';
  //   if (ServerApis.baseUrl === '/') {
  //     url = '/ui/' + url;
  //   }
  //
  //   return this.http.get(url, {responseType: 'blob'}).pipe(
  //     map((res) => {
  //       return res;
  //     })
  //   );
  // }
}

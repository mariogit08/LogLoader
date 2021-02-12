import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Log } from './log';
import { Result } from './result';
import { NotificationService } from '../notification.service';
import { LogFilter } from './logFilter';

@Injectable({
  providedIn: 'root'
})
export class LogService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  private apiURL = "http://localhost:55590/api";

  constructor(private httpClient: HttpClient, private notifyService: NotificationService) {

  }

  getAll(): Observable<Log[]> {
    return this.httpClient.get<Log[]>(this.apiURL + '/logs/')
      .pipe(
        catchError((error) => this.errorHandler(error, this.notifyService))
      )
  }

  getByFilter(logFilter: LogFilter): Observable<Result<Log[]>> {
    let httpParams = new HttpParams();
    httpParams = logFilter.ip ? httpParams.set('IP', logFilter?.ip): httpParams;
    httpParams = logFilter.userAgentProduct ? httpParams.set('userAgentProduct', logFilter?.userAgentProduct?.toString()) : httpParams;
    httpParams = logFilter.fromHour ? httpParams.set('fromHour', logFilter?.fromHour?.toString()): httpParams;
    httpParams = logFilter.fromMinute ? httpParams.set('fromMinute', logFilter?.fromMinute?.toString()) : httpParams;
    httpParams = logFilter.toHour ? httpParams.set('toHour', logFilter?.toHour?.toString()) : httpParams;
    httpParams = logFilter.toMinute ? httpParams.set('toMinute', logFilter?.toMinute?.toString()) : httpParams;
    
    let options = { params: httpParams };    

    return this.httpClient.get<Result<Log[]>>(this.apiURL + '/logs/filter', options)
      .pipe(
        catchError((error) => this.errorHandler(error, this.notifyService))
      )
  }

  create(log): Observable<Result<any>> {
    return this.httpClient.post<Result<any>>(this.apiURL + '/logs/', JSON.stringify(log), this.httpOptions)
      .pipe(
        catchError((error) => this.errorHandler(error, this.notifyService))
      )
  }

  createByFile(fileToUpload: File): Observable<boolean> {
    const endpoint = this.apiURL + '/logs/batch';
    const formData: FormData = new FormData();
    formData.append('file', fileToUpload);
    console.log(fileToUpload.name)
    return this.httpClient.post<any>(endpoint, formData).pipe(
      catchError((error) => this.errorHandler(error, this.notifyService))
    )
  }

  find(id): Observable<Result<Log>> {
    return this.httpClient.get<Result<Log>>(this.apiURL + '/logs/' + id)
      .pipe(
        catchError((error) => this.errorHandler(error, this.notifyService))
      )
  }

  update(id, log): Observable<Log> {
    return this.httpClient.put<Log>(this.apiURL + '/logs/' + id, JSON.stringify(log), this.httpOptions)
      .pipe(
        catchError((error) => this.errorHandler(error, this.notifyService))
      )
  }

  delete(id) {
    return this.httpClient.delete<Log>(this.apiURL + '/logs/' + id, this.httpOptions)
      .pipe(
        catchError((error) => this.errorHandler(error, this.notifyService))
      )
  }


  errorHandler(error, notificationService: NotificationService) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }

    if (notificationService instanceof NotificationService) {
      notificationService.showError("Operation failed", "TReuters LogLoader")
    }
    console.log(typeof notificationService)
    console.log(errorMessage);

    return throwError(errorMessage);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Log } from './log';
import { Result } from './result';
import { NotificationService } from '../notification.service';

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

  constructor(private httpClient: HttpClient, private notifyService: NotificationService) 
  { 
    
  }

  getAll(): Observable<Log[]> {
    return this.httpClient.get<Log[]>(this.apiURL + '/logs/')
      .pipe(
        catchError((error)=>this.errorHandler(error,this.notifyService))
      )
  }

  getByFilter(params): Observable<Log[]> {
    let httpParams = new HttpParams();   

    let options = params.IP ? { params: httpParams.set('IP', params.IP) } : {};
    options = params.startDate ? { params: httpParams.set('startDate', params.startDate) } : options;
    options = params.finalDate ? { params: httpParams.set('finalDate', params.finalDate) } : options;

    return this.httpClient.get<Log[]>(this.apiURL + '/logs/', options)
      .pipe(
        catchError((error)=>this.errorHandler(error,this.notifyService))
      )
  }

  create(log): Observable<Result<any>> {
    return this.httpClient.post<Result<any>>(this.apiURL + '/logs/', JSON.stringify(log), this.httpOptions)
      .pipe(
        catchError((error)=>this.errorHandler(error,this.notifyService))
      )
  }

  createByFile(fileToUpload: File): Observable<boolean> {
    const endpoint = this.apiURL + '/logs/batch';
    const formData: FormData = new FormData();
    formData.append('file', fileToUpload);
    console.log(fileToUpload.name)
    return this.httpClient.post<any>(endpoint, formData).pipe(
      catchError((error)=>this.errorHandler(error,this.notifyService))
    )
  }

  find(id): Observable<Log> {
    return this.httpClient.get<Log>(this.apiURL + '/logs/' + id)
      .pipe(
        catchError((error)=>this.errorHandler(error,this.notifyService))
      )
  }

  update(id, log): Observable<Log> {
    return this.httpClient.put<Log>(this.apiURL + '/logs/' + id, JSON.stringify(log), this.httpOptions)
      .pipe(
        catchError((error)=>this.errorHandler(error,this.notifyService))
      )
  }

  delete(id) {
    return this.httpClient.delete<Log>(this.apiURL + '/logs/' + id, this.httpOptions)
      .pipe(
        catchError((error)=>this.errorHandler(error,this.notifyService))
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
      notificationService.showError("Operation failed","TReuters LogLoader")
    }
    console.log(typeof notificationService)
    console.log(errorMessage);
  
    return throwError(errorMessage);
  }
}

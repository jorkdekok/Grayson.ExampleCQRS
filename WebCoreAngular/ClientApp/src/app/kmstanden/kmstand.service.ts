import {throwError as observableThrowError,  Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { IKmStand } from './kmstand.model';

@Injectable()
export class KmstandService {
  private _kmstandenUrl = 'http://localhost:6002/api/v1/KmStanden?page=0&pageSize=10';

  constructor(private _http: HttpClient) { }

  getKmstanden(): Observable<IKmStand[]> {
    return this._http.get<IKmStand[]>(this._kmstandenUrl)
    .pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
      catchError(this.handleError));
  }

  private handleError(err: HttpErrorResponse) {
    console.error(err.message);
    return observableThrowError(err.message);
  }
}

class KmStand {
  stand: number;
  datum: Date;
}

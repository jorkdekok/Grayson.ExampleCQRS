import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import { IKmStand } from './kmstand.model';

@Injectable()
export class KmstandService {
  private _kmstandenUrl = 'http://localhost:6002/api/v1/KmStanden?page=0&pageSize=10';

  constructor(private _http: HttpClient) { }

  getKmstanden(): Observable<IKmStand[]> {
    return this._http.get<IKmStand[]>(this._kmstandenUrl)
      .do(data => console.log('All: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  private handleError(err: HttpErrorResponse) {
    console.error(err.message);
    return Observable.throw(err.message);
  }
}

class KmStand {
  stand: number;
  datum: Date;
}

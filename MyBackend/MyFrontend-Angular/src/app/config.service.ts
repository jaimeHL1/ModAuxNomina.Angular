import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EMPTY, Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ConfigJson } from './models/config-json.model';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private configJson: ConfigJson | undefined;

  constructor(private http: HttpClient) { }


  loadConfig(): Observable<any> {
    return this.http.get<ConfigJson>('./assets/config.json')
      .pipe(
        tap((configJson: ConfigJson ) => this.configJson = configJson),
        catchError(error => {
          console.error('Error al cargar el config.json', error);
          return throwError(() => new Error('Error al cargar el config.json'));
        })
    );
  }

  getConfig() {
    return this.configJson;
  }
}
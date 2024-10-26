import { Injectable } from '@angular/core';
import { ConfigService } from '../../config.service';
import { ConfigJson } from '../../models/config-json.model';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, firstValueFrom, throwError } from 'rxjs'; 
import { catchError } from 'rxjs/operators';

import { AnticipoCompleto } from '../../models/anticipos/anticipo-completo.model'   // '../models/anticipo.model';  
import { AmortizacionDetalle } from '../../models/anticipos/amortizacion-detalle';
import { AnticipoHistorial } from '../../models/anticipos/anticipo-historial';


@Injectable({
  providedIn: 'root'
})
export class AnticiposService {
  configJson: ConfigJson | undefined;

  constructor(private configService: ConfigService
    , private http: HttpClient) {
    this.configJson = this.configService.getConfig();
  }  
 
  private getApiUrl(): string {
    // Verificar si configJson está inicializada, si no error 
    if (!this.configJson) {
      throw new Error("Configuración no cargada");
    }
    return `${this.configJson.rutaApi}/Anticipos`;
  }

  obtenerAnticiposPorEstado(idEstado?: number, idAnticipo?: number): Observable<AnticipoCompleto[]> {   
    let url = `${this.getApiUrl()}/ListaAnticiposPorEstado/`;
  
    // Si idEstado no es undefined, lo agregamos a la URL
    if (idEstado !== undefined) {
      url += `${idEstado}/`;
    }
  
    if (idAnticipo !== undefined) {
      url += `${idAnticipo}/`;
    }
  
    return this.http.get<AnticipoCompleto[]>(url);
  }

  obtenerAnticipoPorId(idAnticipo?: number): Observable<AnticipoCompleto> {  
    const url = `${this.getApiUrl()}/RecuperarAnticipoPorId/${idAnticipo ?? ''}`;
    return this.http.get<AnticipoCompleto>(url);
  }

  obtenerAmortizacionesPorAnticipo(idAnticipo: number): Observable<AmortizacionDetalle[]> { 
    const url = `${this.getApiUrl()}/AmortizacionDetalle/${idAnticipo}`;
    return this.http.get<any[]>(url);
  }

  obtenerHistorialPorAnticipo(idAnticipo: number): Observable<AnticipoHistorial[]> { 
    const url = `${this.getApiUrl()}/ObtenerHistoricoAnticipo/${idAnticipo}`;
    return this.http.get<AnticipoHistorial[]>(url);
  } 

  insertarAnticipo(anno: string, mes: string, dni: string, cdClasnm: string, pagas: number, usuarioAlta: string): Observable<any> {
    const url = `${this.getApiUrl()}/InsertarAnticipo/${anno}/${mes}/${dni}/${cdClasnm}/${pagas}/${usuarioAlta}`; 

    return this.http.post<any>(url, null).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = 'Ocurrió un error inesperado.';
        if (error.status === 400) {
          errorMessage = 'Datos inválidos.';
        } else if (error.status === 500) {
          errorMessage = 'Error interno del servidor.';
        }
        return throwError(() => new Error(errorMessage));
      })
    );
  }  
  borrarAnticipo(idAnticipo: number): Observable<any> {
    const url = `${this.getApiUrl()}/BorrarAnticipo/${idAnticipo}`;
    return this.http.delete<any>(url).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = 'Error al intentar borrar el anticipo.';
        if (error.status === 400) {
          errorMessage = 'No se puede borrar el anticipo.';
        } else if (error.status === 404) {
          errorMessage = 'Anticipo no encontrado.';
        } else if (error.status === 500) {
          errorMessage = 'Error interno del servidor.';
        }
        return throwError(() => new Error(errorMessage));
      })
    );
  }
}

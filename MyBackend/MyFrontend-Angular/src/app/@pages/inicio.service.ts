import { Injectable } from '@angular/core';
import { ConfigService } from '../config.service';
import { ConfigJson } from '../models/config-json.model';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { EmpleadoOracle } from '../models/oracle/empleadoOracle';
 

@Injectable({
  providedIn: 'root'
})
export class InicioService {
  configJson: ConfigJson | undefined;

  constructor(private configService: ConfigService
    , private http: HttpClient) {
    this.configJson = this.configService.getConfig();
  }

  /**
   * Obtiene datos básicos de empleadoOracle
   * @param dni  
   * @returns 
   */
  async obtenerEmpleado(dni: any): Promise<EmpleadoOracle> {
    let parametros = `/${dni}`;
    let rutaApi = `${this.configJson?.rutaApi}/Oracle/Empleado`
    return firstValueFrom(
      this.http.get<EmpleadoOracle>(rutaApi + parametros)
    );
  }
 

}

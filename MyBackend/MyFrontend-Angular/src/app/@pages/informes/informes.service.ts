import { Injectable } from '@angular/core';
import { ConfigService } from '../../config.service';
import { ConfigJson } from '../../models/config-json.model';
import { HttpClient } from '@angular/common/http';
import { Observable, firstValueFrom } from 'rxjs';
import { CategoriaDTO, InformeDTO, ParametroDTO } from '../../models/informes/informes';
import { ResultadoInformeDTO } from '../../models/informes/ResultadoInforme';



@Injectable({
  providedIn: 'root'
})
export class InformesService {
  configJson: ConfigJson | undefined;

  constructor(private configService: ConfigService
    , private http: HttpClient) {
    this.configJson = this.configService.getConfig();
  }


  //#region Llamadas REST
  obtenerCategoriasInformes(): CategoriaDTO[] {
    return [
      {
        idCategoria: 1, descripcion: "Informes Mensuales", informes: [
          { idInforme: 1, descripcion: "Colegio Huérfanos", parametroDTOs: null },
          { idInforme: 2, descripcion: "Costes de Personal", parametroDTOs: null },
          { idInforme: 3, descripcion: "Var.Desc.No Formalizables - Anticipos (Básicas - Mes anterior)", parametroDTOs: null },
          { idInforme: 4, descripcion: "S.G. Relaciones Institucionales - Gesprodes", parametroDTOs: null },
          { idInforme: 5, descripcion: "Oficina Presupuestaria - Funcionarios en el extranjero", parametroDTOs: null },
          { idInforme: 6, descripcion: "Consulta Productividades - RJ", parametroDTOs: null }
        ]
      },
      {
        idCategoria: 2, descripcion: "Efectivos", informes: [
          { idInforme: 7, descripcion: "Laborales", parametroDTOs: null },
          { idInforme: 8, descripcion: "Régimen de afiliación y unidad", parametroDTOs: null },
          { idInforme: 9, descripcion: "Funcionarios (Secretaría de Estado de Presupuestos y Gastos)", parametroDTOs: null },
        ]
      },
      {
        idCategoria: 3, descripcion: "DARETRI", informes: [
          { idInforme: 10, descripcion: "Resumen DARETRI", parametroDTOs: null }
        ]
      },
      {
        idCategoria: 4, descripcion: "FEDER", informes: [
          { idInforme: 11, descripcion: "FEDER - Unidades", parametroDTOs: null },
          { idInforme: 12, descripcion: "Feder - IR Funcionarios", parametroDTOs: null }
        ]
      }
    ];
  }

  obtenerParametrosInforme(idInformeEntorno: number): ParametroDTO[] {
    return [
      { idParametro: 1, obligatorio: true, texto: "Fecha Inicio", valor: "" },
      { idParametro: 2, obligatorio: true, texto: "Fecha Fin", valor: "" },
    ];
  }

  obtenerInforme(idInformeEntorno: number, parametros: ParametroDTO[]): ResultadoInformeDTO | null {
    return null;
  }
  //#endregion

}

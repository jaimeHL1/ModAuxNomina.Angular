import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError, firstValueFrom, of } from 'rxjs';
import { ConfigJson } from './models/config-json.model'; // Importar la interfaz
import { ConfigService } from './config.service';
import { Router } from '@angular/router';
import { SharedService } from './@pages/shared.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  configJson: ConfigJson | undefined;
  resultadoApi:string |undefined;
  moduloActual:string |undefined;
  iconoModulo:string |undefined;

  iconoAnadir:string |undefined;
  iconoEliminar:string |undefined;  


  constructor(private http: HttpClient
              ,private configService: ConfigService
              ,private router: Router
              ,private sharedService: SharedService) { }

  ngOnInit(){
      this.configJson = this.configService.getConfig();    
      console.log ("app.component Método ngOnInit()"); 
      this.moduloActual="Inicio";
      let url = window.location.href.toLowerCase();
      if (url.indexOf('/administracion') > 0) {
        this.moduloActual="Administración";
        this.iconoModulo="administracion";
      }
      else if (url.indexOf('/anticipos') > 0) {
        this.moduloActual="Anticipos";
        this.iconoModulo="anticipos";
      }
      else if (url.indexOf('/bajas') > 0) {
        this.moduloActual="Bajas Seguridad Social";
        this.iconoModulo="segsoci";
      }
      else if (url.indexOf('/informes') > 0) {
        this.moduloActual="Consultas";
        this.iconoModulo="consultas";
      }

      this.iconoAnadir='<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M256 8C119 8 8 119 8 256s111 248 248 248 248-111 248-248S393 8 256 8zm144 276c0 6.6-5.4 12-12 12h-92v92c0 6.6-5.4 12-12 12h-56c-6.6 0-12-5.4-12-12v-92h-92c-6.6 0-12-5.4-12-12v-56c0-6.6 5.4-12 12-12h92v-92c0-6.6 5.4-12 12-12h56c6.6 0 12 5.4 12 12v92h92c6.6 0 12 5.4 12 12v56z"/></svg>';
  }
  
  navegarAModulo(modulo:string) {  
    switch(modulo){      
      case "Administracion":
        this.iconoModulo="administracion";
        this.moduloActual="Administracion";
        this.router.navigate(["/administracion"]);
        break;
      case "Anticipos":
        this.iconoModulo="anticipos";
        this.moduloActual="Anticipos";
        this.router.navigate(["/anticipos"]);
        break;
      case "ConsultaNomina":
        let url = this.configJson?.rutaConsultaNomina; // Cambia esta URL por la que necesites
        window.open(url, '_blank');
        break;
      // case "GestionProductividad":
      //   let url2 = this.configJson?.rutaGestionProductividad; // Cambia esta URL por la que necesites
      //   window.open(url2, '_blank');
      //   break;
      case "Consultas":
        this.iconoModulo="consultas";
        this.moduloActual="Consultas";
        this.router.navigate(["/informes"]);
        break;

      case "SegSocial":
        this.iconoModulo="segsoci";
        this.moduloActual="Bajas Seguridad Social";
        this.router.navigate(["/bajas"]);
        break;
    }   
  } 


  async llamarAController(){
    this.resultadoApi = await this.sharedService.llamadaController();   
  }

}
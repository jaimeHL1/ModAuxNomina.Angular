import { Injectable } from '@angular/core';
import { ConfigService } from '../config.service';
import { HttpClient } from '@angular/common/http';
import { ConfigJson } from '../models/config-json.model';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  configJson: ConfigJson | undefined;
  constructor(private configService: ConfigService
    , private http: HttpClient) {
    this.configJson = this.configService.getConfig();
  }


  async llamadaController(): Promise<string> {
    let rutaApi = `${this.configJson?.rutaApi}/ModAux`
    return firstValueFrom(
      this.http.get<string>(rutaApi)
    );
  }



  descargarFichero(nombre: string, contentType: string, contenidoBase64: string) {
    // Arreglo para poder descargar ficheros en base64 en IE
    // https://stackoverflow.com/questions/43070627/internet-explorer-fails-opening-a-pdf-string-file
    // if (window.navigator && window.navigator.msSaveOrOpenBlob) {
    //     // IE, Edge
    //     var byteCharacters = atob(contenidoBase64);
    //     var byteNumbers = new Array(byteCharacters.length);
    //     for (var i = 0; i < byteCharacters.length; i++) {
    //         byteNumbers[i] = byteCharacters.charCodeAt(i);
    //     }
    //     var byteArray = new Uint8Array(byteNumbers);
    //     var blob = new Blob([byteArray], { type: contentType });
    //     window.navigator.msSaveOrOpenBlob(blob, nombre);
    // } else {
    // Otros navegadores
    //Generamos un TAG con las propiedades del documento
    var link = document.createElement("a");
    link.href = "data:" + contentType + ";base64," + contenidoBase64;
    link.download = nombre;
    link.target = "_blank";
    //Agregamos el TAG para posteriormente eliminarlo una vez hacemos click sobre él
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    // }
  }

}

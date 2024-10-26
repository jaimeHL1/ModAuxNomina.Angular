import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { LoaderService } from './loader.service';

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {
  //Inyecta el LoaderService para controlar la visualización del loader.
  constructor(private loaderService: LoaderService) {}

  /**
   * req: La solicitud HTTP.
   * next: El manejador de la siguiente solicitud HTTP.
   */
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // modificamos request (envío al servidor)
    req = this.modificarRequestHttp(req);

    this.loaderService.show();
    

    //Pasa la solicitud HTTP al siguiente manejador en la cadena de interceptores.
    return next.handle(req)   
      .pipe(
        //Usa el operador finalize para ocultar el loader cuando la solicitud HTTP se completa, se cancela o se produce un error.
        finalize(() => this.loaderService.hide())
      );
  }


   //#endregion
  //Cambios en cabecera HTTP
  modificarRequestHttp(req:HttpRequest<any>) {
    return req.clone({
      withCredentials: true
      // headers: req.headers
      //   .set('NIFAUTENTICA', (this.appService.usuarioConectado) ? this.appService.usuarioConectado.nif : '')
      //   .set('PERFILAUTORIZA', (this.appService.usuarioConectado) ? this.appService.usuarioConectado.perfilAutoriza : '')
      //   .set('REQUEST_MIDDLEWARE', '1')
    });
  }
  //#endregion

}
import { APP_INITIALIZER, NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

import { ConfigService } from './config.service';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { LoaderComponent } from  './@pages/loader/loader.component';
import { LoaderInterceptor } from './@pages/loader/loader.interceptor';
import { LoaderService } from './@pages/loader/loader.service';
import { registerLocaleData } from '@angular/common';
import localeEs from '@angular/common/locales/es';
import localeDeExtra from '@angular/common/locales/extra/es';
import { AdministracionComponent } from './@pages/administracion/administracion.component';
import { AnticiposComponent } from './@pages/anticipos/anticipos.component';
import { DetalleAnticipoComponent } from './@pages/anticipos/detalle/detalle-anticipo/detalle-anticipo.component';
import { DetalleCalculoComponent } from './@pages/anticipos/detalle/detalle-calculo/detalle-calculo.component';
import { FiltroAnticipoComponent } from './@pages/anticipos/filtro/filtro-anticipo/filtro-anticipo.component';
import { BajasComponent } from './@pages/bajas/bajas.component';
import { HistorialAnticipoComponent } from './@pages/anticipos/detalle/historial-anticipo/historial-anticipo.component';
import { NuevoAnticipoComponent } from './@pages/anticipos/nuevo-anticipo/nuevo-anticipo.component';

registerLocaleData(localeEs, 'es-ES',localeDeExtra);

//initializeApp: Función de inicialización que carga la configuración de la aplicación antes de que se inicie. Usa firstValueFrom para convertir 
//un observable en una promesa y maneja errores en la carga de la configuración.
export function initializeApp(configService: ConfigService) {
  return async () => {
    try {
      await firstValueFrom(configService.loadConfig());
    } catch (error) {
      console.error('Fallo en inicializeApp:', error);
      alert('Fallo al inicializar la aplicación');
    }
  };
}

@NgModule({
  declarations: [
    AppComponent, 
    LoaderComponent, 
    AdministracionComponent, 
    AnticiposComponent, 
    DetalleAnticipoComponent, 
    DetalleCalculoComponent, 
    FiltroAnticipoComponent, 
    BajasComponent, 
    HistorialAnticipoComponent, 
    NuevoAnticipoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,  
    FormsModule
  ],
  providers: [
    // Inicializa la aplicación llamando a initializeApp antes de que la aplicación arranque.
    {provide: APP_INITIALIZER, useFactory: initializeApp, deps: [ConfigService], multi: true},
    //Registra LoaderInterceptor como un interceptor HTTP. useClass especifica la clase que implementa el interceptor y multi: true permite múltiples interceptores.
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
    provideHttpClient( withInterceptorsFromDi()   ),
    //: Establece el ID de localización a 'de-DE' para la aplicación.
    { provide: LOCALE_ID, useValue: 'es-ES' },
    // DecimalPipe,
    LoaderService,
 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

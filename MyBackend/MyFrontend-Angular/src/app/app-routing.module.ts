import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministracionComponent } from './@pages/administracion/administracion.component';
import { AnticiposComponent } from './@pages/anticipos/anticipos.component';
import { DetalleAnticipoComponent } from './@pages/anticipos/detalle/detalle-anticipo/detalle-anticipo.component';
import { BajasComponent } from './@pages/bajas/bajas.component';
import { InformesComponent } from './@pages/informes/informes.component';


const routes: Routes = [
  { path: 'anticipos', component: AnticiposComponent }, 
  // { path: 'detalle-anticipo/:id', component: DetalleAnticipoComponent }, // Ruta para detalle de un anticipo
  { path: 'administracion', component: AdministracionComponent },   
  { path: 'bajas', component: BajasComponent }, 
  { path: 'informes', component: InformesComponent },   
  { path: '**', redirectTo: '', pathMatch: 'full' }  // Ruta para manejar 404
 ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

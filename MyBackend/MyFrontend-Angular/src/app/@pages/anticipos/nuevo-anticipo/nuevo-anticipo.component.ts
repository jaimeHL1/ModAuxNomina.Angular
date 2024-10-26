import { Component } from '@angular/core';
import { InicioService } from '../../inicio.service';  
import { AnticiposService } from './../anticipos.service'; 
import { EmpleadoOracle } from '../../../models/oracle/empleadoOracle';

@Component({
  selector: 'app-nuevo-anticipo',
  templateUrl: './nuevo-anticipo.component.html',
  styleUrl: './nuevo-anticipo.component.scss'
})
export class NuevoAnticipoComponent {
  anticipoDni: string = '';
  anticipoPagas: string = '';
  empleado: EmpleadoOracle | null = null;
  modalActiva: boolean = true;
  mensajeError: string = '';

  constructor(private inicioService: InicioService, private anticiposService: AnticiposService) { }

   // Método que se ejecuta al salir del campo del DNI
   async onDniBlur() {
    if (this.anticipoDni.length === 8) { // Validar que el DNI tenga 8 caracteres
      try {
        this.empleado = await this.inicioService.obtenerEmpleado(this.anticipoDni);
        console.log('Traza Empleado encontrado:', this.empleado); 
      } catch (error) {
        console.error('Error al obtener el empleado:', error);
        this.empleado = null;
      }
    }
  }
 
    grabarAnticipo() {
      // Salimos de la función si no hay empleado
    if (!this.empleado) {  
      this.mensajeError = 'No se ha encontrado información del empleado.';
      return;  
    }
    

    this.mensajeError = '';  // Limpiamos cualquier mensaje de error previo
    const claseNomina = this.empleado.cdclasnm;
    const dni  = this.empleado.cddni;
    const usuarioAlta = 'testUser';  // Usuario por defecto para la prueba
    const fechaActual = new Date();
    const anno = fechaActual.getFullYear().toString();  
    const mes = (fechaActual.getMonth() + 1).toString().padStart(2, '0');
    const anticipoPagas = parseInt(this.anticipoPagas); 

    this.anticiposService.insertarAnticipo(anno, mes, dni, claseNomina, anticipoPagas, usuarioAlta)
      .subscribe({
        next: (res) => { 
          alert(`Anticipo insertado con ID: ${res.identificador}`);
        },
        error: (err) => { 
          this.mensajeError = err.message;
        }
      });
    }
  
    grabarYSalirAnticipo() {
     
    }

    cerrarModal() {
      this.modalActiva = false;
    }
}

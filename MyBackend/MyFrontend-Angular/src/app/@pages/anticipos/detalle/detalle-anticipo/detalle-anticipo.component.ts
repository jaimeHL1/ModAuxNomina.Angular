import { Component, Input, OnInit } from '@angular/core';
import { AnticiposService } from '../../anticipos.service';
import { AnticipoCompleto } from '../../../../models/anticipos/anticipo-completo.model';  
import { AmortizacionDetalle } from '../../../../models/anticipos/amortizacion-detalle';

@Component({
  selector: 'app-detalle-anticipo',
  templateUrl: './detalle-anticipo.component.html',
  styleUrls: ['./detalle-anticipo.component.scss']
})
export class DetalleAnticipoComponent implements OnInit {
  @Input() anticipo!: AnticipoCompleto;  
  @Input() amortizaciones!: AmortizacionDetalle[];   
  spanFormula: string = ''; // Variable para almacenar la fórmula desglosada
  verDetalleCalculo: boolean = false; 
  verHistorialAnticipo: boolean = false;  

  verDetalleAnticipo: boolean = true;
  
  constructor( private anticiposService: AnticiposService) { }

  ngOnInit(): void {
    if (this.anticipo) {
      this.formulaDesglosada();
      this.cargarAmortizaciones(this.anticipo.idAnticipo);
    } 
  }
  
  cargarAmortizaciones(idAnticipo: number): void {
    console.log ("detalle-anticipo.component Método cargarAmortizaciones()");
    this.anticiposService.obtenerAmortizacionesPorAnticipo(idAnticipo).subscribe(
      (amortizaciones: AmortizacionDetalle[]) => {
        this.amortizaciones = amortizaciones;
      },
      (error: any) => {
        console.error('Error al cargar detalle del anticipo', error);
      }
    );
  }

  formulaDesglosada() { 
    if (this.anticipo) {
      if (['01', '02', '03'].includes(this.anticipo.claseNomina)) {
        this.spanFormula = '(((((Sueldo + Trienios + (1/6 Paga extra)) - %Irpf) * Pagas) / Cuotas) * Cuotas)';
      } else if (['05', '06'].includes(this.anticipo.claseNomina)) {
        this.spanFormula = '(((((Sueldo + Trienios + Comp. Ant.) - %Irpf) * Pagas) / Cuotas) * Cuotas)';
      } else {
        this.spanFormula = 'Fórmula no disponible'; // Manejo para otros casos
      }
    }
  }

  mostrarDetalleCalculo(): void {
    this.verDetalleCalculo = true;
    this.verHistorialAnticipo = false;
  } 

  mostrarHistoricoAnticipo(): void {
    this.verHistorialAnticipo = true;
    this.verDetalleCalculo = false;
  }

  cerrarModal() {
    //this.modalActiva = false;
    this.verDetalleAnticipo = false;
  }
  
}
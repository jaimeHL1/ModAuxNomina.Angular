import { Component, Input } from '@angular/core';
import { AnticipoCompleto } from '../../../../models/anticipos/anticipo-completo.model';

@Component({
  selector: 'app-detalle-calculo',
  templateUrl: './detalle-calculo.component.html',
  styleUrl: './detalle-calculo.component.scss'
})
export class DetalleCalculoComponent {
  @Input() anticipo!: AnticipoCompleto;

  sumaConceptos: number = 0;
  irpfAplicado: number = 0;
  multiplicadoPorPagas: number = 0;
  divididoEntreCuotas: number = 0;
  redondeadoSuperior: number = 0;
  multiplicadoPorNumCuotas: number = 0;

  constructor() { }

  ngOnInit(): void {
    console.log("detalle-calculo.component: Anticipo recibido", this.anticipo);
    this.calcularValores();
  }

  calcularValores(): void {
    // Paso 1: Suma de conceptos
    this.sumaConceptos = (this.anticipo?.sueldo || 0) + (this.anticipo?.trienios || 0) + (this.anticipo?.pagaExtra || 0);

    // Paso 2: Aplicar IRPF
    this.irpfAplicado = this.sumaConceptos * (1 - (this.anticipo?.irpf || 0) / 100);

    // Paso 3: Multiplicar por el número de pagas
    this.multiplicadoPorPagas = this.irpfAplicado * (this.anticipo?.numPagasSolicitadas || 0);

    // Paso 4: Dividir entre el número de cuotas
    this.divididoEntreCuotas = this.multiplicadoPorPagas / (this.anticipo?.numMesesDevolucion || 1);

    // Paso 5: Redondear al entero superior
    this.redondeadoSuperior = Math.ceil(this.divididoEntreCuotas);

    // Paso 6: Se multiplica por el nº de cuotas
    this.multiplicadoPorNumCuotas = this.redondeadoSuperior * (this.anticipo?.numMesesDevolucion || 1)
  }

}

import { Component, OnInit } from '@angular/core';
//mport { BajasService } from './Bajas.service';
import { Router } from '@angular/router';
//import { AmortizacionDetalle } from '../../models/anticipos/amortizacion-detalle';

@Component({
  selector: 'app-bajas',
  templateUrl: './bajas.component.html',
  styleUrl: './bajas.component.scss'
})

export class BajasComponent implements OnInit {
  // anticipos: AnticipoCompleto[] = [];
  // amortizaciones: AmortizacionDetalle[] = []; 
  // listaCentros: any[] = [];
  // verDetalleAnticipo: boolean = false;
  // anticipoSeleccionado: AnticipoCompleto | null = null; // Para guardar el anticipo seleccionado
  // searchTerm: string = '';

  //constructor(private anticiposService: AnticiposService) { }

  ngOnInit(): void {
    //this.loadAnticipos();
    console.log("Iniciando componente Bajas.component");
  }
  
}


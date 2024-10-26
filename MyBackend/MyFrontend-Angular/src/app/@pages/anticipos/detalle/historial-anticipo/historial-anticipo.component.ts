import { Component, Input, OnInit } from '@angular/core';
import { AnticipoHistorial } from '../../../../models/anticipos/anticipo-historial';
import { AnticipoCompleto } from '../../../../models/anticipos/anticipo-completo.model';
import { AnticiposService } from '../../anticipos.service';

@Component({
  selector: 'app-historial-anticipo',
  templateUrl: './historial-anticipo.component.html',
  styleUrl: './historial-anticipo.component.scss'
})
export class HistorialAnticipoComponent implements OnInit {
  @Input() anticipo!: AnticipoCompleto;
  
  historial: AnticipoHistorial[] = [];

  constructor(private anticiposService: AnticiposService) { }

  ngOnInit(): void {
    this.cargarHistorial();
  }

  cargarHistorial(): void {
    this.anticiposService.obtenerHistorialPorAnticipo(this.anticipo.idAnticipo).subscribe(
      (historial: AnticipoHistorial[]) => {
        this.historial = historial;
      },
      (error: any) => {
        console.error('Error al cargar detalle del anticipo', error);
      }
    );
  }

}

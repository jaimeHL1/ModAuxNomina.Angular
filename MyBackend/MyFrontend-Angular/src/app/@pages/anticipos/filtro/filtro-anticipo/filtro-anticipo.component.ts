import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-filtro-anticipo',
  templateUrl: './filtro-anticipo.component.html',
  styleUrl: './filtro-anticipo.component.scss'
})
export class FiltroAnticipoComponent {

  selectedNomina: string | null = null;
  selectedSituacion: string | null = null;
  filtroMesConcesion: string | null = null;
  filtroAnnoConcesion: string | null = null;
  filtroMesSolicitud: string | null = null;
  filtroAnnoSolicitud: string | null = null;
  annoActual: number = new Date().getFullYear();
  annoComienzo: number = 2020;

  // Lista nóminas
  nominas = [
    { CDCLASNM: '01', DSCLASNM: 'ALTOS CARGOS' },
    { CDCLASNM: '02', DSCLASNM: 'FUNCIONARIOS' },
    { CDCLASNM: '03', DSCLASNM: 'FUNCIONARIOS EN EL EXTRANJERO' },
    { CDCLASNM: '05', DSCLASNM: 'CONTRATADOS LABORALES' },
    { CDCLASNM: '06', DSCLASNM: 'CONTRATADOS LABORALES EN EL EXTRANJERO' }
  ];

  // Lista de Estados
  situaciones = [
    { codSit: '1', desSit: 'BORRADOR' },
    { codSit: '2', desSit: 'CERTIFICACION' },
    { codSit: '3', desSit: 'NEDAES' },
    { codSit: '4', desSit: 'CONCEDIDO' },
    { codSit: '5', desSit: 'ENAMORTIZACION' },
    { codSit: '6', desSit: 'AMORTIZADO' },
    { codSit: '7', desSit: 'CIERRE' },
  ];

  ngOnInit(): void {
    console.log('ngOnInit en filtro-anticipo.component');
  }

  @Input() verFiltroFechaConcesion: boolean = false;
  @Input() verFiltroFechaSolicitud: boolean = false;
  
  
  ngOnChanges(changes: SimpleChanges) {

    console.log('ngOnChanges en filtro-anticipo.component');
      
    if (changes['verFiltroFechaConcesion']) {
      console.log('changes verFiltroFechaConcesion ' + this.verFiltroFechaConcesion);
      // Lógica cuando cambia verFiltroFechaConcesion
      console.log('verFiltroFechaConcesion ha cambiado:', this.verFiltroFechaConcesion);
    }

    if (changes['verFiltroFechaSolicitud']) {
      // Lógica cuando cambia verFiltroFechaSolicitud
      console.log('verFiltroFechaSolicitud ha cambiado:', this.verFiltroFechaSolicitud);
    }
  }
  
  // Emitir la selección de nómina al componente padre (anticipos)
  @Output() filtroSeleccionadoNomina = new EventEmitter<string | null>();
  @Output() filtroSeleccionadoSituacion = new EventEmitter<string | null>(); 
  @Output() filtroFechaConcesion = new EventEmitter<{ mesConcesion: string | null, annoConcesion: string | null }>();
  @Output() filtroFechaSolicitud = new EventEmitter<{ mesSolicitud: string | null, annoSolicitud: string | null }>();

  obtenerDescripcionNomina(): string {
    const nomina = this.nominas.find(n => n.CDCLASNM === this.selectedNomina);
    return nomina ? nomina.DSCLASNM : '';
  }

  // Obtener la descripción de la situación seleccionada
  obtenerDescripcionSituacion(): string {
    const situacion = this.situaciones.find(s => s.codSit === this.selectedSituacion);
    return situacion ? situacion.desSit : '';
  }

  // Función para seleccionar la nómina
  seleccionarNomina(nomina: string | null) {
    this.selectedNomina = nomina;
    this.filtroSeleccionadoNomina.emit(nomina);
  }

  // Función para seleccionar la situación
  seleccionarSituacion(situacion: string | null) {
    this.selectedSituacion = situacion;
    this.filtroSeleccionadoSituacion.emit(situacion);
  } 

  seleccionarFechaConcesion(mesCon: string | null, annoCon: string | null) {
    this.filtroMesConcesion = mesCon;
    this.filtroAnnoConcesion = annoCon;
    this.filtroFechaConcesion.emit({ mesConcesion: mesCon, annoConcesion: annoCon }); 
  }

  seleccionarFechaSolicitud(mesSol: string | null, annoSol: string | null) {
    this.filtroMesSolicitud = mesSol;
    this.filtroAnnoSolicitud = annoSol
    this.filtroFechaSolicitud.emit({ mesSolicitud: mesSol, annoSolicitud: annoSol }); 
  }

  // Función para borrar el filtro
  borrarFiltroNomina() {
    this.selectedNomina = null;
    this.filtroSeleccionadoNomina.emit(null); // Emitir null cuando se borra el filtro
  }

  // Función para borrar el filtro de situación
  borrarFiltroSituacion() {
    this.selectedSituacion = null;
    this.filtroSeleccionadoSituacion.emit(null); // Emitir null cuando se borra el filtro
  }

  borrarFiltroFechaConcesion() {
    this.filtroMesConcesion = null;
    this.filtroAnnoConcesion = null;
    this.filtroFechaConcesion.emit({ mesConcesion: null, annoConcesion: null });
  }

  borrarFiltroFechaSolicitud() {
    this.filtroMesSolicitud = null;
    this.filtroAnnoSolicitud= null;
    this.filtroFechaSolicitud.emit({ mesSolicitud: null, annoSolicitud: null });
  }

}

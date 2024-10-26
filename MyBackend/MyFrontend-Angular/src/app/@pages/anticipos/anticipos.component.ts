import { Component, OnInit } from '@angular/core';
import { AnticiposService } from './anticipos.service';
import { AnticipoCompleto } from '../../models/anticipos/anticipo-completo.model';
import { AmortizacionDetalle } from '../../models/anticipos/amortizacion-detalle';


@Component({
  selector: 'app-anticipos',
  templateUrl: './anticipos.component.html',
  styleUrl: './anticipos.component.scss'
})

export class AnticiposComponent implements OnInit {
  anticipos: AnticipoCompleto[] = [];
  amortizaciones: AmortizacionDetalle[] = [];
  listaCentros: any[] = [];
  verDetalleAnticipo: boolean = false;
  verHistorialAnticipo: boolean = false;
  anticipoSeleccionado: AnticipoCompleto | null = null; // Para guardar el anticipo seleccionado
  textoFiltroLupa: string = ''; // Filtro por texto de lupa
  filtroNomina: string | null = null;
  filtroSituacion: string | null = null;
  filtroMesConcesion: string | null = null;
  filtroAnnoConcesion: string | null = null;
  filtroMesSolicitud: string | null = null;
  filtroAnnoSolicitud: string | null = null;
  mostrarFormularioNuevoAnticipo: boolean = false;
  verFiltroFechaConcesion: boolean = true;
  verFiltroFechaSolicitud: boolean = true;
  idEstadoSeleccionado: number = 0;

  constructor(private anticiposService: AnticiposService) { }

  ngOnInit(): void {
    this.verFiltroFechaConcesion = false;
    this.verFiltroFechaSolicitud = false;
    console.log('ngOnInit en anticipo.component');
    this.loadAnticipos();
  }

  loadAnticipos(): void {
    this.anticiposService.obtenerAnticiposPorEstado(0).subscribe(
      (data: AnticipoCompleto[]) => {
        this.anticipos = data;
        this.filtrarAnticipos();
      },
      (error: any) => {
        console.error('Error al cargar anticipos', error);
      }
    );
  }

  estados = [
    { I_ID_ESTADO: 0, S_DESCRIPCION: 'Nuevos' },
    { I_ID_ESTADO: 3, S_DESCRIPCION: 'Concesión' },
    { I_ID_ESTADO: 5, S_DESCRIPCION: 'En Amortización' },
    { I_ID_ESTADO: -1, S_DESCRIPCION: 'Consultas' },
    { I_ID_ESTADO: 99, S_DESCRIPCION: 'Estados Especiales' }
  ];

  onEstadoChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    const idEstado = selectElement.id === "null" ? undefined : parseInt(selectElement.id, 10);
    this.idEstadoSeleccionado = idEstado || 0;

      console.log("Estado: " + idEstado?.toString())

    // Mostrar filtros según estado de los anticipos
    switch (idEstado) {
      case 0:
      case 99:
        this.verFiltroFechaConcesion = false;
        this.verFiltroFechaSolicitud = false;
        break;

      case 3:
        this.verFiltroFechaConcesion = false;
        this.verFiltroFechaSolicitud = true;
        break;

      case 5:
      case -1:
        this.verFiltroFechaConcesion = true;
        this.verFiltroFechaSolicitud = true;
        break;

      default:
        this.verFiltroFechaConcesion = true;
        this.verFiltroFechaSolicitud = true;
        break;
    }

    //Mostrar filtro fecha en determinados estados
    this.verFiltroFechaSolicitud

    this.anticiposService.obtenerAnticiposPorEstado(idEstado).subscribe(
      (data: AnticipoCompleto[]) => {
        this.anticipos = data;
        this.textoFiltroLupa = "";
        this.filtrarAnticipos();
      },
      (error: any) => {
        console.error('Error al cargar anticipos', error);
      }
    );
  }

  detalleAnticipo(anticipo: AnticipoCompleto): void {
    console.log("anticipos.component Método detalleAnticipo()");
    this.anticiposService.obtenerAmortizacionesPorAnticipo(anticipo.idAnticipo).subscribe(
      (amortizaciones: AmortizacionDetalle[]) => {
        this.amortizaciones = amortizaciones;
      },
      (error: any) => {
        console.error('Error al cargar detalle del anticipo', error);
      });

    this.anticipoSeleccionado = anticipo;
    this.verDetalleAnticipo = true;
    this.verHistorialAnticipo = true;

  }

  anticiposFiltrados: AnticipoCompleto[] = [];

  // Método que captura la selección del filtro 
  aplicarFiltro(nomina: string | null, situacion: string | null,
    fechaConcesion: { mesConcesion: string | null, annoConcesion: string | null },
    fechaSolicitud: { mesSolicitud: string | null, annoSolicitud: string | null }) {
    this.filtroNomina = nomina;
    this.filtroSituacion = situacion;
    this.filtroMesConcesion = fechaConcesion.mesConcesion;
    this.filtroAnnoConcesion = fechaConcesion.annoConcesion;
    this.filtroMesSolicitud = fechaSolicitud.mesSolicitud;
    this.filtroAnnoSolicitud = fechaSolicitud.annoSolicitud;
    this.filtrarAnticipos();
  }

  onSearch(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    this.textoFiltroLupa = inputElement.value.toLowerCase();
    this.filtrarAnticipos();
  }

  filtrarAnticipos(): void {
    this.anticiposFiltrados = this.anticipos.filter(anticipo => {

      // Filtro nombre/dni
      const cumpleTexto =
        anticipo.dni.toLowerCase().includes(this.textoFiltroLupa) ||
        anticipo.apellidosNombre.toLowerCase().includes(this.textoFiltroLupa);

      // Filtro nómina
      const cumpleNomina = !this.filtroNomina || anticipo.claseNomina === this.filtroNomina;

      // Filtro situación
      const cumpleSituacion = !this.filtroSituacion || anticipo.idEstado === Number(this.filtroSituacion);

      // Filtro por mes/año de concesión
      let cumpleMesAnnoFechaConcesion = true;

      if (anticipo.fechaConcesion) {
        const fechaConcesion = this.parseFecha(anticipo.fechaConcesion);
        if (fechaConcesion) {
          cumpleMesAnnoFechaConcesion =
            (!this.filtroMesConcesion || fechaConcesion.mes.toString().padStart(2, '0') === this.filtroMesConcesion.padStart(2, '0')) &&
            (!this.filtroAnnoConcesion || fechaConcesion.anno.toString() === this.filtroAnnoConcesion);
        } else {
          cumpleMesAnnoFechaConcesion = false;
        }
      }

      // Filtro por mes/año de Solicitud
      let cumpleMesAnnoFechaSolicitud = true;

      if (anticipo.fechaAlta) {
        const fechaSolicitud = this.parseFecha(anticipo.fechaAlta);
        if (fechaSolicitud) {
          cumpleMesAnnoFechaSolicitud =
            (!this.filtroMesSolicitud || fechaSolicitud.mes.toString().padStart(2, '0') === this.filtroMesSolicitud.padStart(2, '0')) &&
            (!this.filtroAnnoSolicitud || fechaSolicitud.anno.toString() === this.filtroAnnoSolicitud);
        } else {
          cumpleMesAnnoFechaSolicitud = false;
        }
      }

      return cumpleTexto && cumpleNomina && cumpleSituacion && cumpleMesAnnoFechaConcesion && cumpleMesAnnoFechaSolicitud;
    });
  }

  parseFecha(fecha: string): { dia: number, mes: number, anno: number } | null {
    if (!fecha) return null;
    const partesFecha = fecha.split('/');
    if (partesFecha.length === 3) {
      const dia = parseInt(partesFecha[0], 10);
      const mes = parseInt(partesFecha[1], 10);
      const anno = parseInt(partesFecha[2], 10);
      return { dia, mes, anno };
    }
    return null;
  }

  mostrarNuevoAnticipo() {
    this.mostrarFormularioNuevoAnticipo = !this.mostrarFormularioNuevoAnticipo;
  }

  borrarAnticipo(anticipo: AnticipoCompleto): void {
    if (confirm(`¿Está seguro que desea eliminar el anticipo de ${anticipo.apellidosNombre}?`)) {
      this.anticiposService.borrarAnticipo(anticipo.idAnticipo).subscribe({
        next: () => {
          // Eliminar el anticipo de la lista local
          this.anticipos = this.anticipos.filter(a => a.idAnticipo !== anticipo.idAnticipo);
          // Actualizar la lista filtrada
          this.filtrarAnticipos();
        },
        error: (error) => {
          alert(`Error al eliminar el anticipo: ${error.message}`);
          console.error('Error al eliminar anticipo:', error);
        }
      });
    }
  }

  concesionAnticipos(): void {}
  infRetEspecie(): void {}
  altaPorTraslado(): void {}

}


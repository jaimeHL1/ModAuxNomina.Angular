export interface AnticipoCompleto {
  usuarioAlta?: string;
  idAnticipo: number;
  codigoHabilitacion: string;
  claseNomina: string;
  descripcionClaseNomina: string;
  duplicado: string;
  dni: string;
  nombre: string;
  apellido1: string;
  apellido2: string;
  apellidosNombre: string;
  annoSolicitud?: number;
  mesSolicitud?: number;
  descripcionMes: string;
  numPagasSolicitadas?: number;
  irpf?: number;
  sueldo?: number;
  trienios?: number;
  pagaExtra?: number;
  importePaga?: number;
  importeLiquido?: number;
  importeTotal?: number;
  totalLiquidoPorNumPagas?: number;
  idEstado?: number;
  fechaBaja?: Date;
  importeAmortizado?: number;
  cuotaMesAmortizacion?: number;
  fechaCertificacion?: string;
  fechaConcesion?: string;
  descripcionEstado?: string;
  fechaAlta?: string;
  mesSiguiente?: string;
  annoSiguiente?: number;
  fechaInicioAmortizacion?: string;
  importeRestante?: number;
  numPagasAmortizadas?: number;
  numMesesDevolucion?: number;
  numMesesPendientes: number;
  grupo?: string;
  nivel?: string;
  observaciones?: string;
  mesConcesion?: number;
  annoConcesion?: number;
  fechaNominaActual?: string;
  fechaFinAmortizacion?: string;
  spanFormula?: string;
}

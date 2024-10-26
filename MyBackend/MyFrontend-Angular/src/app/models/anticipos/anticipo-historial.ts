export interface AnticipoHistorial {
	idAnticipo: number;
	idAmortizacion: number;
	anno?: number;
	mes?: number;
	fechaD?: Date;
	fecha?: string;
	importe?: number;
	observaciones?: string;
	tipoAmortizacion?: number;
	desTipoAmortizacion?: string; 
}
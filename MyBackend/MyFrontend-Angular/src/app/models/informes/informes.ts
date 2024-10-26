export interface InformeDTO {
    idInforme: number;
    descripcion: string;
    parametroDTOs: ParametroDTO[] | null;
}

export interface ParametroDTO {
    idParametro: number;
    valor: string | null;
    obligatorio: boolean;
    texto: string;
}

export interface CategoriaDTO {
    idCategoria: number;
    descripcion: string;
    informes: InformeDTO[];
}

export interface InformeDTO {
    idInforme: number;
    descripcion: string;
}

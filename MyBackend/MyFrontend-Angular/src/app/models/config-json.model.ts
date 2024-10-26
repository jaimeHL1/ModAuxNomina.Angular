export interface UsuarioConectado {
  nombre: string;
  Apellidos: string;
  NIF: string;
  DNI:string;
  Login: string;
  PerfilAutoriza: string;
}

export interface ConfigJson {
  appTitulo:string;
  rutaApi: string;
  rutaConsultaNomina:string;
  rutaGestionProductividad:string;
  entornoGenerico: string;
  usuarioConectado: UsuarioConectado;
}
import { Component, Input, OnInit, isDevMode } from '@angular/core';
import { InformesService } from './informes.service';
import { CategoriaDTO, InformeDTO, ParametroDTO } from '../../models/informes/informes';

import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ResultadoInformeDTO } from '../../models/informes/ResultadoInforme';
import { SharedService } from '../shared.service';


@Component({
  selector: 'app-informes',
  templateUrl: './informes.component.html',
  styleUrl: './informes.component.scss',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
})
export class InformesComponent implements OnInit {
  @Input() form: FormGroup = new FormGroup({
    Categorias: new FormControl(0),
    Informes: new FormControl(0),
  });

  @Input() formParametros!: FormGroup; // Formulario dinámico de parámetros de informe
  @Input() parametrosInforme: ParametroDTO[] | null = null;

  categorias: CategoriaDTO[] | null = null;
  idCategoriaSeleccionada: number | null = null;
  informes: InformeDTO[] | null = null;
  idInformeSeleccionado: number | null = null;
  informeSeleccionado: InformeDTO | null = null;

  constructor(
    private informesServices: InformesService,
    private sharedService: SharedService
  ) { }


  async ngOnInit() {
    // Llamada REST para obtener las categorías e informes correspondientes
    await this.obtenerCategoriasInformes();
  }

  // Cargar categoría seleccionada en el sistema y preparar los informes correspondientes
  SelectCategoria() {
    this.idCategoriaSeleccionada = this.form.controls["Categorias"].value as number;
    const categoria = this.categorias?.find(x => x.idCategoria == this.idCategoriaSeleccionada);

    if (isDevMode()){
      console.log(categoria);
    }

    if (categoria) {
      this.informes = categoria.informes;
      this.parametrosInforme = null;
    }
  }

  // Cargar el informe correspondiente y utilizar el Api para saber que parámetros se tienen que utilizar.
  async SelectInforme() {
    this.idInformeSeleccionado = this.form.controls["Informes"].value as number;
    this.informeSeleccionado = this.informes?.find(x => x.idInforme == this.idInformeSeleccionado) ?? null;

    if (isDevMode()){
      console.log(this.informeSeleccionado );
    }

    if (this.informeSeleccionado) {
      this.parametrosInforme = null;
      // Llamada REST para obtener parámetros
      await this.obtenerParametrosInforme(this.informeSeleccionado.idInforme);
    }
  }

  // Validar el parámetro rellenado del formulario de los parámetros
  public esValido(control: ParametroDTO) {
    return this.formParametros.controls[control.idParametro].valid;
  }


  async obtenerCategoriasInformes() {
    this.categorias = await this.informesServices.obtenerCategoriasInformes();
  }


  // Carga de los parámetros en el formulario dinámico
  async obtenerParametrosInforme(idInforme: number) {
    const idInformeEntorno = idInforme;
    this.parametrosInforme = await this.informesServices.obtenerParametrosInforme(idInformeEntorno);
    this.formParametros = this.toFormGroup(this.parametrosInforme as ParametroDTO[]);
  }

  // Crear los controles del formulario dinámico según sus características
  toFormGroup(parametros: ParametroDTO[] | null) {
    const group: any = {};
    parametros?.forEach((parametro) => {
      group[parametro.idParametro] = parametro.obligatorio
        ? new FormControl('', Validators.required)
        : new FormControl('');
    });
    return new FormGroup(group);
  }

  // Ejecuta el informe
  async GenerarInforme(event: Event) {
    // Rutinas de comprobación
    if (this.informeSeleccionado && this.parametrosInforme) {
      const informe = this.informeSeleccionado;
      const parametros = this.parametrosInforme;

      if (isDevMode()) {
        console.log(`Ejecución del informe: ${informe.idInforme}`);
        console.log(parametros)
      }

      parametros.forEach((p) => {
        const c = this.formParametros.controls[p.idParametro];
        const fecha = new Date(c.value);
        p.valor = `${fecha.getFullYear()}${('0' + (fecha.getMonth() + 1)).slice(-2)}`;
      });

      
    }
  }
}

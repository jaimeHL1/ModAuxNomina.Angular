import { Component, OnInit } from '@angular/core';
import { LoaderService } from './loader.service';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrl: './loader.component.scss'
})
export class LoaderComponent   {
  isLoading = this.loaderService.isLoading;

  constructor(private loaderService: LoaderService) {}    
}

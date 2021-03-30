import { Component, EventEmitter, OnInit, Output, OnDestroy } from '@angular/core';
import { SeguridadService } from '../../seguridad/seguridad.service';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-barra',
  templateUrl: './barra.component.html',
  styleUrls: ['./barra.component.css']
})
export class BarraComponent implements OnInit, OnDestroy {
  @Output() menuToggle = new EventEmitter<void>();
  estadoUsuario!: boolean;
  usuarioSubscription!: Subscription;

  constructor(private seguridadService: SeguridadService) { }

  ngOnInit(): void {
    this.usuarioSubscription = this.seguridadService.seguridadCambio.subscribe( status =>{
        this.estadoUsuario = status;
    });
  }

  onMenuToggleDispath(){
    this.menuToggle.emit();
  }

  ngOnDestroy(){
    this.usuarioSubscription.unsubscribe();
  }

  terminarSesion(){
    this.seguridadService.salirSesion();
  }
}

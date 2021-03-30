import { Component, Input, EventEmitter, Output } from '@angular/core';
import {LibrosServices} from '../services/libros.services';

@Component({
  selector: 'app-libro',
  templateUrl: './libro.component.html',
  styleUrls: ['./libro.component.css'],
})
export class LibroComponent {
  @Input() tituloLibro!: string;
  @Output() libroClicked = new EventEmitter();

  constructor(private librosService: LibrosServices){}

  onClicked() {
    //this.libroClicked.emit();
    this.librosService.eliminarLibro(this.tituloLibro);
  }
}

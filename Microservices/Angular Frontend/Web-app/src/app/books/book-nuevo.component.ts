import {Component} from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-book-nuevo',
  templateUrl: 'book-nuevo.component.html'

})

export class BookNuevoComponent{
  selectAutor!: string;

  guardarLibro(form: NgForm){

  }

}

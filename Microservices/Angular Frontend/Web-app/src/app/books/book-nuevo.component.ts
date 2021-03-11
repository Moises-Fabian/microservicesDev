import {Component, ViewChild} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';

@Component({
  selector: 'app-book-nuevo',
  templateUrl: 'book-nuevo.component.html'

})

export class BookNuevoComponent{
  selectAutor!: string;
  @ViewChild(MatDatepicker) picker!: MatDatepicker<Date>;

  guardarLibro(form: NgForm){

  }

}

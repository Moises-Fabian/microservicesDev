import {Component, ViewChild} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { MatDialog } from '@angular/material/dialog';
import { MatSelectChange } from '@angular/material/select';
import { BookService } from './book.service';

@Component({
  selector: 'app-book-nuevo',
  templateUrl: 'book-nuevo.component.html'

})

export class BookNuevoComponent{
  selectAutor!: string;
  selectAutorTexto!: string;
  fechaPublicacion!: string;
  @ViewChild(MatDatepicker) picker!: MatDatepicker<Date>;

  constructor(private booksService: BookService, private dialogref: MatDialog){}

  selected(event: MatSelectChange){
    this.selectAutorTexto = (event.source.selected as MatOption).viewValue;
  }

  guardarLibro(form: NgForm){
    if(form.valid){
      this.booksService.guardarLibro({
        libroId: 1,
        descripcion: form.value.descripcion,
        titulo: form.value.titulo,
        autor: this.selectAutorTexto,
        precio: form.value.precio,
        fechaPublicacion: new Date(this.fechaPublicacion)
      });
      this.dialogref.closeAll();
    }


  }

}

import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { MatDialog } from '@angular/material/dialog';
import { MatSelectChange } from '@angular/material/select';
import { Subscription } from 'rxjs';
import { Autor } from '../autores/autor.model';
import { AutoresService } from '../autores/autores.service';
import { BookService } from './book.service';

@Component({
  selector: 'app-book-nuevo',
  templateUrl: 'book-nuevo.component.html',
})
export class BookNuevoComponent implements OnInit, OnDestroy {
  selectAutor!: string;
  selectAutorTexto!: string;
  fechaPublicacion!: string;
  @ViewChild(MatDatepicker) picker!: MatDatepicker<Date>;

  autores: Autor[] = [];

  autorSubscription!: Subscription;

  constructor(
    private booksService: BookService,
    private dialogref: MatDialog,
    private autoresService: AutoresService
  ) {}

  ngOnInit() {
    this.autoresService.obtenerAutores();
    this.autorSubscription = this.autoresService
      .obtenerActualListener()
      .subscribe((autoresBackend: Autor[]) => {
        this.autores = autoresBackend;
      });
  }

  selected(event: MatSelectChange) {
    this.selectAutorTexto = (event.source.selected as MatOption).viewValue;
  }

  guardarLibro(form: NgForm) {
    if (form.valid) {
      const autorRequest = {
        id: this.selectAutor,
        nombreCompleto: this.selectAutorTexto,
      };

      const libroRequest = {
        id: '',
        descripcion: form.value.descripcion,
        titulo: form.value.titulo,
        autor: autorRequest,
        precio: parseInt(form.value.precio),
        fechaPublicacion: new Date(this.fechaPublicacion),
      };

      this.booksService.guardarLibro(libroRequest);
      this.autorSubscription = this.booksService
        .guardarLibroListener()
        .subscribe(() => {
          this.dialogref.closeAll();
        });
    }
  }

  ngOnDestroy() {
    this.autorSubscription.unsubscribe();
  }
}

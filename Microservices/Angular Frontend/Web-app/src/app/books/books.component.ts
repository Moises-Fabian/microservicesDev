import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { BookService } from './book.service';
import {Books} from './book.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { BookNuevoComponent } from './book-nuevo.component';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit, AfterViewInit {
  bookData: Books[] = [];
  desplegarColumnas = ["titulo", "descripcion", "autor", "precio"];
  dataSource = new MatTableDataSource<Books>();
  @ViewChild(MatSort) ordenamiento!: MatSort;
  @ViewChild(MatPaginator) paginacion;

  constructor(private bookService: BookService, private dialog: MatDialog) { }
  hacerFiltro(filtro : string){
    this.dataSource.filter = filtro;
  }

  abrirDialog(){
    this.dialog.open(BookNuevoComponent,{
      width : '350px'
    });
  }

  ngOnInit(): void {
    this.dataSource.data = this.bookService.obtenerLibros();
  }

  ngAfterViewInit(){
    this.dataSource.sort = this.ordenamiento;
    this.dataSource.paginator = this.paginacion;

  }

}

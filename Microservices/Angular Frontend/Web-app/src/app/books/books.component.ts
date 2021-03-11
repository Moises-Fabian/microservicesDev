import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { BookService } from './book.service';
import {Books} from './book.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

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

  constructor(private bookService: BookService) { }
  hacerFiltro(filtro : string){
    this.dataSource.filter = filtro;
  }

  ngOnInit(): void {
    this.dataSource.data = this.bookService.obtenerLibros();
  }

  ngAfterViewInit(){
    this.dataSource.sort = this.ordenamiento;

  }

}

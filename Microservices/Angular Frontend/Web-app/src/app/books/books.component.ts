import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { BookService } from './book.service';
import {Books} from './book.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { BookNuevoComponent } from './book-nuevo.component';
import {Subject, Subscription} from 'rxjs';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit, AfterViewInit, OnDestroy {
  bookData: Books[] = [];
  desplegarColumnas = ["titulo", "descripcion", "autor", "precio"];
  dataSource = new MatTableDataSource<Books>();
  @ViewChild(MatSort) ordenamiento!: MatSort;
  @ViewChild(MatPaginator) paginacion;

  private bookSubscription!: Subscription;

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
    this.bookSubscription = this.bookService.bookSubjet.subscribe(() => {
      this.dataSource.data = this.bookService.obtenerLibros();
    })
  }

  ngAfterViewInit(){
    this.dataSource.sort = this.ordenamiento;
    this.dataSource.paginator = this.paginacion;

  }

  ngOnDestroy(){
    this.bookSubscription.unsubscribe();
  }

}

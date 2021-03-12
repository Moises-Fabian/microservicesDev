import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { BookService } from './book.service';
import { Books } from './book.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { BookNuevoComponent } from './book-nuevo.component';
import { Subject, Subscription } from 'rxjs';
import { paginationBooks } from './pagination-books.model';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css'],
})
export class BooksComponent implements OnInit, AfterViewInit, OnDestroy {
  bookData: Books[] = [];
  desplegarColumnas = ['titulo', 'descripcion', 'autor', 'precio'];
  dataSource = new MatTableDataSource<Books>();
  @ViewChild(MatSort) ordenamiento!: MatSort;
  @ViewChild(MatPaginator) paginacion;

  private bookSubscription!: Subscription;

  totalLibros = 0;
  librosporPagina = 2;
  paginaCombo = [1, 2, 5, 10];
  paginaActual = 1;
  sort = 'titulo';
  sortDirection = 'asc';
  filterValue = null;

  constructor(private bookService: BookService, private dialog: MatDialog) {}

  eventoPaginador(event: PageEvent){
    this.librosporPagina = event.pageSize;
    this.paginaActual = event.pageIndex + 1;
    this.bookService.obtenerLibros(this.librosporPagina, this.paginaActual, this.sort, this.sortDirection, this.filterValue);
  }


  hacerFiltro(filtro: string) {
    this.dataSource.filter = filtro;
  }

  abrirDialog() {
    this.dialog.open(BookNuevoComponent, {
      width: '350px',
    });
  }

  ngOnInit(): void {
    this.bookService.obtenerLibros(this.librosporPagina, this.paginaActual, this.sort, this.sortDirection, this.filterValue);
    this.bookService
                .obtenerActualListener()
                .subscribe( (pagination: paginationBooks) => {
                  this.dataSource = new MatTableDataSource<Books>(pagination.data);
                  this.totalLibros = pagination.totalRows;
                });
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.ordenamiento;
    this.dataSource.paginator = this.paginacion;
  }

  ngOnDestroy() {
    this.bookSubscription.unsubscribe();
  }
}

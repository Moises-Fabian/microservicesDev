import {Books} from './book.model';
import {Subject} from 'rxjs';
import { environment } from 'src/environments/environment';
import { paginationBooks } from './pagination-books.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BookService{
  baseUrl = environment.baseUrl;

  //private booksLista: Books[] =[];

  bookSubjet = new Subject();

  bookPagination!: paginationBooks;
  bookPaginationSubject = new Subject<paginationBooks>();

  constructor(private http: HttpClient){}

  obtenerLibros(libroPorPagina: number, paginaActual: number, sort: string, sortDirection: string, filterValue: any): void{
    const request = {
       pageSize: libroPorPagina,
       page: paginaActual,
       sort,
       sortDirection,
       filterValue
    };

    this.http.post<paginationBooks>(this.baseUrl + 'Libro/Pagination', request)
    .subscribe( (Response) =>{
      this.bookPagination = Response;
      this.bookPaginationSubject.next(this.bookPagination);
    });
  }

  obtenerActualListener(): any{
    return this.bookPaginationSubject.asObservable();
  }

  guardarLibro(book: Books): void{
    this.http.post(this.baseUrl + 'Libro', book)
      .subscribe( () => {
        this.bookSubjet.next();
      });
  }

  guardarLibroListener(): any{
    return this.bookSubjet.asObservable();
  }

}

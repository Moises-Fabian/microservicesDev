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

  private booksLista: Books[] =[];

  bookSubjet = new Subject();

  bookPagination!: paginationBooks;
  bookPaginationSubject = new Subject<paginationBooks>();

  constructor(private http: HttpClient){}

  obtenerLibros(libroPorPagina: number, paginaActual: number, sort: string, sortDirection: string, filterValue: any){
    const request = {
       pageSize: libroPorPagina,
       page: paginaActual,
       sort,
       sortDirection,
       filterValue
    };

    this.http.post<paginationBooks>(this.baseUrl + 'api/Libro/Pagination', request)
    .subscribe( (Response) =>{
      this.bookPagination = Response;
      this.bookPaginationSubject.next(this.bookPagination);
    });
  }

  obtenerActualListener(){
    return this.bookPaginationSubject.asObservable();
  }

  guardarLibro(book: Books){
    this.http.post(this.baseUrl + 'api/libro', book)
      .subscribe( (response) => {
        this.bookSubjet.next();
      });
  }

  guardarLibroListener(){
    return this.bookSubjet.asObservable();
  }

}

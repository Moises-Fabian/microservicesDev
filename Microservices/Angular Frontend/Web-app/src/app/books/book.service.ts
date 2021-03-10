import {Books} from './book.model';

export class BookService{

  booksLista: Books[] =[

  ];

  obtenerLibros(){
    return this.booksLista.slice();
  }

}

import {Books} from './book.model';

export class BookService{

  private booksLista: Books[] =[

    {libroId: 1, titulo: 'Algoritmos', descripcion: 'libro basico', autor: 'Juan perez', precio: 25000 },
    {libroId: 2, titulo: 'Matematicas', descripcion: 'libro avanzado', autor: 'gabriel mendoza', precio: 25000 },
    {libroId: 3, titulo: 'Aritmetica', descripcion: 'octavo basico', autor: 'luis correa', precio: 25000 },
    {libroId: 4, titulo: 'Java', descripcion: 'Springboot', autor: 'Aldo Martinez', precio: 25000 },

  ];

  obtenerLibros(){
    return this.booksLista.slice();
  }

}

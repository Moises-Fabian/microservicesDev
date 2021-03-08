import {Subject} from 'rxjs';

export class LibrosServices{

  libroSubject = new Subject();

  private libros = ['libro de moises', 'libro de programacion', 'NetCore5'];

  agregarlibro(libroNombre: string){
    this.libros.push(libroNombre);
    this.libroSubject.next();
  }

  eliminarLibro(libroNombre: string){
    this.libros = this.libros.filter(x => x !== libroNombre);
    this.libroSubject.next();
  }

  obtenerLibros(){
    return [...this.libros];
  }
}

import { Component, OnInit, OnDestroy } from '@angular/core';
import { LibrosServices } from '../services/libros.services';
import { Subscription} from 'rxjs';

@Component({
  selector: 'app-libros',
  templateUrl: './libros.component.html',
})
export class LibrosComponent implements OnInit, OnDestroy{
  libros = [] as any;
  constructor(private librosService: LibrosServices){}
  private libroSubscription!: Subscription;


  eliminarLibro(libro){
  }

  guardarLibro(f){
    if(f.valid){
      this.librosService.agregarlibro(f.value.nombreLibro);

    }
  }

  ngOnInit(){
    this.libros = this.librosService.obtenerLibros();
    this.libroSubscription = this.librosService.libroSubject.subscribe( () =>{
      this.libros = this.librosService.obtenerLibros();
    });
  }
  ngOnDestroy(){
    this.libroSubscription.unsubscribe();
  }
}

import { Injectable } from "@angular/core";
import { Autor } from "./autor.model";

@Injectable({
  providedIn: 'root'
})
export class AutoresService{
  private autoresLista: Autor[] = [
    {autorId: 1, nombre: 'Moises', apellido: 'Lagos', gradoAcademico: 'Ingeniero de software'},
    {autorId: 2, nombre: 'Lorenzo', apellido: 'Ramirez', gradoAcademico: 'Matemática'},
    {autorId: 3, nombre: 'Angelo', apellido: 'Martinez', gradoAcademico: 'Ciencias de la computacion'},
    {autorId: 4, nombre: 'Martin', apellido: 'Gutierrez', gradoAcademico: 'Ingeniería de sistemas'}

  ];

  obtenerAutores(){
    return this.autoresLista.slice();
  }
}

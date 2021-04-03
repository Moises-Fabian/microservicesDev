import {Subject} from 'rxjs';
import { Usuario } from './usuario.model';
import { LoginData } from './login-data.model';
import {Router} from '@angular/router';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SeguridadService {
  baseUrl = environment.baseUrl;

  seguridadCambio = new Subject<boolean>();
  private usuario!: Usuario | null;

  constructor(private router: Router, private http: HttpClient){

  }

  registrarUsuario(usr: Usuario) {
    this.usuario = {
      email: usr.email,
      usuarioId: Math.round(Math.random() * 10000).toString(),
      nombre: usr.nombre,
      apellidos: usr.apellidos,
      username: usr.username,
      password: ''
    };

    this.seguridadCambio.next(true);
    this.router.navigate(['/']);
  }

  login(loginData: LoginData) {
    this.http.post(this.baseUrl + 'usuario/login', loginData)
    .subscribe( (Response) => {
      console.log('login respuesta', Response);
    });
  }

  salirSesion() {
    this.usuario = null;
    this.seguridadCambio.next(false);
    this.router.navigate(['/login']);
  }

  obtenerUsuario() {
    return { ...this.usuario };
  }

  onSesion(){
    return this.usuario != null;
  }
}

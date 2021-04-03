import {Subject} from 'rxjs';
import { Usuario } from './usuario.model';
import { LoginData } from './login-data.model';
import {Router} from '@angular/router';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Token } from '@angular/compiler/src/ml_parser/lexer';

@Injectable({
  providedIn: 'root'
})
export class SeguridadService {
  private token!: string;
  baseUrl = environment.baseUrl;

  seguridadCambio = new Subject<boolean>();
  private usuario!: Usuario | null;

  obtenerToken(): string{
    return this.token;
  }

  constructor(private router: Router, private http: HttpClient){

  }

  registrarUsuario(usr: Usuario){
    this.usuario = {
      email: usr.email,
      usuarioId: Math.round(Math.random() * 10000).toString(),
      nombre: usr.nombre,
      apellidos: usr.apellidos,
      username: usr.username,
      password: '',
      token: usr.token
    };

    this.seguridadCambio.next(true);
    this.router.navigate(['/']);
  }

  login(loginData: LoginData): void {
    this.http.post<Usuario>(this.baseUrl + 'usuario/login', loginData)
    .subscribe( (Response) => {
      console.log('login respuesta', Response);

      this.token = Response.token;
      this.usuario = {
        email: Response.email,
        nombre: Response.nombre,
        apellidos: Response.apellidos,
        token: Response.token,
        password: '',
        username: Response.username,
        usuarioId: Response.usuarioId
      };
      this.seguridadCambio.next(true);
      this.router.navigate(['/']);
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

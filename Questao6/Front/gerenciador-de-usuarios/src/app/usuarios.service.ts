import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from '../models/Usuario.model';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  private apiUrl = 'http://localhost:3000';

  constructor(private http: HttpClient) { }

  getUsuarios(): Observable<Usuario[]> {
    return this.http.get<Usuario[]>(`${this.apiUrl}/usuarios`);
  }
  
  getUsuarioById(id: number): Observable<Usuario> {
    const url = `${this.apiUrl}/usuarios/${id}`;
    return this.http.get<Usuario>(url);
  }

  adicionarUsuario(usuario: Usuario): Observable<Usuario> {
    var url = `${this.apiUrl}/usuarios`;
    return this.http.post<Usuario>(url, usuario)
  }

  atualizarUsuario(usuario: Usuario): Observable<Usuario> {
    const url = `${this.apiUrl}/usuarios/${usuario.id}`;
    return this.http.put<Usuario>(url, usuario);
  }

  excluirUsuario(usuario: Usuario) {
    const url = `${this.apiUrl}/usuarios/${usuario.id}`;
    return this.http.delete<Usuario>(url);
  }
}
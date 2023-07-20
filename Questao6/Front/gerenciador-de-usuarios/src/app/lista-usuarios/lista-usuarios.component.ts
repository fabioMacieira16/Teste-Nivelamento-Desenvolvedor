import { Component, OnInit } from '@angular/core';
import { UsuariosService } from '../usuarios.service';
import { Usuario } from '../../models/Usuario.model';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-lista-usuarios',
  templateUrl: './lista-usuarios.component.html',
  styleUrls: ['./lista-usuarios.component.css']
})
export class ListaUsuariosComponent implements OnInit {

  usuarios: Usuario[] = [];
  
  constructor(private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private usuariosService: UsuariosService
  ) { }

  ngOnInit() {
   this.atualizarListaUsuarios();

  }

    // atualizarFotoUsuario(usuario: Usuario, novaFoto: string): void {
  //   this.usuariosService.atualizarFotoUsuario(usuario.id, novaFoto).subscribe(usuarioAtualizado => {
  //     //Atualiza a lista de usuários com o usuário atualizado
  //     const index = this.usuarios.findIndex(u => u.id === usuarioAtualizado.id);

  //     this.usuarios[index] = usuarioAtualizado;

  //   });
  // }
  excluirUsuario(usuario: Usuario) {

    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().subscribe((confirmado: boolean) => {
      if (confirmado) {
        this.usuariosService.excluirUsuario(usuario).subscribe(
          () => {
            this.snackBar.open('Usuário excluído com sucesso!', 'Fechar', {
              duration: 3000,
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
            });
            this.atualizarListaUsuarios();
          },
          (error) => {
            this.snackBar.open('Erro ao excluir o usuário.', 'Fechar', {
              duration: 3000,
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
            });
          }
        );
      }
    });
  }

  atualizarListaUsuarios() {
    this.usuariosService.getUsuarios().subscribe(usuarios => {
      this.usuarios = usuarios;
    });
  }
}
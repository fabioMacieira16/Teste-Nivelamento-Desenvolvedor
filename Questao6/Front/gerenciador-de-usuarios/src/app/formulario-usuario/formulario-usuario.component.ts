import { Component, OnInit, Input } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UsuariosService } from '../usuarios.service';
import { Usuario } from 'src/models/Usuario.model';

@Component({
  selector: 'app-formulario-usuario',
  templateUrl: './formulario-usuario.component.html',
  styleUrls: ['./formulario-usuario.component.css']
})
export class FormularioUsuarioComponent implements OnInit {

  usuarioForm: FormGroup = new FormGroup({});
  usuario: Usuario = {} as Usuario;
  editMode = false;
  selectedFile: File | undefined;

  constructor(
    private snackBar: MatSnackBar,
    private router: Router,
    private formBuilder: FormBuilder,
    private usuariosService: UsuariosService,
    private ActivatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.usuarioForm = this.formBuilder.group({
      nome: ['', Validators.required],
      email: ['', Validators.required, Validators.email],
      foto: ['']
    });

    this.ActivatedRoute.params.subscribe(params => {
      const id = +params['id'];
      if (id) {
        this.editMode = true;
        this.usuariosService.getUsuarioById(id).subscribe(usuario => {
          this.usuario = usuario;
          this.usuarioForm.patchValue({
            nome: usuario.nome,
            email: usuario.email,
            foto: usuario.foto
          });
        });
      }
    });
  }

  onSubmit() {
    if (this.usuario.id) {
      this.usuariosService.atualizarUsuario(this.usuario).subscribe(
        (usuarioAtualizado: Usuario) => {
          this.snackBar.open('Usuário atualizado!', 'Fechar', {
            duration: 3000,
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            panelClass: ['seu-estilo']
          });
          // this.usuarioForm.reset();
          this.router.navigate(['/lista-usuarios'])
        },
        (error) => {
          this.snackBar.open('Erro ao atualizar o usário!', error, {
            duration: 3000,
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            panelClass: ['seu-estilo']
          });
        }
      );
    } else {
      this.usuariosService.adicionarUsuario(this.usuario).subscribe(
        (usuarioAdcionado: Usuario) => {
          this.snackBar.open('Usuário Salvo com sucesso!', 'Fechar', {
            duration: 3000,
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            panelClass: ['seu-estilo']
          });
          this.usuarioForm.reset();
        },
        (error) => {
          this.snackBar.open('Erro ao Salvar usário!', error, {
            duration: 3000,
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            panelClass: ['seu-estilo']
          });
        }
      );
    }
  }

  onFileSelected(event: any){
    this.selectedFile = event.target.files[0];
  }

  cleanForm() {
    this.usuarioForm.reset();
  }
}
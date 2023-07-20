import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListaUsuariosComponent } from './lista-usuarios/lista-usuarios.component';
import { FormularioUsuarioComponent } from './formulario-usuario/formulario-usuario.component';

const routes: Routes = [
  { path: '', redirectTo: '/lista-usuarios', pathMatch: 'full' },
  { path: 'lista-usuarios', component: ListaUsuariosComponent },
  { path: 'adicionar-usuario', component: FormularioUsuarioComponent },
  { path: 'editar-usuario/:id', component: FormularioUsuarioComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
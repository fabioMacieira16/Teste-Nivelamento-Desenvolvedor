export class Usuario {
    id: number | null;
    nome: string  | null;
    email: string | null;
    foto: string  | null;
  
    constructor(id: number | null, nome: string  | null, email: string  | null, foto: string  | null) {
      this.id = id;
      this.nome = nome;
      this.email = email;
      this.foto = foto;
    }
  }
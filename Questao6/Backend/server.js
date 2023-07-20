const fs = require("fs");
const express = require("express");
const bodyParser = require("body-parser");
const cors = require("cors");

const app = express();
const port = process.env.PORT || 3000;

app.use(bodyParser.json());
app.use(cors());

// Dados fake
let usuarios = [
  { id: 1, nome: 'Fulano', email: 'fulano@example.com', foto: 'https://images.pexels.com/photos/2777898/pexels-photo-2777898.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1' },
  { id: 2, nome: 'Ciclano', email: 'ciclano@example.com', foto: 'https://images.pexels.com/photos/2007647/pexels-photo-2007647.jpeg?auto=compress&cs=tinysrgb&w=600' },
  { id: 3, nome: 'Beltrano', email: 'beltrano@example.com', foto: 'https://images.pexels.com/photos/8059107/pexels-photo-8059107.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1' },
];

// Rota para listar usuários
app.get('/usuarios', (req, res) => {
  res.json(usuarios);
});


// Rota para listar usuários com o dados.json
// app.get("/usuarios", (req, res) => {
//   try {
//     const dadosJSON = fs.readFileSync('dados.json', 'utf8');
//     const dados = JSON.parse(dadosJSON);
//     console.log(dados);

//     res.json(dados);
//   } catch (error) {
//     res.status(500).json({ error: 'Erro ao ler os dados do arquivo' });
//   }
// });

// Rota para usuários por id
app.get("/usuarios/:id", (req, res) => {
  const id = parseInt(req.params.id);

  const usuario = usuarios.find((u) => u.id === id);

  if (!usuario) {
    res.status(404).json({ error: "Usuário não encontrado" });
  } else {
    res.json(usuario);
  }
});

app.put("/usuarios/:id", (req, res) => {
  const id = parseInt(req.params.id);
  const { nome, email, foto } = req.body;

  const usuario = usuarios.find((u) => u.id === id);

  if (!usuario) {
    res.status(404).json({ error: "Usuário não encontrado" });
  } else {
    if (nome) {
      usuario.nome = nome;
    }

    if (email) {
      usuario.email = email;
    }

    if (foto) {
      usuario.foto = foto;
    }

    res.json(usuario);
  }
});

app.delete("/usuarios/:id", (req, res) => {
  const id = parseInt(req.params.id);

  const index = usuarios.findIndex((user) => user.id === id);

  if (index !== -1) {
    usuarios.splice(index, 1);
    res.json({ mensagem: "Usuário excluido com sucesso!" });
  } else {
    res.status(404).json({ error: "Usuário não encontrado" });
  }
});

app.post("/usuarios", (req, res) => {
  const user = req.body;
  user.id = usuarios.length + 1;

  usuarios.push(user);

  res.json(user);
});

app.listen(port, () => {
  console.log(`Servidor rodando na porta ${port}`);
});
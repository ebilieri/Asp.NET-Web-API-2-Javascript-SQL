var tbody = document.querySelector('table tbody');
var aluno = {}; // Instanciar aluno

function cadastrar() {
    // Preenchar dados do aluno
    aluno.nome = document.querySelector('#nome').value;
    aluno.sobrenome = document.querySelector('#sobrenome').value;
    aluno.telefone = document.querySelector('#telefone').value;
    aluno.ra = document.querySelector('#ra').value;

    if (aluno.Id === undefined || aluno.Id === 0) {
        //salvar
        salvarAluno('POST', 0, aluno);
    }
    else {
        //alterar
        salvarAluno('PUT', aluno.Id, aluno);
    }

    carregarAlunos();
    cancelarLimpar();
}

function carregarAlunos() {
    tbody.innerHTML = '';

    // carregar lista do banco de dados
    var xhr = new XMLHttpRequest();
    xhr.open('GET', 'https://localhost:44346/api/aluno', true);
    xhr.onload = function () {
        var listaAlunos = JSON.parse(this.responseText);
        // Percorre a lista e preenche a tabela
        for (var indice in listaAlunos) {
            adicionaLinha(listaAlunos[indice]);
        }
    }

    xhr.send();
}

function salvarAluno(metodo, id, corpo) {
    if (id === undefined || id === 0)
        id = '';

    var xhr = new XMLHttpRequest();
    xhr.open(metodo, `https://localhost:44346/api/aluno/${id}`, false);
  
    xhr.setRequestHeader('content-type', 'application/json');
    // Gravar registros no banco de dados
    xhr.send(JSON.stringify(corpo));            
}

function excluirAluno(id) {
    var xhr = new XMLHttpRequest();
    // excluir registro do banco de dados
    xhr.open('DELETE', `https://localhost:44346/api/aluno/${id}`, false);

    xhr.send();
}

function excluir(id) {
    excluirAluno(id);
    carregarAlunos();
}

function editarAluno(alunoEdit) {
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar');
    var titulo = document.querySelector("#titulo");

    // carregar dados no formulário para edição
    document.querySelector('#nome').value = alunoEdit.Nome;
    document.querySelector('#sobrenome').value = alunoEdit.SobreNome;
    document.querySelector('#telefone').value = alunoEdit.Telefone;
    document.querySelector('#ra').value = alunoEdit.RA;

    btnSalvar.textContent = 'Salvar';
    btnCancelar.textContent = 'Cancelar';
    titulo.textContent = `Editar Aluno ${alunoEdit.Nome}`

    aluno = alunoEdit;            
}

function cancelarLimpar() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar');
    var titulo = document.querySelector("#titulo");

    // limpar aluno
    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';

    btnSalvar.textContent = 'Cadastrar';
    btnCancelar.textContent = 'Limpar';
    titulo.textContent = 'Cadastrar Aluno';
    aluno = {};
}

function adicionaLinha(aluno) {
	// adicionar linhas na tabela
    var trow = `<tr>
                    <td>${aluno.Nome}</td>
                    <td>${aluno.SobreNome}</td>
                    <td>${aluno.Telefone}</td>
                    <td>${aluno.RA}</td>
                    <td>
                        <button onclick='editarAluno(${JSON.stringify(aluno)})'>Editar</button>
                        <button onclick='excluir(${aluno.Id})'>Excluir</button>
                    </td>
                </td>`

    tbody.innerHTML += trow;
}

// carregar a listagem de aluno
carregarAlunos();
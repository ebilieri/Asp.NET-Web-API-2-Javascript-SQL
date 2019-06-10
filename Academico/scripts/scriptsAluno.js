
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

    $('#exampleModal').modal('hide');
    carregarAlunos();
   // cancelarLimpar();    
}

function carregarAlunos() {
    tbody.innerHTML = '';

    // carregar lista do banco de dados
    var xhr = new XMLHttpRequest();
    console.log('UNSENT', xhr.readyState);

    xhr.open('GET', 'https://localhost:44346/api/aluno/listar', true);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));
    
    console.log('OPENED', xhr.readyState);

    xhr.onerror = function(){
        console.log('ERROR', xhr.readyState);
    }

    //xhr.onload = function () {
    xhr.onreadystatechange = function() {
        if (this.readyState == 4) {
            if (this.status == 200) {
                var listaAlunos = JSON.parse(this.responseText);
                console.log('DONE', xhr.readyState);
                
                // Percorre a lista e preenche a tabela
                for (var indice in listaAlunos) {
                    adicionaLinha(listaAlunos[indice]);
                }
            }
            else if (this.status == 500) {
                var erro = JSON.parse(this.responseText);
                console.log(erro.Message);
                console.log(erro.ExceptionMessage);
            }
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

function excluir(aluno) {
    bootbox.confirm({
        message: `Tem certeza que deseja excluir o aluno ${aluno.Nome}?`,
        buttons: {
            confirm: {
                label: 'Sim',
                className: 'btn-success'
            },
            cancel: {
                label: 'Não',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result){
                excluirAluno(aluno.Id);
                carregarAlunos();
            }
        }
    });    
}

function editarAluno(alunoEdit) {
    var btnSalvar = document.querySelector('#btnSalvar');    
    var titulo = document.querySelector("#titulo");

    // carregar dados no formulário para edição
    document.querySelector('#nome').value = alunoEdit.Nome;
    document.querySelector('#sobrenome').value = alunoEdit.SobreNome;
    document.querySelector('#telefone').value = alunoEdit.Telefone;
    document.querySelector('#ra').value = alunoEdit.RA;

    btnSalvar.textContent = 'Salvar';    
    exampleModalLabel.textContent = `Editar Aluno ${alunoEdit.Nome}`

    aluno = alunoEdit;            
}

function novoAluno(){
    var btnSalvar = document.querySelector('#btnSalvar');    
    var titulo = document.querySelector("#titulo");

    // limpar aluno
    aluno = {};
    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';

    btnSalvar.textContent = 'Cadastrar';    
    titulo.textContent = 'Cadastrar Aluno';
    
    $('#exampleModal').modal('show');
}

function cancelar() {    
    // limpar aluno
    aluno = {};
        
    $('#exampleModal').modal('hide');
}

function adicionaLinha(aluno) {
	// adicionar linhas na tabela
    var trow = `<tr>
    <td>${aluno.Nome}</td>
    <td>${aluno.SobreNome}</td>
    <td>${aluno.Telefone}</td>
    <td>${aluno.RA}</td>
    <td>
    <button class="btn btn-info" data-toggle="modal" data-target="#exampleModal" onclick='editarAluno(${JSON.stringify(aluno)})'>Editar</button>
    <button class="btn btn-danger" onclick='excluir(${JSON.stringify(aluno)})'>Excluir</button>
    </td>
    </td>`

    tbody.innerHTML += trow;
}

// carregar a listagem de aluno
carregarAlunos();

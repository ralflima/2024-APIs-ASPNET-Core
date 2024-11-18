using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("pessoa")]
public class PessoaControle : ControllerBase
{

    // Atributo PessoaRepositorio
    private readonly PessoaRepositorio _pessoaRepositorio;

    // Construtor
    public PessoaControle(PessoaRepositorio pessoaRepositorio)
    {
        _pessoaRepositorio = pessoaRepositorio;
    }

    // Rota de cadastro
    [HttpPost]
    public IActionResult Cadastrar([FromBody] Pessoa p)
    {
        if(p.Nome == "")
        {
            return BadRequest(new {mensagem = "O nome é obrigatório!"});
        }
        else if(p.Cidade == "")
        {
            return BadRequest(new {mensagem = "A cidade é obrigatória!"});
        }
        else if(p.Idade < 0 || p.Idade > 120)
        {
            return BadRequest(new {mensagem = "A idade precisa estar entre 0 e 120!"});
        }
        else
        {
            var obj = _pessoaRepositorio.CadastrarPessoa(p);
            return Created(string.Empty, obj);
        }
    }

    // Rota de seleção
    [HttpGet]
    public List<Pessoa> Selecionar()
    {
        return _pessoaRepositorio.SelecionarPessoas();
    }

    // Rota de alteração (localhost:5080/pessoa/1)
    [HttpPut("{codigo}")]
    public IActionResult Alterar(int codigo, [FromBody] Pessoa p)
    {

        if(!_pessoaRepositorio.ExistePessoa(codigo))
        {
            return NotFound(new {mensagem = "O código informado não existe!"});
        }
        else if(p.Nome == "")
        {
            return BadRequest(new {mensagem = "O nome é obrigatório!"});
        }
        else if(p.Cidade == "")
        {
            return BadRequest(new {mensagem = "A cidade é obrigatória!"});
        }
        else if(p.Idade < 0 || p.Idade > 120)
        {
            return BadRequest(new {mensagem = "A idade precisa estar entre 0 e 120!"});
        }
        else
        {
            p.Codigo = codigo;

            _pessoaRepositorio.AlterarPessoa(p);

            return Ok(p);    
        }   
        
    }

    // Rota de remoção
    [HttpDelete("{codigo}")]
    public IActionResult Remover(int codigo)
    {
        if(_pessoaRepositorio.ExistePessoa(codigo))
        {
            _pessoaRepositorio.RemoverPessoa(codigo);
            return Ok(new {mensagem = "Pessoa removida com sucesso!"});
        }
        else
        {
            return NotFound(new {mensagem = "Código não encontrado!"});
        }
    }

}
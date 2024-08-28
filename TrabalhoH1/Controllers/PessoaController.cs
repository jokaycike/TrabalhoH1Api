using Microsoft.AspNetCore.Mvc;

namespace TrabalhoH1.Controllers
{
    public class PessoaController : Controller
    {
        private static List<Pessoa> listaPessoa = new List<Pessoa>();

        [HttpPost]
        [Route("AdicionarPessoa")]
        public IActionResult AdicionarPessoa(Pessoa _novaPessoa)
        {
            listaPessoa.Add(_novaPessoa);
            return Ok("Nova pessoa criada!");
        }

        [HttpPut]
        [Route("AtualizarPessoa/{Cpf}")]

        public IActionResult AtualizarPessoa(string cpf, Pessoa _atualizadaPessoa)
        {
            Pessoa? qualPessoa = listaPessoa
                .Where(pessoa => pessoa.Cpf == cpf)
                .FirstOrDefault();
            if (qualPessoa is null)
                return NotFound($"O cpf {cpf} não encontrado");
            qualPessoa.Nome = _atualizadaPessoa.Nome;
            qualPessoa.Cpf = _atualizadaPessoa.Cpf;
            qualPessoa.Altura = _atualizadaPessoa.Altura;
            qualPessoa.Peso = _atualizadaPessoa.Peso;

            return Ok("Pessoa atualizada!");

        }

        [HttpDelete]
        [Route("RemoverPessoa")]

        public IActionResult RemoverPessoa(string cpfPessoa)
        {
            Pessoa? pessoaRemove = listaPessoa
                .Where(a => a.Cpf == cpfPessoa)
                .FirstOrDefault();
            if (pessoaRemove is null)
                return NotFound($"O cpf {cpfPessoa} não encontra");

            listaPessoa.Remove(pessoaRemove);
            return Ok($"Pessoa removido: {pessoaRemove.Nome}");
        }

        [HttpGet]
        [Route("TodasPessoas")]

        public IActionResult TodasPessoas()
        {
            return Ok(listaPessoa);
        }

        [HttpGet]
        [Route("PessoaPorCpf/{cpf}")]

        public IActionResult PessoaPorCpf(string cpf) 
        { 
            Pessoa? qualPessoa = listaPessoa
                .Where(a => a.Cpf == cpf)
                .FirstOrDefault();
            if (qualPessoa is null)
                return NotFound($"O cpf {cpf} não encontrado");
            
            return Ok(qualPessoa);
        }

        [HttpPost]
        [Route("PessoaIMCBom")]

        public IActionResult PessoaIMCBom()
        {
            List<Pessoa> _pessoasComIMCBom = new List<Pessoa>();

            for (int i = 0; i < listaPessoa.Count; i++)
            {
                Pessoa pessoaAtual = listaPessoa[i];
                double IMCAtual = pessoaAtual.Peso / (pessoaAtual.Altura * pessoaAtual.Altura);

                if (IMCAtual >= 18 && IMCAtual < 24)
                {
                    _pessoasComIMCBom.Add(pessoaAtual);
                }
            }

            return Ok(_pessoasComIMCBom);

        }

        [HttpGet]
        [Route("PorNome/{nome}")]
        public IActionResult PorNome(string nome)
        {
            var qualPessos = listaPessoa
                    .Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            return Ok(qualPessos);
        }
    }
}

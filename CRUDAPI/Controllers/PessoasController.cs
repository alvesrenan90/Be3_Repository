using System.Collections.Generic;
using System.Threading.Tasks;
using CRUD_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly Contexto _contexto;

        public PessoasController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> PegarTodosAsync()
        {
            return await _contexto.Pessoas.ToListAsync();
        }

        [HttpGet("{pessoaId}")]
        public async Task<ActionResult<Pessoa>> PegarPessoaPeloIdAsync(int pessoaId)
        {
            Pessoa pessoa = await _contexto.Pessoas.FindAsync(pessoaId);

            if (pessoa == null)
                return NotFound();

            return pessoa;
        }
        private static List<Pessoa> pessoas = new List<Pessoa>();
        [HttpPost]
        public async Task<ActionResult<Pessoa>> SalvarPessoaAsync(Pessoa pessoa)
        {
            foreach (Pessoa PessoaCadastrada in pessoas)
            {
                if (pessoa.CPF.Equals(PessoaCadastrada.CPF))
                {
                    Console.WriteLine("CPF JÁ EXISTE NA BASE, CADASTRO RECUSADO.");
                    return NotFound();
                }
                else
                {
                    await _contexto.Pessoas.AddAsync(pessoa);
                    await _contexto.SaveChangesAsync();
                    Console.WriteLine("CLIENTE CADASTRADO COM SUCESSO.");
                }
            }
            return Ok();
        }

   [HttpPut]
        public async Task<ActionResult> AtualizarPessoaAsync (Pessoa pessoa) {
            _contexto.Pessoas.Update (pessoa);
            await _contexto.SaveChangesAsync ();

            return Ok ();
        }

    [HttpDelete("{pessoaId}")]
    public async Task<ActionResult> ExcluirPessoaAsync(int pessoaId)
    {
        Pessoa pessoa = await _contexto.Pessoas.FindAsync(pessoaId);

        Console.WriteLine("Você não pode realizar essa operação.");
       // if (pessoa == null)
            return NotFound();

       // _contexto.Remove(pessoa);
        //await _contexto.SaveChangesAsync();
        //return Ok();
    }

}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDiarioNegociacoes.DB;
using ApiDiarioNegociacoes.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiDiarioNegociacoes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "free")]
    public class NegociacoesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public NegociacoesController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Negociacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Negociacao>>> GetNegociacoes()
        {
            return await _context.Negociacoes.Include(n=>n.Estrategia).AsNoTracking().ToListAsync();
        }

        // GET: api/Negociacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Negociacao>> GetNegociacao(int id)
        {
            var negociacao = await _context.Negociacoes.FindAsync(id);

            if (negociacao == null)
            {
                return NotFound();
            }

            return negociacao;
        }

        // PUT: api/Negociacoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNegociacao(int id, Negociacao negociacao)
        {
            if (id != negociacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(negociacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NegociacaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Negociacoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Negociacao>> PostNegociacao(Negociacao negociacao)
        {
            _context.Negociacoes.Add(negociacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNegociacao", new { id = negociacao.Id }, negociacao);
        }

        // DELETE: api/Negociacoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Negociacao>> DeleteNegociacao(int id)
        {
            var negociacao = await _context.Negociacoes.FindAsync(id);
            if (negociacao == null)
            {
                return NotFound();
            }

            _context.Negociacoes.Remove(negociacao);
            await _context.SaveChangesAsync();

            return negociacao;
        }

        private bool NegociacaoExists(int id)
        {
            return _context.Negociacoes.Any(e => e.Id == id);
        }
    }
}

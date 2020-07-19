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
    [Authorize(Roles ="free,premiun,adm")]
    public class EstrategiasController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public EstrategiasController(ApiDbContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estrategia>>> GetEstrategias()
        {
            return await _context.Estrategias.Where(e=>e.User.Username==User.Identity.Name).ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Estrategia>>> GetEstrategia(int id)
        {
            var estrategia = await _context.Estrategias.Where(e=>e.Id==id
                                                        &&e.User.Username==User.Identity.Name)
                                                        .Include(e=>e.Negociacoes).ToListAsync();

            if (estrategia.Count == 0)
            {
                return NotFound();
            }

            return estrategia;
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstrategia(int id,[FromBody] Estrategia estrategia)
        {
            if (id != estrategia.Id)
            {
                return BadRequest();
            }

            _context.Entry(estrategia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstrategiaExists(id))
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

        
        [HttpPost]
        public async Task<ActionResult<dynamic>> PostEstrategia(Estrategia estrategia)
        {
            
            estrategia.User = _context.Users.Where(u => u.Username.Equals(User.Identity.Name))
                                                    .Include(u => u.Estrategias).FirstOrDefault();

            _context.Estrategias.Add(estrategia);           

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstrategia", new { id = estrategia.Id }, estrategia);
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult<Estrategia>> DeleteEstrategia(int id)
        {
            var estrategia =  _context.Estrategias.Where(e => e.User.Username == User.Identity.Name
                                                                && e.Id == id).First();
                                                           
            if (estrategia == null)
            {
                return NotFound();
            }

            _context.Estrategias.Remove(estrategia);
            await _context.SaveChangesAsync();

            return estrategia;
        }

        private bool EstrategiaExists(int id)
        {
            return _context.Estrategias.Any(e => e.Id == id);
        }
    }
}

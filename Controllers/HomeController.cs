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
    public class HomeController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public HomeController(ApiDbContext context)
        {
            _context = context;
        }    

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody]User user)
        {
            bool result = _context.Users.AnyAsync(u => u.Email == user.Email).Result;

            if (!result)
            {
                return NotFound(new { message = "Usuario inexistente!" });
            }

            var usuario = _context.Users.Where(u => u.Email == user.Email). FirstOrDefault();

            if(usuario.Password != user.Password)
            {
                return NotFound(new { message = "Senha incorreta!" });
            }
            

            var token = Services.TokenService.GerarToken(usuario);

            user.Password = "";
            usuario.Password = "";
            
            return new
            {
                Usuario = new User { 
                    Id = usuario.Id,
                    Username = usuario.Username,
                    Email = usuario.Email,
                    Role = usuario.Role
                },
                
                Token = token
            };
        }

        [HttpPost]
        [Route("Cadastrar")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Cadastrar([FromBody]User user)
        {
            if (UserExists(user.Email))
            {
                ModelState.AddModelError(string.Empty, "Usuario já cadastrado!");
                return BadRequest(ModelState);
            }

            user.Role = "free";
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user.Password = "";
            return user;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Teste()
        {
            return  Ok(new { message = "olá" });
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private bool UserExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}

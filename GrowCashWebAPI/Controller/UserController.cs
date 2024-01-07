using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowCashWebAPI.Model;
using GrowCashWebAPI.Service;
using GrowCashWebAPI.Service.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace GrowCashWebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        private IAccountService _accountService;
        private IRevenueService _revenueService;
        private IExpenseService _expenseService;

        public UserController(
            IUserService userService,
            IAccountService accountService,
            IRevenueService revenueService,
            IExpenseService expenseService
        )
        {
            _userService = userService;
            _accountService = accountService;
            _revenueService = revenueService;
            _expenseService = expenseService;
        }

        [HttpGet]
        public IActionResult GetUserAll()
        {
            try
            {
                var usuarios = _userService.FindAll();

                if (usuarios.Count == 0) return NotFound(new { message = "Nenhum usuário encontrado." });
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor. ", ex });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserFindById(int id)
        {
            try
            {
                var usuario = _userService.FindById(id);
                if (usuario == null) return NotFound(new { message = "Nenhum usuário encontrado." });
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { message = "Erro interno do servidor. ", ex });
            }
        }
        
        [HttpGet("/account/{id}")]
        public IActionResult GetUserAccountFindById(int id)
        {
            try
            {
                var usuario = _userService.FindById(id);

                if (usuario == null)
                {
                    return NotFound(new { message = "Nenhum usuário encontrado." });
                }

                usuario.Account = _accountService.FindAll(usuario.Id);

                if (!usuario.Account.Any())
                {
                    return NotFound(new { message = "Nenhuma conta neste usuário foi encontrada." });
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor.", ex });
            }

        }
        [HttpPost]
        public IActionResult CreateUsuario([FromBody] UserModel user)
        {
            try
            {
                if (user == null) return BadRequest();
                return Ok(_userService.Create(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor. ", ex });
            }
        }

        [HttpPut]
        public IActionResult UpdateUsuario([FromBody] UserModel user)
        {
            try
            {
                if (user == null) return BadRequest();
                return Ok(_userService.Update(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor. ", ex });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            try
            {
                _userService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor. ", ex });
            }
        }
    }
}
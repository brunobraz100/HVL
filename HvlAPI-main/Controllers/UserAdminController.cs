using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hvl.models;
using hvl.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hvl.Controllers
{
    [Route("api/users")]
    [Authorize(Roles = "1")]
    public class UserAdminController : Controller
    {
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GellAll()
        {
            List<UserModel> users = new List<UserModel>();
            try
            {
                users = await new UserServices().GetUsers();
            }catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok(users);
        }

        [Route("AddUserAdmin")]
        [HttpPost]
        public async Task<IActionResult> AddUserAdmin([FromBody] UserModel user)
        {
            int output = 0;
            try
            {
                output = await new UserServices().AddUserAdmin(user: user);
                
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok(output);
        }

        [Route("UpdateUserAdmin")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserAdmin([FromBody] UserModel user)
        {
            string message = "";
            try
            {
                new UserServices().UpdateUserAdmin(user: user);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok("Administrador atualizado com sucesso.");
        }

        [Route("DeleteUserAdmin/{nmCPF}")]
        [HttpGet]
        public IActionResult DeleteUserAdmin(long nmCPF)
        {
            string message = "";

            try
            {
                new UserServices().DeleteUserAdmin(nmCPF: nmCPF);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok("Administrador excluído com sucesso.");
        }


    }
}

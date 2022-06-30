using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using hvlFront.services;
using hvlFront.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using System.Linq;

namespace hvlFront.Controllers
{   
    [Authorize(Roles = "1")]
    public class AdminController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.User.FindFirst("token").Value;
            List<AdminLoginViewModel> admins = new List<AdminLoginViewModel>();
            try
            {
                admins = await new AdminServices().GetAdmins(token: token);
            } catch(Exception ex)
            {}
            return View(admins);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdminLoginViewModel admin)
        {  
            string token = HttpContext.User.FindFirst("token").Value;
            admin.dsSenha = "Alterar123";
            string regex = "[./-]";
            string cpfSempontuacao = Regex.Replace(admin.nmCpfString, regex, "");
            admin.nmCpf = long.Parse(cpfSempontuacao);
            try
            {
                bool saved = await new AdminServices().AddAdmin(admin: admin, token: token);
                if(saved)
                {
                    ViewBag.retorno = "<p class='alert alert-success'>Administrador cadastrado com sucesso.</p>";
                } 
                else 
                {
                    ViewBag.retorno = "<p class='alert alert-danger'>Não foi possível adicionar este usuário</p>";
                }
            } catch(Exception ex)
            {
                ViewBag.retorno = "<p class='alert alert-danger'>" +ex.Message+ "</p>";
            }
            return View(admin);
        }

        public async Task<IActionResult> Edit(long nmCpf)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            AdminLoginViewModel admin = new AdminLoginViewModel();

            try
            {
                List<AdminLoginViewModel> admins = new List<AdminLoginViewModel>();
                admins = admins = await new AdminServices().GetAdmins(token: token);
                admin = admins.Where(w => w.nmCpf == nmCpf).FirstOrDefault();
                admin.nmCpfString = nmCpf.ToString();

            } catch(Exception ex)
            {

            }

            return View(admin);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AdminLoginViewModel admin)
        {  
            string token = HttpContext.User.FindFirst("token").Value;
            string regex = "[./-]";
            string cpfSempontuacao = Regex.Replace(admin.nmCpfString, regex, "");
            admin.nmCpf = long.Parse(cpfSempontuacao);
            try
            {
                bool saved = await new AdminServices().UpdateAdmin(admin: admin, token: token);
                if(saved)
                {
                    ViewBag.retorno = "<p class='alert alert-success'>Administrador atualizado com sucesso.</p>";
                } 
                else 
                {
                    ViewBag.retorno = "<p class='alert alert-danger'>Não foi possível adicionar este usuário</p>";
                }
            } catch(Exception ex)
            {
                ViewBag.retorno = "<p class='alert alert-danger'>" +ex.Message+ "</p>";
            }
            return View(admin);
        }

        
        [HttpGet]
        public async Task<IActionResult> DeleteAdministrador(long nmCpf)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            try
            {
                bool deleted = await new AdminServices().DeleteAdmin(nmCPF: nmCpf, token: token);
                if(!deleted)
                {
                    return Json(new {message= "Administrador não pode ser excluído.", complete = deleted});
                }
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            return Json(new {message = "Administrador excluído com sucesso.", complete = true});
        }
    }
}
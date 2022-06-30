using System;
using System.Threading.Tasks;
using hvlFront.Models;
using hvlFront.services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace hvlFront.Controllers
{
    public class CustomerController : Controller
    {
        [Authorize(Roles = "1")]
        public async Task<IActionResult> index()
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            string Token = HttpContext.User.FindFirst("token").Value;
            try 
            {
                customers = await new CustomerServices().GetAllCustomers(token: Token);
            } catch(Exception ex)
            {

            }

            return View(customers);
        }

        [Authorize(Roles = "1")]
        public IActionResult Add() 
        {
            return View();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IActionResult> Add(CustomerViewModel customer) 
        {
            string token = HttpContext.User.FindFirst("token").Value;
            customer.DsSenha = "Alterar123";
            
            string regex = "[./-]";
            string cnpjSemPontuacao = Regex.Replace(customer.NmCnpjString, regex, "");
            customer.NmCnpj = long.Parse(cnpjSemPontuacao);
            try
            {   
                bool output = await new CustomerServices().AddCustomer(customer: customer, token: token);
                if(output)
                {
                    ViewBag.retorno = "<p class='alert alert-success'>Cliente cadastrado com sucesso.</p>";    
                } 
                else 
                {
                    ViewBag.retorno = "<p class='alert alert-danger'>Cliente não pode cadastrado.</p>";    
                }
            } catch(Exception ex)
            {
                ViewBag.retorno = "<p class='alert alert-danger'>" +ex.Message+"</p>";
            }

            return View(customer);
        }

        [Authorize(Roles = "1")]
        public async Task<IActionResult> Edit(int IdCliente)
        {
            string token = HttpContext.User.FindFirst("token").Value;

            CustomerViewModel customer = new CustomerViewModel();
            try
            {
                List<CustomerViewModel> customers = new List<CustomerViewModel>(); 
                customers = await new CustomerServices().GetAllCustomers(token: token);
                customer = customers.Where(w => w.IdCliente == IdCliente).FirstOrDefault();
                customer.NmCnpjString = customer.NmCnpj.ToString();
            }
            catch(Exception ex)
            {

            }
            return View(customer);
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerViewModel customer)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            string regex = "[./-]";
            string cnpjSemPontuacao = Regex.Replace(customer.NmCnpjString, regex, "");
            customer.NmCnpj = long.Parse(cnpjSemPontuacao);

            try
            {   
                bool output = await new CustomerServices().EditCustomer(customer: customer, token: token);
                if(output)
                {
                    ViewBag.retorno = "<p class='alert alert-success'>Cliente atualizado com sucesso.</p>";    
                } 
                else 
                {
                    ViewBag.retorno = "<p class='alert alert-danger'>Cliente não pode atualizado.</p>";    
                }
            } catch(Exception ex)
            {
                ViewBag.retorno = "<p class='alert alert-danger'>" +ex.Message+"</p>";
            }

            return View(customer);
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IActionResult> RemoveCliente(int IdCliente)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            try
            {
                bool output = await new CustomerServices().RemoveCliente(IdCliente: IdCliente, token: token);
                if(!output)
                {
                    return Json(new {message = "Cliente não pode ser excluído.", complete = output});
                }
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            return Json(new {message = "Cliente excluído com sucesso.", complete = true});
        }

    }
}
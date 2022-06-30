using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using hvl.models;
using hvl.services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace hvl.Controllers
{
    [Route("api/Customers")]
    [Authorize]
    public class CustomerController : Controller
    {

        [HttpGet]
        [Route("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            try 
            {
                customers = new CustomerServices().GetAllCustomers();
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok(customers);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerModel  customer)
        {
            try
            {
                new CustomerServices().AddCustomer(customer: customer);
                string message = $"Bem vindo ao portal HVL Serviços. <a href='https://hvlservicos.com.br' target='_blank'> Clique neste link </a> para acessar o portal e atualizar sua senha. Sua senha inicial é '{customer.DsSenha}'";
                await new EmailServices().SendMail(to: customer.DsEmail, subject: "Bem vindo", message: message);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok("Cliente adicionado com sucesso!");
        }

        [HttpPost]
        [Route("UpdateCustomer")]
        public IActionResult UpdateCustomer([FromBody] CustomerModel customer)
        {
            try 
            {
                new CustomerServices().UpdateCustomer(customer: customer);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }


            return Ok("Cliente editado com sucesso.");
        }

        [HttpPost]
        [Route("ChangePasswordCustomer")]
        public IActionResult ChangePasswordCustomer([FromBody] CustomerModel customer)
        {
            try 
            {
                new CustomerServices().ChangePasswordCustomer(customer: customer);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }


            return Ok("Senha alterada com sucesso.");
        }


        [HttpGet]
        [Route("DeleteCustomer/{idCliente}")]
        public IActionResult DeleteCustomer(int idCliente)
        {
            try 
            {
                new CustomerServices().DeleteCustomer(IdCliente: idCliente);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }


            return Ok("Cliente excluído com sucesso.");
        }

        

    }
}
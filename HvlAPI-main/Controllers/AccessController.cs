using System;
using Microsoft.AspNetCore.Mvc;
using hvl.models;
using hvl.services;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace hvl.Controllers
{
    [Route("api/access")]
    public class AccessController : Controller
    {
        [Route("LoginAdmin")]
        [HttpPost]
        public IActionResult LoginAdmin([FromBody] AdminLoginModel adminLogin)
        {
            long output = 0;
            try 
            {
                output = new AccessServices().AdminLogin(adminLogin: adminLogin);
                if(output == 1 && adminLogin.dsSenha.Equals("Alterar123"))
                {
                    UserModel user = new UserModel()
                    {
                        nmCPF = adminLogin.nmCpf
                    };
                    string token = TokenServices.GenerateTokenAdmin(user: user);

                    return Ok(new {changePassword = true, token, nmCpf = adminLogin.nmCpf});
                } else if(output == 1 && !adminLogin.dsSenha.Equals("Alterar123"))
                {
                    UserModel user = new UserModel()
                    {
                        nmCPF = adminLogin.nmCpf
                    };
                    string token = TokenServices.GenerateTokenAdmin(user: user);

                    return Ok(new {changePassword = false, token, nmCpf = adminLogin.nmCpf});
                }
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            
            return Ok("Usuário ou senha inválidos!");
        }


        [Route("LoginCustomer")]
        [HttpPost]
        public IActionResult LoginCustomer([FromBody] CustomerLoginModel customerLogin)
        {
            long output = 0;
            try 
            {
                output = new AccessServices().CustomerLogin(customerLogin: customerLogin);
                if(output == 1 && customerLogin.dsSenha.Equals("Alterar123"))
                {
                    CustomerModel customer = new CustomerModel()
                    {
                        NmCnpj = customerLogin.nmCnpj
                    };
                    string token = TokenServices.GenerateTokenClient(customer: customer);

                    return Ok(new {changePassword = true, token, nmCnpj = customerLogin.nmCnpj });
                } else if(output == 1 && !customerLogin.dsSenha.Equals("Alterar123"))
                {
                    CustomerModel customer = new CustomerModel()
                    {
                        NmCnpj = customerLogin.nmCnpj
                    };
                    string token = TokenServices.GenerateTokenClient(customer: customer);

                    return Ok(new {changePassword = false, token, nmCnpj = customerLogin.nmCnpj});
                }
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            
            return Ok("Usuário ou senha inválidos!");
        }

        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] CustomerLoginModel customer)
        {
            try
            {
                string email = new AccessServices().ForgotPassword(customer: customer);

                if(!string.IsNullOrWhiteSpace(email))
                {
                    List<CustomerModel> customers = new List<CustomerModel>();
                    customers = new CustomerServices().GetAllCustomers();
                    long IdCliente = customers.Where(w => w.NmCnpj == customer.nmCnpj).FirstOrDefault().IdCliente;
                    CustomerModel customerModel = new CustomerModel()
                    {
                        IdCliente = IdCliente,
                        NmCnpj = customer.nmCnpj
                    };
                    string token = TokenServices.GenerateTokenClientForgot(customer: customerModel);
                    string message = $"Você solicitou a recuperação da sua senha de acesso ao sistema de gestão de documentos da HVL. \n\n <a href='http://hvlservicos.com.br/Account/ChangeForgot?token={token}&nmCnpj={customer.nmCnpj}'>Clique aqui para efetuar a troca.</a>";
                    bool sendEmail = await new EmailServices().SendMail(to: email, "Recuperação de Senha", message: message);
                    if(!sendEmail)
                    {
                        return Ok("O e-mail não pode ser enviado. Por favor entre em contato com o responsável do sistema.");
                    }
                    else 
                    {
                        return Ok("Link para recuperar a senha foi enviado para o e-mail deste cadastro.");
                    }
                }
                else 
                {
                    return Ok("Nenhum e-mail cadastrado neste CNPJ. Por favor verifique.");
                }

            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok();
        }
    }

}
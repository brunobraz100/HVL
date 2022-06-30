using hvlFront.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using hvlFront.services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace hvlFront.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> index(CustomerLoginViewModel customerLogin)
        {
            try
            {
                LoginModel login = await new AccountServices().CustomerLogin(customerLogin: customerLogin);

                if(!string.IsNullOrWhiteSpace(login.token))
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, login.nmCnpj.ToString()),
                        new Claim(ClaimTypes.Role, "2"),
                        new Claim("token", login.token)
                    };

                    try
                    {

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme
                        );

                        AuthenticationProperties authenticationProperties = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.UtcNow.AddHours(2)
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), authenticationProperties);
                    } catch(Exception ex)
                    {
                        throw ex;
                    }


                    if(login.changePassword)
                    {
                        return RedirectToAction("ChangeFirstPassword", "Account");
                    }
                    
                    return RedirectToAction("index", "Home");
                }
            } catch(Exception ex)
            {
                ViewBag.retorno = "<p class='alert alert-danger'>" +ex.Message+"</p>";
            }
            return View(customerLogin);
        }

        public IActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Admin(AdminLoginViewModel adminLogin)
        {
            try
            {
                LoginModel login = await new AccountServices().AdminLongin(adminLogin: adminLogin);

                if(!string.IsNullOrWhiteSpace(login.token))
                {
                    
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, login.nmCpf.ToString()),
                        new Claim(ClaimTypes.Role, "1"),
                        new Claim("token", login.token)
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                                claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties authProperties = new AuthenticationProperties
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTime.UtcNow.AddHours(2),
                                AllowRefresh = true
                            };

                    await HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity),
                                authProperties);

                    if(login.changePassword)
                    {
                        return RedirectToAction("ChangeFirstPassword", "Account");
                    }
                    
                    return RedirectToAction("index", "Home");
                }
            } catch(Exception ex)
            {
                ViewBag.retorno = "<p class='alert alert-danger'>" +ex.Message+"</p>";
            }
            return View(adminLogin);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            string role = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Role).Value;

            if(!changePassword.NewPassword.Equals(changePassword.NewPassword))
            {   
                ViewBag.retorno = "<p class='alert alert-danger'>A nova senha e a sua confirmação devem ser iguais.</p>";
                return View(changePassword);
            }

            if(string.IsNullOrWhiteSpace(changePassword.ActualPassword))
            {   
                ViewBag.retorno = "<p class='alert alert-danger'>A sua senha atual precisa ser preenchida.</p>";
                return View(changePassword);
            }

            if(role == "1")
            {
                AdminLoginViewModel adminLogin = new AdminLoginViewModel()
                {
                    nmCpf = long.Parse(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value),
                    dsSenha = changePassword.NewPassword
                };

                bool change = await new AdminServices().UpdateAdmin(admin: adminLogin, token: token);
                if(change)
                {
                    ViewBag.retorno = "<p class='alert alert-success'>Senha alterada com sucesso.</p>";
                }
                else 
                {
                    ViewBag.retorno = "<p class='alert alert-danger'>Não foi possível trocar sua senha.</p>";
                }
            } 
            else 
            {
                long nmCnpj = long.Parse(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value);
                List<CustomerViewModel> customers = new List<CustomerViewModel>();
                customers = await new CustomerServices().GetAllCustomers(token: token);
                CustomerViewModel customer = customers.Where(w => w.NmCnpj == nmCnpj).FirstOrDefault();
                customer.DsSenha = changePassword.NewPassword;

                bool change = await new CustomerServices().ChangePassword(customer: customer, token: token);
                if(change)
                {
                    ViewBag.retorno = "<p class='alert alert-success'>Senha alterada com sucesso.</p>";
                }
                else 
                {
                    ViewBag.retorno = "<p class='alert alert-danger'>Não foi possível trocar sua senha.</p>";
                }

            }
            return View();
        }
        
        [Authorize]
        public IActionResult ChangeFirstPassword()
        {
            return View();
        }

        public async Task<IActionResult> ChangeForgot(string token, long nmCnpj)
        {
            
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, nmCnpj.ToString()),
                new Claim(ClaimTypes.Role, "2"),
                new Claim("token", token)
            };

            try
            {

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme
                );

                AuthenticationProperties authenticationProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(2)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authenticationProperties);
            } catch(Exception ex)
            {

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeFirstPassword(ChangePasswordViewModel changePassword)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            string role = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Role).Value;

            if(!changePassword.NewPassword.Equals(changePassword.ComfirmPassword))
            {   
                ViewBag.retorno = "<p class='alert alert-danger'>A nova senha e a sua confirmação devem ser iguais.</p>";
                return View(changePassword);
            }

            if(role == "1")
            {
                AdminLoginViewModel adminLogin = new AdminLoginViewModel()
                {
                    nmCpf = long.Parse(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value),
                    dsSenha = changePassword.NewPassword
                };

                bool change = await new AdminServices().UpdateAdmin(admin: adminLogin, token: token);
                if(change)
                {
                    ViewBag.retorno = "<p class='alert alert-success'>Senha alterada com sucesso.</p>";
                }
                else 
                {
                    ViewBag.retorno = "<p class='alert alert-danger'>Não foi possível trocar sua senha.</p>";
                }
            } 
            else 
            {
                long nmCnpj = long.Parse(HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value);
                List<CustomerViewModel> customers = new List<CustomerViewModel>();
                customers = await new CustomerServices().GetAllCustomers(token: token);
                CustomerViewModel customer = customers.Where(w => w.NmCnpj == nmCnpj).FirstOrDefault();
                customer.DsSenha = changePassword.NewPassword;

                bool change = await new CustomerServices().ChangePassword(customer: customer, token: token);
                if(change)
                {
                    ViewBag.retorno = "<p class='alert alert-success'>Senha alterada com sucesso.</p>";
                }
                else 
                {
                    ViewBag.retorno = "<p class='alert alert-danger'>Não foi possível trocar sua senha.</p>";
                }

            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(CustomerLoginViewModel customer)
        {
            try
            {
                string message = await new AccountServices().ForgotPassword(customer: customer);
                if(message.Equals("Link para recuperar a senha foi enviado para o e-mail deste cadastro."))
                {
                    ViewBag.retorno = "<p class='alert alert-success'>"+message+"</p>";
                }
                else 
                {
                    ViewBag.retorno = "<p class='alert alert-danger'>"+message+"</p>";
                }

            } catch(Exception ex)
            {
                ViewBag.retorno = "<p class='alert alert-danger'>"+ex.Message+"</p>";
            }
            return View();
        }
    }
}
using hvlFront.Models;
using hvlFront.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace hvlFront.Controllers
{   
    [Authorize]
    public class AgendaVencimentoController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(IFormFile vencimentos)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            if(vencimentos != null)
            {
                List<AgendaVencimentoModel> vencimentosModel = new List<AgendaVencimentoModel>();
                using(StreamReader sr = new StreamReader(vencimentos.OpenReadStream()))
                {
                    string[] headers = sr.ReadLine().Split("\n");
                    while(!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(";");
                        
                        if(!string.IsNullOrWhiteSpace(line[0]) || !string.IsNullOrWhiteSpace(line[1]) || !string.IsNullOrWhiteSpace(line[2]) ||
                        !string.IsNullOrWhiteSpace(line[3]) || !string.IsNullOrWhiteSpace(line[4]) || !string.IsNullOrWhiteSpace(line[5]))
                        {
                            string regex = "[./-]";
                            string cnpjSemPontuacao = Regex.Replace(line[1], regex, "");
                            string[] dateSplited = line[5].Split("/");
                            DateTime dtVencimento = new DateTime(
                                int.Parse(dateSplited[2]),
                                int.Parse(dateSplited[1]),
                                int.Parse(dateSplited[0])
                            );
                            AgendaVencimentoModel vencimento = new AgendaVencimentoModel()
                            {
                                DsCliente = line[0],
                                NmCnpj = long.Parse(cnpjSemPontuacao),
                                DsPlaca = line[2],
                                DsUF = line[3],
                                DsAET = line[4],
                                DtVencimento = dtVencimento
                            };

                            vencimentosModel.Add(vencimento);
                        }
                    }

                    try
                    {
                        bool saved = await new AgendaVencimentoServices().AddVencimentos(vencimentos: vencimentosModel, token: token);
                        if(saved)
                        {
                            ViewBag.retorno = "<p class='alert alert-success'>Vencimentos cadastrados com sucesso.</p>";    
                        }
                        else 
                        {
                            ViewBag.retorno = "<p class='alert alert-danger'>Um problema ocorreu ao tentar salvar estes agendamentos</p>";    
                        }
                    } catch(Exception ex)
                    {
                        ViewBag.retorno = "<p class='alert alert-danger'>"+ex.Message+"</p>";    
                    }
                }
            }
            return View();
        }

         public IActionResult Remove()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Remove(IFormFile vencimentos)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            if(vencimentos != null)
            {
                List<AgendaVencimentoModel> vencimentosModel = new List<AgendaVencimentoModel>();
                using(StreamReader sr = new StreamReader(vencimentos.OpenReadStream()))
                {
                    string[] headers = sr.ReadLine().Split("\n");
                    while(!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(";");
                        
                        if(!string.IsNullOrWhiteSpace(line[0]) || !string.IsNullOrWhiteSpace(line[1]) || !string.IsNullOrWhiteSpace(line[2]) ||
                        !string.IsNullOrWhiteSpace(line[3]) || !string.IsNullOrWhiteSpace(line[4]) || !string.IsNullOrWhiteSpace(line[5]))
                        {
                            string regex = "[./-]";
                            string cnpjSemPontuacao = Regex.Replace(line[1], regex, "");
                            string[] dateSplited = line[5].Split("/");
                            DateTime dtVencimento = new DateTime(
                                int.Parse(dateSplited[2]),
                                int.Parse(dateSplited[1]),
                                int.Parse(dateSplited[0])
                            );
                            
                            AgendaVencimentoModel vencimento = new AgendaVencimentoModel()
                            {
                                DsCliente = line[0],
                                NmCnpj = long.Parse(cnpjSemPontuacao),
                                DsPlaca = line[2],
                                DsUF = line[3],
                                DsAET = line[4],
                                DtVencimento = dtVencimento
                            };

                            vencimentosModel.Add(vencimento);
                        }
                    }

                    try
                    {
                        bool saved = await new AgendaVencimentoServices().RemoveVencimentos(vencimentos: vencimentosModel, token: token);
                        if(saved)
                        {
                            ViewBag.retorno = "<p class='alert alert-success'>Vencimentos removidos com sucesso.</p>";    
                        }
                        else 
                        {
                            ViewBag.retorno = "<p class='alert alert-danger'>Um problema ocorreu ao tentar remover estes agendamentos</p>";    
                        }
                    } catch(Exception ex)
                    {
                        ViewBag.retorno = "<p class='alert alert-danger'>"+ex.Message+"</p>";    
                    }
                }
            }
            return View();
        }
    }
}
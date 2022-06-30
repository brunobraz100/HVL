using System;
using System.Collections.Generic;
using hvl.models;
using hvl.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hvl.Controllers
{
    [Authorize]
    [Route("api/vencimentos")]
    public class AgendaVencimentoController : Controller
    {
        [AllowAnonymous]
        [Route("ListVencimentos")]
        [HttpGet]
        public IActionResult ListVencimentos()
        {   
            string validate = Request.Headers["x-api-key"].ToString();
            if(!validate.Equals(Settings.xApiKey))
            {
                return BadRequest("Não autorizado!");
            }
            List<AgendaVencimentoModel> vencimentos = new List<AgendaVencimentoModel>();
            try
            {   
                vencimentos = new AgendaVencimentoServices().GetVencimentos();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(vencimentos);
        }

        [Route("UploadVencimentos")]
        [HttpPost]
        public IActionResult UploadVencimentos([FromBody] List<AgendaVencimentoModel> vencimentos)
        {
            try
            {
                foreach(AgendaVencimentoModel vencimento in vencimentos)
                {
                    new AgendaVencimentoServices().AddAgendaVencimento(agendaVencimento: vencimento);
                }
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Vencimentos carregados com sucesso!");
        }


        [Route("RemoveVencimentos")]
        [HttpPost]
        public IActionResult RemoveVencimentos([FromBody] List<AgendaVencimentoModel> vencimentos)
        {
            try
            {
                foreach(AgendaVencimentoModel vencimento in vencimentos)
                {
                    new AgendaVencimentoServices().RemoveAgendaVencimento(agendaVencimento: vencimento);
                }
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Vencimentos removidos com sucesso!");
        }
    }
}
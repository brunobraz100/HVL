using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using hvl.models;
using hvl.services;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace hvl.Controllers
{

    [Route("api/documents")]
    [Authorize]
    public class DocumnetController : Controller
    {
        [Route("GetDocumentsByClient/{IdCliente}")]
        [HttpGet]
        public IActionResult GetDocumentsByClient(long IdCliente)
        {
            List<DocumentModel> documents = new List<DocumentModel>();

            try
            {
                documents = new DocumentServices().GetDocuments(IdCliente: IdCliente);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok(documents);
        }

        [Route("GetSingleDocument/{IdDocumento}")]
        [HttpGet]
        public IActionResult GetSingleDocument(long IdDocumento)
        {
            DocumentModel document = new DocumentModel();

            try
            {
                document = new DocumentServices().GetSigleDocument(IdDocumento: IdDocumento);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok(document);
        }

        [Route("GetSingleDocumentClientId/{IdCliente}")]
        [HttpGet]
        public IActionResult GetSingleDocumentClientId(long IdCliente)
        {
            DocumentModel document = new DocumentModel();

            try
            {
                document = new DocumentServices().GetSigleDocumentClientId(IdCliente: IdCliente);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok(document);
        }


        [Route("GetDocument/{IdDocumento}")]
        [HttpGet]
        public async Task<IActionResult> GetDocument(long IdDocumento)
        {
            byte[] document = null;

            try
            {
                document = await new DocumentServices().GetDocument(IdDocumento: IdDocumento);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok(document);
        }

        [Route("AddDocumento")]
        [HttpPost]
        public async Task<IActionResult> AddDocument([FromBody] DocumentModel document)
        {
            string cpf = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            long nmCPF = long.Parse(cpf);
            try
            {
                new DocumentServices().AdicionaDocumento(document: document, nmCPF: nmCPF);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok("Documento salvo com sucesso.");
        }

        [Route("DeletarDocumento/{idDocumento}")]
        [HttpGet]
        public IActionResult DeletarDocumento(long idDocumento)
        {
            try
            {
                new DocumentServices().ExcluiDocumento(IdDocumento: idDocumento);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok("Documento removido com sucesso.");
        }


    }

}
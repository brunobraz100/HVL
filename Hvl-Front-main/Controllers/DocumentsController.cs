using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using hvlFront.Models;
using hvlFront.services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace hvlFront.Controllers
{   
    public class DocumentsController : Controller
    {
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Index(int IdCliente)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            List<DocumentModel> documents = new List<DocumentModel>();
            try
            {
                documents = await new DocumentServices().GetDocuments(IdCliente: IdCliente, token: token);
                ViewBag.IdCliente = IdCliente;
            } catch(Exception ex){}
            return View(documents);
        }

        [Authorize(Roles = "2")]
        public async Task<IActionResult> MyDocuments()
        {
            string token = HttpContext.User.FindFirst("token").Value;
            string identity = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;

            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            List<DocumentModel> documents = new List<DocumentModel>();
            try
            {
                customers = await new CustomerServices().GetAllCustomers(token: token);
                CustomerViewModel currentCustomer = customers.Where(w => w.NmCnpj == long.Parse(identity)).FirstOrDefault();
                long IdCliente = currentCustomer.IdCliente; 
                documents = await new DocumentServices().GetDocuments(IdCliente: IdCliente, token: token);
            } catch(Exception ex){}
            return View(documents);
        }

        [HttpGet]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> DownloadFile(long IdDocumento)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            string nmCnpj = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            DocumentModel documentModel = new DocumentModel();
            try
            {


                documentModel = await new DocumentServices().GetSingleDocumentById(documentId: IdDocumento, token: token);
                var fileBytes = await new DocumentServices().GetDocumentBytes(IdDocument: IdDocumento, token: token);
                documentModel.LgDocumento = fileBytes;
            } catch(Exception ex)
            {
                
            }
            return File(documentModel.LgDocumento, "application/force-download", documentModel.DsDocumento + documentModel.DsExtensao);
        }

        [Authorize(Roles = "1")]
        public async Task<IActionResult> Add(int IdCliente)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            try
            {
                ViewBag.categoria = await new CategoryServices().GetAllCategories(token: token);
                ViewBag.IdCliente = IdCliente;
            } catch(Exception){}
            return View();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IActionResult> Add(IFormFile  document, DocumentModel documentModel)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            try
            {
                if(document != null)
                {
                    using(MemoryStream memory = new MemoryStream())
                    {
                        document.CopyTo(memory);
                        documentModel.LgDocumento = memory.ToArray();
                    }
                    string[] vencimento = documentModel.vencimento.Split("/");
                    string[] documentSplited = document.FileName.Split(".");
                    documentModel.DsExtensao = "." + documentSplited[documentSplited.Length - 1];
                    documentModel.DtValidade = new DateTime(int.Parse(vencimento[2]), int.Parse(vencimento[1]), int.Parse(vencimento[0]));
                    
                    if(string.IsNullOrWhiteSpace(documentModel.DsDocumento))
                    {
                        documentModel.DsDocumento = documentSplited[0];
                    }

                    bool saved = await new DocumentServices().AddDocument(document: documentModel, token: token);
                    if(saved)
                    {
                        ViewBag.retorno = "<p class='alert alert-success'>Documento salvo com sucesso.</p>";    
                    }
                    else 
                    {
                        ViewBag.retorno = "<p class='alert alert-danger'>Documento não pode ser salvo.</p>";    
                    }
                }
                else 
                {
                    ViewBag.retorno = "<p class='alert alert-danger'>Um arquivo precisa ser anexado.</p>";    
                }

                ViewBag.categoria = await new CategoryServices().GetAllCategories(token: token);
                ViewBag.IdCliente = documentModel.IdCliente;
            } catch(Exception ex)
            {
                ViewBag.retorno = "<p class='alert alert-danger'>"+ex.Message+"</p>";    

            }
            return View();
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IActionResult> DeleteDocument(long IdDocument)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            try
            {
                bool deleted = await new DocumentServices().DeleteDocument(IdDocument: IdDocument, token: token);
                if(!deleted)
                {
                    return Json(new {message = "Documento não pode ser removido.", complete = deleted});        
                }
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            return Json(new {message = "Documento exclído com sucesso.", complete = true});
        }
    }
}
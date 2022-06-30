using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using hvlFront.Models;
using hvlFront.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace hvlFront.Controllers
{
    [Authorize(Roles = "1")]
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.User.FindFirst("token").Value;
            List<CategoryModel> categories = new List<CategoryModel>();
            try
            {
                categories = await new CategoryServices().GetAllCategories(token: token);
            } catch(Exception ex){}
            return View(categories);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryModel category)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            if(string.IsNullOrWhiteSpace(category.DsCategoria))
            {
                ViewBag.retorno = "<p class='alert alert-danger'>Você precisa informar um nome para esta categoria.</p>";
                return View(category);
            }

            try
            {
                bool saved = await new CategoryServices().AddCategory(category: category, token: token);
                if(saved)
                {
                    ViewBag.retorno = "<p class='alert alert-success'>Categoria adicionanda com sucesso.</p>";
                } 
                else 
                {
                    ViewBag.retorno = "<p class='alert alert-danger'>Categoria não poder ser adicionada.</p>";
                }
            } catch(Exception ex)
            {
                ViewBag.retorno = "<p class='alert alert-danger'>"+ex.Message+"</p>";
            }
            return View(category);
        }


        public async Task<IActionResult> Edit(int IdCategoria)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            CategoryModel category = new CategoryModel();
            try
            {
                List<CategoryModel> categories = new List<CategoryModel>();
                categories = await new CategoryServices().GetAllCategories(token: token);
                category = categories.Where(w => w.IdCategoria == IdCategoria).FirstOrDefault();
            } catch(Exception){}
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryModel category)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            if(string.IsNullOrWhiteSpace(category.DsCategoria))
            {
                ViewBag.retorno = "<p class='alert alert-danger'>Você precisa informar um nome para esta categoria.</p>";
                return View(category);
            }

            try
            {
                bool saved = await new CategoryServices().EditCategory(category: category, token: token);
                if(saved)
                {
                    ViewBag.retorno = "<p class='alert alert-success'>Categoria atualizada com sucesso.</p>";
                } 
                else 
                {
                    ViewBag.retorno = "<p class='alert alert-danger'>Categoria não poder ser atualizada.</p>";
                }
            } catch(Exception ex)
            {
                ViewBag.retorno = "<p class='alert alert-danger'>"+ex.Message+"</p>";
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int IdCategoria)
        {
            string token = HttpContext.User.FindFirst("token").Value;
            try
            {
                bool deleted = await new CategoryServices().DeleteCategory(idCategory: IdCategoria, token:token);
                if(!deleted)
                {
                    return Json(new {message = "Esta categoria não pode ser excluída.", complete = deleted});
                }
            } catch(Exception ex)
            {
                throw ex;
            }
            return Json(new {message = "Categoria excluída com sucesso.", complete = true});
        }


    }
}
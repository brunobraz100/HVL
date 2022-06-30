using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using hvl.models;
using hvl.services;
using Microsoft.AspNetCore.Authorization;

namespace hvl.Controllers
{

    [Route("api/categories")]
    [Authorize]
    public class CategoryController : Controller
    {
        [Route("GetAllCategories")]
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            List<CategoryModel> categories = new List<CategoryModel>();

            try
            {
                categories = new CategoryServices().GetAllCategories();
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok(categories);
        }

        [Route("AddCategory")]
        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryModel category)
        {
            try 
            {
                new CategoryServices().AddCategory(category: category);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok("Categoria cadastrada com sucesso.");
        }

        [Route("UpdateCategory")]
        [HttpPost]
        public IActionResult UpdateCategory([FromBody] CategoryModel category)
        {
            try 
            {
                new CategoryServices().UpdateCategory(category: category);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok("Categoria atualizada com sucesso.");
        }

        [Route("DeleteCategory/{idCategory}")]
        [HttpGet]
        public IActionResult DeleteCategory(int idCategory)
        {
            try 
            {
                new CategoryServices().DeleteCategory(idCategory: idCategory);
            } catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok("Categoria excluída com sucesso.");
        }

    }
}
using System;
using System.Threading.Tasks;
using hvlFront.Models;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace hvlFront.services
{
    public class CategoryServices
    {
        private readonly string baseUrl;
        public CategoryServices()
        {
            baseUrl = Settings.baseUrl;
        }

        public async Task<List<CategoryModel>> GetAllCategories(string token)
        {
            List<CategoryModel> categories = new List<CategoryModel>();

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                try 
                {
                    string response = await client.GetStringAsync(baseUrl + "categories/GetAllCategories");
                    categories = JsonConvert.DeserializeObject<List<CategoryModel>>(response);
                } catch(Exception ex)
                {
                    throw ex;
                }

            }

            return categories;           
        }

        public async Task<bool> AddCategory(CategoryModel category, string token)
        {
            bool complete = true;

            string jsonCategory = JsonConvert.SerializeObject(category);
            StringContent content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                try
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(baseUrl + "categories/AddCategory", content);
                    if(!responseMessage.IsSuccessStatusCode)
                    {
                        complete = false;
                    }
                } catch(Exception ex)
                {
                    throw ex;
                }
            }
            return complete;
        }

        public async Task<bool> EditCategory(CategoryModel category, string token)
        {
            bool complete = true;

            string jsonCategory = JsonConvert.SerializeObject(category);
            StringContent content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                try
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(baseUrl + "categories/UpdateCategory", content);
                    if(!responseMessage.IsSuccessStatusCode)
                    {
                        complete = false;
                    }
                } catch(Exception ex)
                {
                    throw ex;
                }
            }
            return complete;
        }

        public async Task<bool> DeleteCategory(int idCategory, string token)
        {
            bool complete = true;

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                try
                {
                    string responseMessage = await client.GetStringAsync(baseUrl + "categories/DeleteCategory/" + idCategory);
                    if(!responseMessage.Contains("Categoria excluída com sucesso."))
                    {
                        complete = false;
                    }
                } catch(Exception ex)
                {
                    throw ex;
                }
            }
            return complete;
        }
    }
}
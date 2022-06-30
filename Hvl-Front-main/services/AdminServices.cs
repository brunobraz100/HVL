using System;
using System.Collections.Generic;
using hvlFront.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace hvlFront.services
{
    public class AdminServices
    {
        private readonly string baseUrl;
        public AdminServices()
        {
            baseUrl = Settings.baseUrl;
        }
        public async Task<List<AdminLoginViewModel>> GetAdmins(string token)
        {
            List<AdminLoginViewModel> admins = new List<AdminLoginViewModel>();

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                try
                {
                    string response = await client.GetStringAsync(baseUrl + "users/GetAll");
                    admins = JsonConvert.DeserializeObject<List<AdminLoginViewModel>>(response);
                }   
                catch(Exception ex)
                {
                    throw ex;
                }
            }

            return admins;
        }

        public async Task<bool> AddAdmin(AdminLoginViewModel admin, string token)
        {
            bool complete = true;

            string jsonAdmin = JsonConvert.SerializeObject(admin);
            StringContent content = new StringContent(jsonAdmin, Encoding.UTF8, "application/json");

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                try
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(baseUrl + "users/AddUserAdmin", content);
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


        public async Task<bool> UpdateAdmin(AdminLoginViewModel admin, string token)
        {
            bool complete = true;

            string jsonAdmin = JsonConvert.SerializeObject(admin);
            StringContent content = new StringContent(jsonAdmin, Encoding.UTF8, "application/json");

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                try
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(baseUrl + "users/UpdateUserAdmin", content);
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

        public async Task<bool> DeleteAdmin(long nmCPF, string token)
        {
            bool complete = true;

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                
                try
                {
                    string response = await client.GetStringAsync(baseUrl + "users/DeleteUserAdmin/" + nmCPF);
                    if(!response.Contains("Administrador excluído com sucesso."))
                    {
                        complete = false;
                    }
                }catch(Exception ex)
                {
                    throw ex;
                }
            }

            return complete;
        }
    }
}
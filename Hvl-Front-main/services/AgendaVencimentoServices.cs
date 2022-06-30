using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using hvlFront.Models;
using Newtonsoft.Json;

namespace hvlFront.services
{
    public class AgendaVencimentoServices
    {
        private readonly string baseUrl;
        public AgendaVencimentoServices()
        {
            baseUrl = Settings.baseUrl;
        }

        public async Task<bool> AddVencimentos(List<AgendaVencimentoModel> vencimentos, string token)
        {
            bool complete = true;

            using(HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(36000);
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                
                try
                {
                    string vencimentosJson = JsonConvert.SerializeObject(vencimentos);
                    StringContent content = new StringContent(vencimentosJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage responseMessage = await client.PostAsync($"{baseUrl}vencimentos/UploadVencimentos", content);
                    if(!responseMessage.IsSuccessStatusCode)
                    {
                        complete = false;
                    }
                    string output = await responseMessage.Content.ReadAsStringAsync();
                } catch(Exception ex)
                {
                    throw ex;
                }
            }

            return complete;
        }


        public async Task<bool> RemoveVencimentos(List<AgendaVencimentoModel> vencimentos, string token)
        {
            bool complete = true;

            using(HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(36000);
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                
                try
                {
                    string vencimentosJson = JsonConvert.SerializeObject(vencimentos);
                    StringContent content = new StringContent(vencimentosJson, Encoding.UTF8, "application/json");

                    HttpResponseMessage responseMessage = await client.PostAsync($"{baseUrl}vencimentos/RemoveVencimentos", content);
                    if(!responseMessage.IsSuccessStatusCode)
                    {
                        complete = false;
                    }
                    string output = await responseMessage.Content.ReadAsStringAsync();
                } catch(Exception ex)
                {
                    throw ex;
                }
            }

            return complete;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using hvlMailer.Models;
using Newtonsoft.Json;

namespace hvlMailer.Services
{
    public class AgendaVencimentoServices
    {
        private readonly string baseUrl;
        public AgendaVencimentoServices()
        {
            baseUrl = Settings.Url;
        }


        public async Task<List<AgendaVencimentoModel>> ListaVencimentos()
        {
            List<AgendaVencimentoModel> vencimentoModels = new List<AgendaVencimentoModel>();
            
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-api-key", Settings.xApiKey); 
                    
                try
                {
                    string output = await client.GetStringAsync($"{baseUrl}vencimentos/ListVencimentos");
                    vencimentoModels = JsonConvert.DeserializeObject<List<AgendaVencimentoModel>>(output);
                    
                } catch(Exception ex)
                {
                    throw new Exception("Erro ao buscar vencimento na API. " + ex.Message);
                }
            }

            return vencimentoModels;
        }

    }
}
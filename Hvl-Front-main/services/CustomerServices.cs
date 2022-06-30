using System;
using System.Net.Http;
using hvlFront.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace hvlFront.services
{
    public class CustomerServices
    {   
        private readonly string baseUrl;
        public CustomerServices()
        {
            baseUrl = Settings.baseUrl;
        }
        public async Task<List<CustomerViewModel>> GetAllCustomers(string token)
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                try 
                {
                    string output = await client.GetStringAsync(baseUrl + "Customers/GetAllCustomers");
                    customers = JsonConvert.DeserializeObject<List<CustomerViewModel>>(output);
                } catch(Exception ex)
                {
                    throw ex;
                }
            }

            return customers;
        }

        public async Task<bool> AddCustomer(CustomerViewModel customer, string token)
        {
            bool complete = true;

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                string jsonCustormer = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(jsonCustormer, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(baseUrl + "customers/AddCustomer", content);
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

        public async Task<bool> EditCustomer(CustomerViewModel customer, string token)
        {
            bool complete = true;

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                string jsonCustormer = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(jsonCustormer, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(baseUrl + "customers/UpdateCustomer", content);
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


        public async Task<bool> ChangePassword(CustomerViewModel customer, string token)
        {
            bool complete = true;

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                string jsonCustormer = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(jsonCustormer, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(baseUrl + "customers/ChangePasswordCustomer", content);
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

    
        public async Task<bool> RemoveCliente(int IdCliente, string token)
        {
            bool complete = true;

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                try
                {
                    string responseMessage = await client.GetStringAsync(baseUrl + "customers/DeleteCustomer/" + IdCliente);
                    if(!responseMessage.Contains("Cliente excluído com sucesso."))
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
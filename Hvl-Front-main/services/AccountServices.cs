using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using hvlFront.Models;
using System.Text;

namespace hvlFront.services
{
    public class AccountServices
    {   
        private readonly string baseUrl;

        public AccountServices()
        {
            baseUrl = Settings.baseUrl;
        }

        public async Task<LoginModel> CustomerLogin(CustomerLoginViewModel customerLogin)
        {
            LoginModel login = new LoginModel();

            using(HttpClient client = new HttpClient())
            {
                string jsonCustomer = JsonConvert.SerializeObject(customerLogin);
                StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

                try 
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(baseUrl + "access/LoginCustomer", content);

                    if(responseMessage.IsSuccessStatusCode)
                    {
                        string output = await responseMessage.Content.ReadAsStringAsync();
                        try 
                        {
                            login = JsonConvert.DeserializeObject<LoginModel>(output);
                        } 
                        catch{
                            throw new Exception(output);
                        }
                    }
                } catch(Exception ex)
                {
                    throw ex;
                }
            }
            return login;
        }

        public async Task<LoginModel> AdminLongin(AdminLoginViewModel adminLogin)
        {
            LoginModel login = new LoginModel();

            using(HttpClient client = new HttpClient())
            {
                string jsonCustomer = JsonConvert.SerializeObject(adminLogin);
                StringContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

                try 
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(baseUrl + "access/LoginAdmin", content);

                    if(responseMessage.IsSuccessStatusCode)
                    {
                        string output = await responseMessage.Content.ReadAsStringAsync();
                        try 
                        {
                            login = JsonConvert.DeserializeObject<LoginModel>(output);
                        } 
                        catch{
                            throw new Exception(output);
                        }
                    }
                } catch(Exception ex)
                {
                    throw ex;
                }
            }

            return login;
        }

        public async Task<string> ForgotPassword(CustomerLoginViewModel customer)
        {
            string output = "";

            using(HttpClient client = new HttpClient())
            {
                string customerJson = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(customerJson, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(baseUrl + "access/ForgotPassword", content);
                    
                    if(responseMessage.IsSuccessStatusCode)
                    {
                        output = await responseMessage.Content.ReadAsStringAsync();
                    } 
                    else 
                    {
                        throw new Exception("Não foi possível recuperar a senha para este CNPJ.");
                    }


                } catch(Exception ex)
                {
                    throw ex;
                }
            }

            return output;
        }
    }
}
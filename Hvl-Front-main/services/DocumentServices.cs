using System;
using System.Threading.Tasks;
using hvlFront.Models;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace hvlFront.services
{
    public class DocumentServices
    {
        private readonly string baseUrl;
        public DocumentServices()
        {
            baseUrl = Settings.baseUrl;
        }

        public async Task<List<DocumentModel>> GetDocuments(long IdCliente, string token)
        {
            List<DocumentModel> documents = new List<DocumentModel>();

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    try
                    {
                        string response = await client.GetStringAsync(baseUrl + "documents/GetDocumentsByClient/" + IdCliente);
                        documents = JsonConvert.DeserializeObject<List<DocumentModel>>(response);
                    } catch(Exception ex)
                    {
                        throw ex;
                    }
            }

            return documents;
        }


        public async Task<DocumentModel> GetSingleDocuments(long IdDocument, string token)
        {
            DocumentModel document = new DocumentModel();

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    try
                    {
                        string response = await client.GetStringAsync(baseUrl + "documents/GetSingleDocument/" + IdDocument);
                        document = JsonConvert.DeserializeObject<DocumentModel>(response);
                    } catch(Exception ex)
                    {
                        throw ex;
                    }
            }

            return document;
        }

        public async Task<DocumentModel> GetSingleDocumentById(long documentId, string token)
        {
            DocumentModel document = new DocumentModel();

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    try
                    {
                        string response = await client.GetStringAsync(baseUrl + "documents/GetSingleDocument/" + documentId);
                        document = JsonConvert.DeserializeObject<DocumentModel>(response);
                    } catch(Exception ex)
                    {
                        throw ex;
                    }
            }

            return document;
        }

        public async Task<byte[]> GetDocumentBytes(long IdDocument, string token)
        {
            byte[] documentBytes = null;

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    try
                    {
                        string response = await client.GetStringAsync(baseUrl + "documents/GetDocument/" + IdDocument);
                        documentBytes = JsonConvert.DeserializeObject<byte[]>(response);
                    } catch(Exception ex)
                    {
                        throw ex;
                    }
            }

            return documentBytes;
        }

        public async Task<bool> AddDocument(DocumentModel document, string token)
        {
            bool complete = true;

            string jsonDocument = JsonConvert.SerializeObject(document);
            StringContent content = new StringContent(jsonDocument, Encoding.UTF8, "application/json");

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                try 
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(baseUrl + "documents/AddDocumento", content);
                    string output = await responseMessage.Content.ReadAsStringAsync();
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

        public async Task<bool> DeleteDocument(long IdDocument, string token)
        {
            bool complete = true;

            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    try
                    {
                        string response = await client.GetStringAsync(baseUrl + "documents/DeletarDocumento/" + IdDocument);
                        if(!response.Contains("Documento removido com sucesso."))
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
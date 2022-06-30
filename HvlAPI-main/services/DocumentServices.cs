using System;
using MySqlConnector;
using hvl.models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace hvl.services
{
    public class DocumentServices
    {          
        public List<DocumentModel> GetDocuments(long IdCliente)
        {
            List<DocumentModel> documents = new List<DocumentModel>();

            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_SELECIONA_DADOS_DOCUMENTO_POR_ID_CLIENTE", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    cmd.Parameters.AddWithValue("@IdCliente", IdCliente);
                    
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    documents = (
                                    from r in dt.AsEnumerable()
                                    select new DocumentModel(){
                                        IdDocumento = r.Field<long>("ID_DOCUMENTO"),
                                        idCategoria = r.Field<int>("ID_CATEGORIA"),
                                        DsDocumento = r.Field<string>("DS_DOCUMENTO"),
                                        DsCategoria = r.Field<string>("DS_CATEGORIA"),
                                        DtCarga = r.Field<DateTime>("DT_CARGA"),
                                        DtValidade = r.Field<DateTime>("DT_VALIDADE"),
                                        IdCliente = r.Field<long>("ID_CLIENTE")
                                    }
                                ).ToList();

                } catch(Exception ex)
                {
                    throw ex;
                }
                finally 
                {
                    try
                    {
                        connection.Close();
                    } catch(Exception){}
                }

            }

            return documents;
        }

        public DocumentModel GetSigleDocument(long IdDocumento)
        {
            DocumentModel document = new DocumentModel();

            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_SELECIONA_DADOS_DOCUMENTO_POR_ID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    cmd.Parameters.AddWithValue("@IdDocumento", IdDocumento);
                    
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    document = (
                                    from r in dt.AsEnumerable()
                                    select new DocumentModel(){
                                        //IdDocumento = r.Field<long>("ID_DOCUMENTO"),
                                        idCategoria = r.Field<int>("ID_CATEGORIA"),
                                        DsDocumento = r.Field<string>("DS_DOCUMENTO"),
                                        DsCategoria = r.Field<string>("DS_CATEGORIA"),
                                        DtCarga = r.Field<DateTime>("DT_CARGA"),
                                        DsExtensao = r.Field<string>("DS_EXTENSAO"),
                                        DtValidade = r.Field<DateTime>("DT_VALIDADE"),
                                        IdCliente = r.Field<long>("ID_CLIENTE")
                                    }
                                ).FirstOrDefault();

                } catch(Exception ex)
                {
                    throw ex;
                }
                finally 
                {
                    try
                    {
                        connection.Close();
                    } catch(Exception){}
                }

            }

            return document;
        }

        public DocumentModel GetSigleDocumentClientId(long IdCliente)
        {
            DocumentModel document = new DocumentModel();

            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_SELECIONA_DADOS_DOCUMENTO_POR_ID_CLIENTE", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    cmd.Parameters.AddWithValue("@IdCliente", IdCliente);
                    
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    document = (
                                    from r in dt.AsEnumerable()
                                    select new DocumentModel(){
                                        IdDocumento = r.Field<long>("ID_DOCUMENTO"),
                                        idCategoria = r.Field<int>("ID_CATEGORIA"),
                                        DsDocumento = r.Field<string>("DS_DOCUMENTO"),
                                        DsExtensao = r.Field<string>("DS_EXTENSAO"),
                                        DsCategoria = r.Field<string>("DS_CATEGORIA"),
                                        DtCarga = r.Field<DateTime>("DT_CARGA"),
                                        DtValidade = r.Field<DateTime>("DT_VALIDADE"),
                                        IdCliente = r.Field<long>("ID_CLIENTE")
                                    }
                                ).FirstOrDefault();

                } catch(Exception ex)
                {
                    throw ex;
                }
                finally 
                {
                    try
                    {
                        connection.Close();
                    } catch(Exception){}
                }

            }

            return document;
        }


        public async Task<byte[]> GetDocument(long IdDocumento)
        {
            byte[] documentBytes = null;

            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_SELECIONA_BLOB_DOCUMENTO_POR_ID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try 
                {
                    cmd.Parameters.AddWithValue("@IdDocumento", IdDocumento);
                    
                    connection.Open();
                    MySqlDataReader reader = await cmd.ExecuteReaderAsync();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    documentBytes = (from r in dt.AsEnumerable()
                                select r.Field<byte[]>("LG_DOCUMENTO")).FirstOrDefault();

                } catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    try 
                    {
                        connection.Close();
                    } catch(Exception){}
                }
            }

            return documentBytes;
        }


        public void AdicionaDocumento(DocumentModel document, long nmCPF)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_INSERE_DOCUMENTOS", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try 
                {
                    cmd.Parameters.AddWithValue("@IdCategoria", document.idCategoria);
                    cmd.Parameters.AddWithValue("@DsDocumento", document.DsDocumento);
                    cmd.Parameters.AddWithValue("@DsExtensao", document.DsExtensao);
                    cmd.Parameters.AddWithValue("@nmCPF", nmCPF);
                    cmd.Parameters.AddWithValue("@DtValidade", document.DtValidade);
                    cmd.Parameters.AddWithValue("@IdCliente", document.IdCliente);
                    cmd.Parameters.AddWithValue("@LgDocumento", document.LgDocumento);
                    
                    connection.Open();
                    cmd.ExecuteNonQuery();

                } catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    try 
                    {
                        connection.Close();
                    } catch(Exception){}
                }
            }
        }


        public void ExcluiDocumento(long IdDocumento)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_EXCLUI_DOCUMENTOS", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try 
                {
                    cmd.Parameters.AddWithValue("@IdDocumento", IdDocumento);
                    
                    connection.Open();
                    cmd.ExecuteNonQuery();

                } catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    try 
                    {
                        connection.Close();
                    } catch(Exception){}
                }
            }
        }


    }
}
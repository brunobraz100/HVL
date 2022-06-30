using hvl.models;
using MySqlConnector;
using System;
using System.Linq;
using System.Data;


namespace hvl.services
{
    public class AccessServices
    {
        public long AdminLogin(AdminLoginModel adminLogin)
        {
            long output = 0;

            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_SELECIONA_ADM_POR_LOGIN", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@nmCpf", adminLogin.nmCpf);
                cmd.Parameters.AddWithValue("@dsSenha", adminLogin.dsSenha);

                try 
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    output = (from r in dt.AsEnumerable()
                        select r.Field<long>(0)).FirstOrDefault();

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
            
            return output;
        }


        public long CustomerLogin(CustomerLoginModel customerLogin)
        {
            long output = 0;

            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_SELECIONA_CLIENTE_POR_LOGIN", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@nmCnpj", customerLogin.nmCnpj);
                cmd.Parameters.AddWithValue("@dsSenha", customerLogin.dsSenha);

                try 
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    output = (from r in dt.AsEnumerable()
                        select r.Field<long>(0)).FirstOrDefault();
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
            
            return output;
        }

        public string ForgotPassword(CustomerLoginModel customer)
        {
            string email = "";

            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_SELECIONA_EMAIL_CLIENTE_POR_CNPJ", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("nmCNPJ", customer.nmCnpj);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    
                    email = (from r in dt.AsEnumerable()
                            select r.Field<string>(0)).FirstOrDefault();

                } catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    try
                    {
                        connection.Close();
                    }catch(Exception)
                    {}
                }
            }

            return email;
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using hvl.models;
using MySqlConnector;

namespace hvl.services
{
    public class CustomerServices
    {
        public List<CustomerModel> GetAllCustomers()
        {
            List<CustomerModel> customers = new List<CustomerModel>();

            using (MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_SELECIONA_CLIENTES", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                try
                {
                    connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    customers = (from r in dt.AsEnumerable()
                                 select new CustomerModel()
                                 {
                                     IdCliente = r.Field<long>("ID_CLIENTE"),
                                     NmCnpj = r.Field<long>("NM_CNPJ"),
                                     DsRasaoSocial = r.Field<string>("DS_RAZAO_SOCIAL"),
                                     DsEmail = r.Field<string>("DS_EMAIL")
                                 }).ToList();
                } catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    try
                    {
                        connection.Close();
                    }
                    catch (Exception) { }
                }

            }

            return customers;
        }

        public string AddCustomer(CustomerModel customer)
        {
            string message = "";

            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_INSERE_CLIENTES", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@NmCnpj", customer.NmCnpj);
                cmd.Parameters.AddWithValue("@DsSenha", customer.DsSenha);
                cmd.Parameters.AddWithValue("@DsRazaoSocial",customer.DsRasaoSocial);
                cmd.Parameters.AddWithValue("@DsEmail", customer.DsEmail);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    message = (from r in dt.AsEnumerable()
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
                    }
                    catch (Exception) { }
                }
            }

            return message;
        }

        public string UpdateCustomer(CustomerModel customer)
        {
            string message = "";

            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_ATUALIZA_CLIENTES", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@IdCliente", customer.IdCliente);

                if(customer.NmCnpj > 0)
                {
                    cmd.Parameters.AddWithValue("@NmCnpj", customer.NmCnpj);
                }

                if (!string.IsNullOrWhiteSpace(customer.DsEmail))
                {
                    cmd.Parameters.AddWithValue("@DsEmail", customer.DsEmail);
                }

                if (!string.IsNullOrWhiteSpace(customer.DsRasaoSocial))
                {
                    cmd.Parameters.AddWithValue("@DsRazaoSocial", customer.DsRasaoSocial);
                }

                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    message = (from r in dt.AsEnumerable()
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
                    }
                    catch (Exception) { }
                }

            }

            return message;
        }

        public void ChangePasswordCustomer(CustomerModel customer)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_ATUALIZA_SENHA_CLIENTES", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@IdCliente", customer.IdCliente);
                cmd.Parameters.AddWithValue("@DsSenha", customer.DsSenha);

                try
                {
                    connection.Open();
                    cmd.ExecuteReader();
                    
                } catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    try
                    {
                        connection.Close();
                    }
                    catch (Exception) { }
                }
            }

        }

        public void DeleteCustomer(int IdCliente)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_EXCLUI_CLIENTES", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@IdCliente", IdCliente);

                try
                {
                    connection.Open();
                    cmd.ExecuteReader();
            
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    try
                    {
                        connection.Close();
                    }
                    catch (Exception) { }
                }

            }
        }

    }
}

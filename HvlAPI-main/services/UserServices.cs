using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using hvl.models;
using MySqlConnector;

namespace hvl.services
{
    public class UserServices
    {


        public async Task<List<UserModel>> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();

            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("PR_SELECIONA_ADMS", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    users = (from r in dt.AsEnumerable()
                             select new UserModel()
                             {
                                 nmCPF = r.Field<long>("NM_CPF"),
                                 //DsSenha = r.Field<string>("DS_SENHA")
                             }).ToList();

                    connection.Close();

                } catch(Exception ex)
                {
                    throw ex;
                }
            }

            return users;
        }

        public async Task<int> AddUserAdmin(UserModel user)
        {
            int output = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("PR_INSERE_ADMS", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@nmCPF", user.nmCPF);
                    cmd.Parameters.AddWithValue("@DsSenha", user.DsSenha);

                    connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();

                    dt.Load(reader);

                    output = (from r in dt.AsEnumerable()
                              select r.Field<int>(0)).FirstOrDefault();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return output;
        }

        public void UpdateUserAdmin(UserModel user)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_ATUALIZA_ADMS", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if(user.nmCPF > 0)
                {
                    cmd.Parameters.AddWithValue("@nmCPF", user.nmCPF);
                }

                if(!string.IsNullOrWhiteSpace(user.DsSenha))
                {
                    cmd.Parameters.AddWithValue("@DsSenha", user.DsSenha);
                }

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

        public void DeleteUserAdmin(long nmCPF)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_EXCLUI_ADMS", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@nmCPF", nmCPF);

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

    }
}

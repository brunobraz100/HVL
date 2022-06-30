using System;
using System.Collections.Generic;
using System.Data;
using hvl.models;
using MySqlConnector;
using System.Linq;

namespace hvl.services
{
    public class CategoryServices
    {
        
        public List<CategoryModel> GetAllCategories()
        {
            List<CategoryModel> categories = new List<CategoryModel>();

            using(MySqlConnection connnection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_SELECIONA_CATEGORIAS", connnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                try 
                {
                    connnection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    categories = (
                        from r in dt.AsEnumerable()
                        select new CategoryModel()
                        {
                            IdCategoria = r.Field<int>("ID_CATEGORIA"),
                            DsCategoria = r.Field<string>("DS_CATEGORIA")
                        }
                    ).ToList();

                } catch(Exception ex)
                {
                    throw ex;
                } finally
                {
                    try 
                    {
                        connnection.Open();

                    } catch(Exception){}
                }
            }

            return categories;
        }


        public void AddCategory(CategoryModel category)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_INSERE_CATEGORIAS", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@DsCategoria", category.DsCategoria);

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
                    } catch(Exception){}
                }
            }
        }

        public void UpdateCategory(CategoryModel category)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_ATUALIZA_CATEGORIAS", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@idCategoria", category.IdCategoria);

                if(!string.IsNullOrWhiteSpace(category.DsCategoria))
                {
                    cmd.Parameters.AddWithValue("@DsCategoria", category.DsCategoria);
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
                    } catch(Exception){}
                }
            }
        }

        public void DeleteCategory(int idCategory)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_EXCLUI_CATEGORIA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@idCategoria", idCategory);

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
                    } catch(Exception){}
                }
            }
        }

    }
}
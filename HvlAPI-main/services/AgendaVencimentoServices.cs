using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using hvl.models;
using MySqlConnector;

namespace hvl.services
{
    public class AgendaVencimentoServices
    {

        public List<AgendaVencimentoModel> GetVencimentos()
        {
            List<AgendaVencimentoModel> agendaVencimentos = new List<AgendaVencimentoModel>();

            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_SELECIONA_VENCIMENTOS", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt =new DataTable();
                    dt.Load(reader);

                    agendaVencimentos = (from r in dt.AsEnumerable()
                        select new AgendaVencimentoModel(){
                            DsCliente = r.Field<string>("DS_CLIENTE"),
                            DsEmail = r.Field<string>("DS_EMAIL"),
                            DsPlaca = r.Field<string>("DS_PLACA"),
                            DsUF = r.Field<string>("DS_UF"),
                            DsAET = r.Field<string>("DS_AET"),
                            DtVencimento = r.Field<DateTime>("DT_VENCIMENTO")
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
                    } catch(Exception){}
                }
            }

            return agendaVencimentos;
        }

        public void AddAgendaVencimento(AgendaVencimentoModel agendaVencimento)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_INSERE_VENCIMENTOS",connection: connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@DsCliente",agendaVencimento.DsCliente);
                cmd.Parameters.AddWithValue("@NmCnpj",agendaVencimento.NmCnpj);
                cmd.Parameters.AddWithValue("@DsPlaca",agendaVencimento.DsPlaca);
                cmd.Parameters.AddWithValue("@DsUF",agendaVencimento.DsUF);
                cmd.Parameters.AddWithValue("@DsAET",agendaVencimento.DsAET);
                cmd.Parameters.AddWithValue("@DtVencimento",agendaVencimento.DtVencimento);

                try
                {
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
                        connection.Clone();
                    } catch(Exception){}
                }
            }
        }

        public void RemoveAgendaVencimento(AgendaVencimentoModel agendaVencimento)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString: Settings.Connection))
            {
                MySqlCommand cmd = new MySqlCommand("PR_EXCLUI_VENCIMENTOS",connection: connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@DsCliente",agendaVencimento.DsCliente);
                cmd.Parameters.AddWithValue("@NmCnpj",agendaVencimento.NmCnpj);
                cmd.Parameters.AddWithValue("@DsPlaca",agendaVencimento.DsPlaca);
                cmd.Parameters.AddWithValue("@DsUF",agendaVencimento.DsUF);
                cmd.Parameters.AddWithValue("@DsAET",agendaVencimento.DsAET);
                cmd.Parameters.AddWithValue("@DtVencimento",agendaVencimento.DtVencimento);

                try
                {
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
                        connection.Clone();
                    } catch(Exception){}
                }
            }
        }

    }
}
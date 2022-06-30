using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using hvlMailer.Models;
using hvlMailer.Services;

namespace hvlMailer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Pegando lista de clientes com vencimentos agendados!");

            try
            {
                List<AgendaVencimentoModel> vencimentos = new List<AgendaVencimentoModel>();
                vencimentos = await new AgendaVencimentoServices().ListaVencimentos();
                int enviados = 0;
                if(vencimentos.Count > 0)
                {
                    foreach(AgendaVencimentoModel vencimento in vencimentos)
                    {
                        bool sended = await new EmailServices().sendMail(vencimento: vencimento);
                        if(sended == false)
                        {
                            throw new Exception($"E-mail não pode ser disparado para ${vencimento.DsCliente}.");
                        }
                        else 
                        {
                            enviados++;
                            Console.WriteLine($"E-mail enviado para: ${vencimento.DsCliente}");            
                        }

                    }
                } else 
                {
                    Console.WriteLine("Nenhum vencimento agendado!");    
                }
                await new EmailServices().sendReport(enviados);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

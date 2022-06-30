using System;

namespace hvl.models
{
    public class AgendaVencimentoModel
    {
        public string DsCliente { get; set; }
        public long NmCnpj { get; set; }
        public string DsPlaca { get; set; }
        public string DsUF { get; set; }
        public string DsAET { get; set; }
        public DateTime DtVencimento { get; set; }
        public string DsEmail { get; set; }
    }
}
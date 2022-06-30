using System;
using System.IO;

namespace hvlFront.Models
{
    public class DocumentModel
    {
        public long IdDocumento { get; set; }

        public int idCategoria { get; set; }

        public string DsDocumento { get; set; }

        public string DsCategoria { get; set; }
        
        public string DsExtensao { get; set; }

        public DateTime? DtCarga { get; set; }
        
        public DateTime DtValidade { get; set; }

        public long IdCliente { get; set; }

        public byte[]? LgDocumento { get; set; }

        public string vencimento { get; set; }

    }
}
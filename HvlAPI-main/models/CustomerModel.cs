using System;
namespace hvl.models
{
    public class CustomerModel
    {
        public long IdCliente { get; set; }

        public long NmCnpj { get; set; }

        public string DsSenha { get; set; }

        public string DsRasaoSocial { get; set; }

        public string DsEmail { get; set; }
    }
}

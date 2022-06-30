namespace hvlFront.Models
{
    public class LoginModel
    {
        public bool changePassword { get; set; }

        public string token { get; set; }

        public long? nmCpf { get; set; }

        public long? nmCnpj { get; set; }
    }
}
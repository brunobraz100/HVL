
using System.Threading.Tasks;

namespace hvl.services.interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendMail(string to, string subject, string message);
    }
}
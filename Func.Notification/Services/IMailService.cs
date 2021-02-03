using Func.Notification.Models;
using System.Threading.Tasks;

namespace Func.Notification.Services
{
    public interface IMailService
    {
        Task<string> SendMail(MailEventModel model, string acao);
    }
}

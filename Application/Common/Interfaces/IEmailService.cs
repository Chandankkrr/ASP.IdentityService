using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        public Task<string> Send(IMessage message);
    }
}
using System.Threading.Tasks;

namespace Skytecs.UniSenderApiClient
{
    public interface IUniOneClient
    {
        Task<SendResult> Send(SendRequest request);
    }
}
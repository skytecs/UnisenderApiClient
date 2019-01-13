using System.Threading.Tasks;

namespace Skytecs.UniSenderApiClient
{
    public interface IUniOneClient
    {
        Task<UniOneSendResult> Send(UniOneSendRequest request);
    }
}
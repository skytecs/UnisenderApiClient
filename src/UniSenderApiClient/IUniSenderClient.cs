using System.Threading;
using System.Threading.Tasks;

namespace UniSenderApiClient
{
    public interface IUniSenderClient
    {
        Task<UniSenderResult> SendAsync(UniSenderRequest request, CancellationToken cancellationToken);
    }
}
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniSenderApiClient
{
    public static class Extensions
    {
        public static IServiceCollection AddUniOneClient(this IServiceCollection services, Action<UniSenderApiOptions> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }


            var options = new UniSenderApiOptions();

            configure(options);

            return services.AddSingleton(options).AddSingleton<IUniSenderClient, UniSenderClient>();
        }
    }
}

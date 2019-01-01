using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skytecs.UniSenderApiClient
{
    public static class UniOneClientExtensions
    {
        public static IServiceCollection AddUniOneClient(this IServiceCollection services, Action<UniOneOptions> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }


            var options = new UniOneOptions();

            configure(options);

            return services.AddSingleton(options).AddSingleton<IUniOneClient, UniOneClient>();
        }
    }
}

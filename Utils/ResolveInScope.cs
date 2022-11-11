using BetterHostedServices;
using Microsoft.Extensions.DependencyInjection;

namespace Utils
{
    public class ResolveInScope<T> : NotEndingBackgroundService
    where T : CriticalBackgroundService
    {
        private IServiceProvider provider;

        public ResolveInScope(IServiceProvider provider)
        {
            this.provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            using IServiceScope scope = provider.CreateScope();

            T worker = ActivatorUtilities.CreateInstance<T>(provider);

            await worker.StartAsync(token);
        }
    }
}
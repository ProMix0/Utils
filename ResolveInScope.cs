using BetterHostedServices;
using Microsoft.Extensions.DependencyInjection;

namespace Utils
{
    /// <summary>
    /// The class will resolve underlying <see cref="CriticalBackgroundService"/> in scope
    /// </summary>
    /// <typeparam name="T">Wrapped <see cref="CriticalBackgroundService"/></typeparam>
    public class ResolveInScope<T> : NotEndingBackgroundService
    where T : CriticalBackgroundService
    {
        private readonly IServiceProvider provider;

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
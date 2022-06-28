
using Service.Services;

namespace Service.HostedService
{
    public class FetchDatabaseTimedHostedService : BackgroundService
    {
        public IServiceProvider Services { get; }

        public FetchDatabaseTimedHostedService(IServiceProvider services)
        {
            Services = services;

        }
        protected override async Task ExecuteAsync(CancellationToken token)
        {

            await FetchData(token);
        }

        private async Task FetchData(CancellationToken token)
        {
            using var scope = Services.CreateScope();
            var scopedProcessingService =
                scope.ServiceProvider
                    .GetRequiredService<IUserService>();
            while (!token.IsCancellationRequested)
            {
                scopedProcessingService.FetchData(token);
                await Task.Delay(600000, token);
            }
        }
    }
}

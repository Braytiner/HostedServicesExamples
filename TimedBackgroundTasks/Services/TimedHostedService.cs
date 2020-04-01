using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TimedBackgroundTasks.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer;

        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Serviço TimedHosted iniciado!");
            _timer = new Timer(FacaOTrabalho, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void FacaOTrabalho(object state)
        {
            _logger.LogInformation(DateTime.Now.ToString());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Serviço TimedHosted finalizado!");
            _timer?.Change(Timeout.Infinite, 0);
            
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TT.Deliveries.Data.Repositories;

namespace TT.Deliveries.Web.Api.Services
{
    public class DeliveryExpirationService : IHostedService, IDisposable
    {
        private Timer _expirationTimer;
        private readonly DeliveryRepository _deliveryRepository;

        public DeliveryExpirationService(DeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //expiration service will run every 5 minutes, this can change based on business need 
            _expirationTimer = new Timer(ExpireDeliveries, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop the timer when the application is shutting down
            _expirationTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void ExpireDeliveries(object state)
        {
            _deliveryRepository.ExpireNotCompletedDeliveries();
        }

        public void Dispose()
        {
            _expirationTimer?.Dispose();
        }
    }

}

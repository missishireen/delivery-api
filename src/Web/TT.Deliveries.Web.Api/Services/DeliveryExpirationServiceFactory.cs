using Microsoft.Extensions.DependencyInjection;
using System;
using TT.Deliveries.Data.Repositories;

namespace TT.Deliveries.Web.Api.Services
{

    public static class DeliveryExpirationServiceFactory
    {
        public static DeliveryExpirationService Create(IServiceProvider serviceProvider)
        {
            var scopedProvider = serviceProvider.CreateScope().ServiceProvider;
            var deliveryRepository = scopedProvider.GetRequiredService<DeliveryRepository>();

            return new DeliveryExpirationService(deliveryRepository);
        }
    }
}

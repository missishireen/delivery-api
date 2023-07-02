using System.Collections.Generic;
using System;
using TT.Deliveries.Data.Models;
using System.Linq;
using TT.Deliveries.Data.Enums;

namespace TT.Deliveries.Data.Repositories
{
    public class DeliveryRepository
    {
        private readonly DeliveryContext _context;

        public DeliveryRepository(DeliveryContext context)
        {
            _context = context;
        }

        public IEnumerable<Delivery> GetAllDeliveries()
        {
            return _context.Deliveries.ToList();
        }

        public Delivery GetDeliveryById(Guid id)
        {
            return _context.Deliveries.FirstOrDefault(d => d.Id == id);
        }

        public void AddDelivery(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            _context.SaveChanges();
        }

        public Delivery UpdateDelivery(Delivery delivery)
        {
            var existingDelivery = _context.Deliveries.FirstOrDefault(d => d.Id == delivery.Id);
            if (existingDelivery != null)
            {
                existingDelivery.State = delivery.State;
                existingDelivery.AccessWindow = delivery.AccessWindow;
                existingDelivery.Recipient = delivery.Recipient;

                _context.SaveChanges();
            }
            return existingDelivery;
        }

        public Delivery DeleteDelivery(Guid id)
        {
            var delivery = _context.Deliveries.FirstOrDefault(d => d.Id == id);
            if (delivery != null)
            {
                _context.Deliveries.Remove(delivery);
                _context.SaveChanges();
            }
            return delivery;
        }

        public Delivery ApproveDelivery(Guid id)
        {
            var existingDelivery = _context.Deliveries.FirstOrDefault(d => d.Id == id);
            if (existingDelivery != null)
            {
                if (existingDelivery.AccessWindow.StartTime < DateTime.Now)
                {
                    existingDelivery.State = DeliveryState.APPROVED;
                    _context.SaveChanges();
                }
            }
            return existingDelivery;
        }

        public Delivery CompleteDelivery(Guid id)
        {
            var existingDelivery = _context.Deliveries.FirstOrDefault(d => d.Id == id);
            if (existingDelivery != null)
            {
                if (existingDelivery.State == DeliveryState.APPROVED)
                {
                    existingDelivery.State = DeliveryState.COMPLETED;
                    _context.SaveChanges();
                }
            }
            return existingDelivery;
        }
        public void ExpireNotCompletedDeliveries()
        {
            var toExpireDeliveries = _context.Deliveries.Where(d => d.State != DeliveryState.COMPLETED && d.AccessWindow.EndTime < DateTime.Now).ToList();
            foreach (var delivery in toExpireDeliveries)
            {
                delivery.State = DeliveryState.EXPIRED;
            }
            _context.SaveChanges();
        }

        public object CancelDelivery(Guid id)
        {
            var existingDelivery = _context.Deliveries.FirstOrDefault(d => d.Id == id);
            if (existingDelivery != null)
            {
                if (existingDelivery.State == DeliveryState.CREATED || existingDelivery.State == DeliveryState.APPROVED)
                {
                    existingDelivery.State = DeliveryState.CANCELLED;
                    _context.SaveChanges();
                }
            }
            return existingDelivery;
        }
    }
}
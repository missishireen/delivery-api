using System;
using System.Collections.Generic;
using System.Text;
using TT.Deliveries.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace TT.Deliveries.Data
{
    public class DeliveryContext : DbContext
    {
        public DeliveryContext(DbContextOptions<DeliveryContext> options) : base(options)
        {
        }

        public DbSet<Delivery> Deliveries { get; set; }
    }
}

using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    /// <summary>
    /// Following will add to database if there are no records in "Order" table
    /// </summary>
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() { 
                    UserName = "swn", 
                    FirstName = "Danish", 
                    LastName = "Bilal",
                    EmailAddress = "dani.stranger@gmail.com", 
                    AddressLine = "Bahcelievler", 
                    Country = "Pakistan", 
                    State = "Punjab",
                    ZipCode = "54000",
                    TotalPrice = 350, 
                    CVV = "OK", 
                    CardName = "OK", 
                    CardNumber = "123456",
                    PaymentMethod = 1,
                    Expiration = "10=10-2024",
                    CreatedBy = "Danish",
                    CreatedDate = DateTime.Now.Date,
                    LastModifiedBy = "Danish",
                    LastModifiedDate = DateTime.Now.Date
                }
            };
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KolokwiumCF.Models;

namespace KolokwiumDF.Services
{
    public class PaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> AddClientPayment(int idClient, int idSubscription, Payment paymentDto)
        {
            var client = await _context.Clients.FindAsync(idClient);
            if (client == null)
            {
                return new NotFoundObjectResult($"Client with Id {idClient} not found.");
            }

            var subscription = await _context.Subscriptions
                .Include(s => s.Client)
                .FirstOrDefaultAsync(s => s.Id == idSubscription && s.Client.Id == idClient);
            if (subscription == null)
            {
                return new NotFoundObjectResult($"Subscription with Id {idSubscription} for Client with Id {idClient} not found.");
            }

            var payment = new Payment
            {
                Amount = paymentDto.Amount,
                Date = paymentDto.Date,
                IdSubscriptionNav = idSubscription
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return new OkObjectResult("Payment added successfully.");
        }
    }
}
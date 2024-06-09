using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using KolokwiumCF.Models;

namespace KolokwiumDF.Controllers;
 
[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
 
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClientSubscriptions(int id)
    {
        var client = await _context.Client
            .Include(c => c.Discount)
            .Include(c => c.Subscriptions)
            .ThenInclude(s => s.Payments)
            .FirstOrDefaultAsync(c => c.Id == id);
 
        if (client == null)
        {
            return NotFound();
        }
 
        var newClient = new Client
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            Phone = client.Phone,
            Discount = client.Discount?.Value,
            Subscriptions = client.Subscriptions.Select(s => new Subscription
            {
                IdSubscription = s.Id,
                Name = s.Name,
                RenewalPeriod = s.RenewalPeriod,
                TotalPaidAmount = s.Payments.Sum(p => p.Amount)
            }).ToList()
        };
 
        return newClient;
    }
}
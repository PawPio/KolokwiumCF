namespace KolokwiumCF.Models;

public class Sale
{
    public int IdSale { get; set; }
    public DateOnly CreatedAt { get; set; }
    
    public virtual Client IdClientNav { get; set; }
    public virtual Subscription IdSubscription { get; set; }
    
    
}
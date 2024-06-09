namespace KolokwiumCF.Models;

public class Payment
{
    public int IdPayment { get; set; }
    public DateOnly Date { get; set; }
    public double Amount { get; set; }
    
    public virtual Client IdClientNav { get; set; }
    public virtual Subscription IdSubscriptionNav { get; set; }
    
    
}
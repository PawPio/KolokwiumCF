namespace KolokwiumCF.Models;

public class Client
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Discount { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; }
}

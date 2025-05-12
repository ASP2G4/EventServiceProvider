using System.ComponentModel.DataAnnotations.Schema;

namespace EventServiceProvider.Business.Models;

public class AddEventFormData
{
    public string EventName { get; set; } = null!;
    public string EventDescription { get; set; } = null!;
    public string EventLocation { get; set; } = null!;

    public DateTime EventDate { get; set; }
    public int TicketAmount { get; set; }
    public int TicketPrice { get; set; }
}

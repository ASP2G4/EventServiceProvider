using System.ComponentModel.DataAnnotations.Schema;

namespace EventServiceProvider.Business.Models;

public class AddEventFormData
{
    public string EventName { get; set; } = null!;
    public string EventDescription { get; set; } = null!;
    public string EventLocation { get; set; } = null!;


    public DateTime EventDate { get; set; }

    public int SilverTicketAmount { get; set; }
    public double SilverTicketPrice { get; set; }
    public int GoldTicketAmount { get; set; }
    public double GoldTicketPrice { get; set; }
}

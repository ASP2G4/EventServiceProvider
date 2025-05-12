using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventServiceProvider.Data.Entities;

public class EventEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string EventName { get; set; } = null!;
    public string EventDescription { get; set; } = null!;
    public string EventLocation { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime EventDate { get; set; }
}

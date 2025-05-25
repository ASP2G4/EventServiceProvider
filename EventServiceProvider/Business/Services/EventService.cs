using EventServiceProvider.Business.Models;
using EventServiceProvider.Data.Entities;
using EventServiceProvider.Data.Repositories;

namespace EventServiceProvider.Business.Services;

public interface IEventService
{
    Task<bool> CreateEventAsync(AddEventFormData eventFormData);
    Task<IEnumerable<Event>> GetAllEventsAsync();
    Task<Event?> GetEventByIdAsync(string eventId);
}

public class EventService(IEventRepository eventRepository, TicketContract.TicketContractClient ticketContractClient) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;
    private readonly TicketContract.TicketContractClient _ticketContractClient = ticketContractClient;

    public async Task<bool> CreateEventAsync(AddEventFormData eventFormData)
    {
        if (eventFormData == null)
            return false;

        var entity = new EventEntity
        {
            EventName = eventFormData.EventName,
            EventDescription = eventFormData.EventDescription,
            EventLocation = eventFormData.EventLocation,
            EventDate = eventFormData.EventDate,
        };

        var result = await _eventRepository.AddAsync(entity);

        if (result)
        {
            var request = new TicketRequest
            {
                EventId = entity.Id,
                EventName = eventFormData.EventName,
                SilverTicketAmount = eventFormData.SilverTicketAmount,
                SilverTicketPrice = eventFormData.SilverTicketPrice,
                GoldTicketAmount = eventFormData.GoldTicketAmount,
                GoldTicketPrice = eventFormData.GoldTicketPrice,
            };

            var response = await _ticketContractClient.CreateTicketAsync(request);
        }

        return result;
    }

    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        var entities = await _eventRepository.GetAllAsync();
        var events = entities.Select(entity => new Event
        {
            Id = entity.Id,
            EventName = entity.EventName,
            EventDescription = entity.EventDescription,
            EventLocation = entity.EventLocation,
            EventDate = entity.EventDate,
        });

        return events.OrderBy(entity => entity.EventDate);
    }

    public async Task<Event?> GetEventByIdAsync(string eventId)
    {
        var entity = await _eventRepository.GetAsync(x => x.Id == eventId);
        return entity == null
            ? null
            : new Event
            {
                Id = entity.Id,
                EventName = entity.EventName,
                EventDescription = entity.EventDescription,
                EventLocation = entity.EventLocation,
                EventDate = entity.EventDate,
            };
    }
}

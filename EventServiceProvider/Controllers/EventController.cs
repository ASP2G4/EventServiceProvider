using EventServiceProvider.Business.Models;
using EventServiceProvider.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventServiceProvider.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController(IEventService eventService) : ControllerBase
{
    private readonly IEventService _eventService = eventService;

    [HttpPost]
    public async Task<IActionResult> Create(AddEventFormData formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _eventService.CreateEventAsync(formData);

       return result ? Ok() : Problem();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventService.GetAllEventsAsync();
        return Ok(events);
    }

    [HttpGet("{eventId}")]
    public async Task<IActionResult> Get(string eventId)
    {
        var events = await _eventService.GetEventByIdAsync(eventId);
        return events == null ? NotFound() : Ok(events);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEventFormData formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _eventService.UpdateEventAsync(formData);
        return result ? Ok(result) : NotFound();
    }

    //[HttpDelete("{eventId}")]
    //public async Task<IActionResult> Delete(string eventId)
    //{
    //    if (string.IsNullOrEmpty(eventId))
    //        return BadRequest();

    //    var result = await _eventService.DeleteEventAsync(eventId);
    //    return result ? Ok(result) : NotFound();
    //}
}

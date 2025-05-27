EventServiceProvider

EventServiceProvider is a microservice designed to read, save, and update event data. It exposes a REST API to manage events and interacts with a TicketServiceProvider via gRPC for ticket-related details.
Features

    Create new events with detailed information

    Retrieve existing events

    Update event data

    Communicates with TicketServiceProvider to manage ticket inventory and pricing

API Endpoint
POST /api/event

Create or update an event.

Request Body Example:

{
  "eventName": "Event nummer 3 är ännu bättre.",
  "eventDescription": "Jag hatar Azure",
  "eventLocation": "Stockholm",
  "eventDate": "2024-05-28",
  "silverTicketAmount": 7000,
  "silverTicketPrice": 200,
  "goldTicketAmount": 40,
  "goldTicketPrice": 3000
}

Note:
The ticket-related fields (silverTicketAmount, silverTicketPrice, goldTicketAmount, goldTicketPrice) are required as the service interacts with the TicketServiceProvider via gRPC to manage tickets.
Prerequisites

    .NET runtime (version your service targets)

    Running TicketServiceProvider accessible via gRPC

How to Run Locally

    Clone the repository

    Ensure TicketServiceProvider is running and accessible

    Build and run EventServiceProvider

    Use Postman or similar tools to POST event data to https://localhost:7043/api/event

Communication with TicketServiceProvider

This service communicates with the TicketServiceProvider over gRPC for ticket management. Ensure that the TicketServiceProvider endpoint and credentials (if any) are correctly configured in the application settings.

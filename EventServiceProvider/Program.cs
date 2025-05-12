using EventServiceProvider;
using EventServiceProvider.Business.Services;
using EventServiceProvider.Data.Contexts;
using EventServiceProvider.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddGrpcClient<TicketContract.TicketContractClient>(x =>
{
    x.Address = new Uri(builder.Configuration["TicketServiceProvider"]!);
});

var app = builder.Build();


app.MapOpenApi();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();

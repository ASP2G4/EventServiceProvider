using EventServiceProvider;
using EventServiceProvider.Business.Services;
using EventServiceProvider.Data.Contexts;
using EventServiceProvider.Data.Repositories;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddGrpcClient<TicketContract.TicketContractClient>(x =>
{
    x.Address = new Uri(builder.Configuration["TicketServiceProvider"]!);
});

var app = builder.Build();


app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Service"));
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();

using Domain.Repositories;
using Domain.Services.Costumer;
using Domain.Services.Order;
using Persistence.Extensions;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
      policy =>
      {
          policy.AllowAnyOrigin()
             .AllowAnyHeader()
             .AllowAnyMethod();
      });
});

//adds DbContext
builder.Services.AddPersistenceInfrastructure(builder.Configuration);

//dependency injection containers
builder.Services.AddScoped<ICostumerService, CostumerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICostumerRepository, CostumerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

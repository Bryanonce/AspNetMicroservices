using Basket.API.Services;
using Basket.Repository;
using Discount.gRPC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IBasketRepository, BasketRepository>();
builder.Services.AddTransient<ClientDiscountGrpc>();
var gRPC_Uri = builder.Configuration.GetValue<string>("GrpcService:Url");
builder.Services.AddGrpcClient<Discounter.DiscounterClient>(opt =>
{
    opt.Address = new Uri(gRPC_Uri);
});
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

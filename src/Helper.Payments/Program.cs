using Helper.Payments.Api;
using Helper.Payments.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDb(builder.Configuration);
builder.Services.AddHostedService<PaymentQue>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

;

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
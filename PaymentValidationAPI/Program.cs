using PaymentValidationAPI.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddCreditCardEndpoints();

var app = builder.Build();

app.UseExceptionHandler(appError => {});
app.UseOpenApi();
app.UseHttpsRedirection();

app.MapCreditCardEndpoints();

app.Run();

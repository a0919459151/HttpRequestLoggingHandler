var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFastEndpoints()
    .AddJsonServerClient()
    .AddServices();

var app = builder.Build();

app.UseFastEndpoints();

app.Run();

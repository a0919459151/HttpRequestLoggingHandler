var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFastEndpoints()
    .AddDbContext()
    .AddJsonServerClient()
    .AddServices();

var app = builder.Build();

app.UseFastEndpoints();

app.Run();

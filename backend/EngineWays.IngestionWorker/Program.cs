using EngineWays.Backend.Infrastructure;
using EngineWays.IngestionWorker;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<AppDbContext>("engineways");

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

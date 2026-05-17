var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataBindMount("./data/postgres")
    .AddDatabase("engineways");

builder.AddProject<Projects.EngineWays_Backend>("backend")
    .WithReference(postgres);

builder.AddProject<Projects.EngineWays_IngestionWorker>("ingestion-worker")
    .WithReference(postgres);

builder.Build().Run();

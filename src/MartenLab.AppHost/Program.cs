var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres:15-alpine")
    .WithContainerName("martenlab-postgres");

var postgresDb = postgres.AddDatabase("martenlab");

var api = builder.AddProject<Projects.MartenLab_API>("martenlab-api")
    .WithReference(postgresDb)
    .WithEnvironment("ConnectionStrings__Postgres", postgresDb)
    .WaitFor(postgresDb);

builder.Build().Run();
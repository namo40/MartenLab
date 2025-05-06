var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres:15-alpine")
    .WithContainerName("martenlab-postgres");

var postgresDb = postgres.AddDatabase("martenlab");

var api = builder.AddProject<Projects.MartenLab_API>("martenlab-api")
    .WithReference(postgresDb)
    .WithEnvironment("ConnectionStrings__Postgres", postgresDb)
    .WithEnvironment("Jwt__Secret", $"{Guid.NewGuid().ToString()}{Guid.NewGuid().ToString()}")
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Swagger UI";
        url.Url += "/swagger";
    })
    .WaitFor(postgresDb);

builder.Build().Run();
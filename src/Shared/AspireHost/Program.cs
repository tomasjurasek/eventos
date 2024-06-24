var builder = DistributedApplication.CreateBuilder(args);

var writeModelApi = builder.AddProject<Projects.WriteModel_API>("writeModelApi");

builder.AddProject<Projects.Gateway>("gateway")
    .WithReference(writeModelApi);

builder.Build().Run();

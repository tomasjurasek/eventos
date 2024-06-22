var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.EventPlanning_API>("api");

builder.AddProject<Projects.EventPlanning_Gateway>("gateway")
    .WithReference(api);

builder.Build().Run();

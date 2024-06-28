var builder = DistributedApplication.CreateBuilder(args);

var eventStore = builder.AddPostgresContainer("EventStore");
var writeModelApi = builder.AddProject<Projects.WriteModel_API>("writeModelApi")
    .WithReference(eventStore);


builder.AddProject<Projects.Gateway>("gateway")
    .WithReference(writeModelApi);

builder.Build().Run();

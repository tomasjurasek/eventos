var builder = DistributedApplication.CreateBuilder(args);

var eventStore = builder.AddPostgres("eventstore");
var eventManagement = builder.AddProject<Projects.EventManagement_API>("eventmanagement")
    .WithReference(eventStore);

builder.AddProject<Projects.Gateway>("gateway")
    .WithReference(eventManagement);

builder.Build().Run();

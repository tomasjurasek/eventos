var builder = DistributedApplication.CreateBuilder(args);

var eventBus = builder.AddRabbitMQContainer("eventBus");
//var eventStore = builder.AddPostgresContainer("eventStore");

builder.AddContainer("eventStore", "eventstore/eventstore", "20.10.2-buster-slim")
    .WithServiceBinding(containerPort: 2113, hostPort: 2113, scheme: "http")
    .WithEnvironment("EVENTSTORE_CLUSTER_SIZE", "1")
    .WithEnvironment("EVENTSTORE_RUN_PROJECTIONS", "All")
    .WithEnvironment("EVENTSTORE_START_STANDARD_PROJECTIONS", "true")
    .WithEnvironment("EVENTSTORE_INSECURE", "true");

var writerService = builder.AddProject<Projects.EventPlanning_Writer_API>("writer")
    .WithReference(eventBus);
    

var readerService = builder.AddProject<Projects.EventPlanning_Reader_API>("reader")
    .WithReference(eventBus);


builder.AddProject<Projects.EventPlanning_Gateway>("gateway")
    .WithReference(writerService)
    .WithReference(readerService);


builder.Build().Run();

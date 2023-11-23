var builder = DistributedApplication.CreateBuilder(args);

var eventBus = builder.AddRabbitMQContainer("eventBus");


builder.AddContainer("eventStore", "eventstore/eventstore", "20.10.2-buster-slim")
    .WithServiceBinding(containerPort: 2113, hostPort: 2113, scheme: "http")
    .WithEnvironment("EVENTSTORE_CLUSTER_SIZE", "1")
    .WithEnvironment("EVENTSTORE_RUN_PROJECTIONS", "All")
    .WithEnvironment("EVENTSTORE_START_STANDARD_PROJECTIONS", "true")
    .WithEnvironment("EVENTSTORE_INSECURE", "true");

var eventPlanningService = builder.AddProject<Projects.EventPlanning_API>("eventPlanningService")
    .WithReference(eventBus);
    

var eventPlanningReadModelService = builder.AddProject<Projects.EventPlanning_ReadModel_API>("eventPlanningReadModelService")
    .WithReference(eventBus);


builder.AddProject<Projects.EventPlanning_Gateway>("eventPlanningGateway")
    .WithReference(eventPlanningService)
    .WithReference(eventPlanningReadModelService);


builder.Build().Run();

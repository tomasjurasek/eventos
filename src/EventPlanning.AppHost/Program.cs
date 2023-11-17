var builder = DistributedApplication.CreateBuilder(args);

var eventBus = builder.AddRabbitMQContainer("eventPlanningEventBus");

builder.AddProject<Projects.EventPlanning_API>("eventPlanningService")
    .WithReference(eventBus);

builder.AddProject<Projects.EventPlanning_ReadModel_API>("eventPlanningReadModelService")
    .WithReference(eventBus);


builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

var eventPlanningService = builder.AddProject<Projects.EventPlanning_API>("eventPlanningService");
    

builder.Build().Run();

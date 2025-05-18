var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.ApiDemo>("apidemo");

builder.AddProject<Projects.BlazorClientDemo>("blazorclientdemo")
    .WithReference(api);

builder.AddProject<Projects.BlazorServerDemo>("blazorserverdemo");

builder.AddProject<Projects.BlazorSSRDemo>("blazorssrdemo");

builder.AddProject<Projects.MVCDemo>("mvcdemo");

builder.AddProject<Projects.RPDemo>("rpdemo");

builder.Build().Run();

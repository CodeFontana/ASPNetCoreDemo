var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.ApiDemo>("apidemo");

builder.AddProject<Projects.BlazorClientDemo>("blazorclientdemo")
    .WithEndpoint(
        "https",
        endpoint =>
        {
            endpoint.UriScheme = "https";
            endpoint.Transport = "http";
            endpoint.Port = 5003;
            endpoint.IsProxied = false;
        })
    .WithReference(api)
    .WaitFor(api);

builder.AddProject<Projects.BlazorServerDemo>("blazorserverdemo");

builder.AddProject<Projects.BlazorSSRDemo>("blazorssrdemo");

builder.AddProject<Projects.MVCDemo>("mvcdemo");

builder.AddProject<Projects.RPDemo>("rpdemo");

builder.Build().Run();

using Framework;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.Init();

using var serviceProvider = services.BuildServiceProvider();
await serviceProvider.GetRequiredService<FlowEngine>().RunAsync();


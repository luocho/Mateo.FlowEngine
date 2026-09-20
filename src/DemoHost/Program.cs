using Framework;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.Init();

using var serviceProvider = services.BuildServiceProvider();
serviceProvider.GetRequiredService<FlowEngine>().ExecuteFlow(nameof(A_P_DownLoadForHttp));


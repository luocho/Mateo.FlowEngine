using Framework;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.Init();

using var serviceProvider = services.BuildServiceProvider();

var action = serviceProvider.GetKeyedService<IAction>(nameof(A_P_DownLoadForHttp));
await action.RunAsync();
action = serviceProvider.GetKeyedService<IAction>(nameof(A_P_AnalyseFile));
await action.RunAsync();
action = serviceProvider.GetKeyedService<IAction>(nameof(A_S_SaveFile));
await action.RunAsync();
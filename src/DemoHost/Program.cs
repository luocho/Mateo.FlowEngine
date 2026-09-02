using Framework;
using LibraryA;

AClient client = new AClient();

client.wrapper = new TaskWrapper(client);

ITask task = client;
await task.Run();

Console.ReadLine();
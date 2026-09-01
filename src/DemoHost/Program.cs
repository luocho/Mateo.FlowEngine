using Framework;
using LibraryA;

AClient client = new AClient
{
    wrapper = new TaskWrapper(this),
    inputParameter = ["Water","123"]
};

ITask task = client;
await task.Run();

Console.ReadLine();
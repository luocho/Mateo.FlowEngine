using Framework;
using LibraryA;
using System.Reflection;

var wrapper = new TaskWrapper();

AClient client = new AClient();
var t = client.GetType();
var field = t.GetField("<wrapper>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
if (field != null)
{
    field.SetValue(client, wrapper);
}
BClient clientb = new BClient();
var tb = clientb.GetType();
var fieldb = tb.GetField("<wrapper>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
if (fieldb != null)
{
    fieldb.SetValue(clientb, wrapper);
}

ITask task = client;
ITask taskb = clientb;
await taskb.Run();
await task.Run();
Console.WriteLine(Thread.CurrentThread.Name);

Console.ReadLine();

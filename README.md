# A/B 类库消息通信最小示例

项目关系：

```text
DemoHost
  ├─ LibraryA ─┐
  ├─ LibraryB ─┼─> Framework
  └─ Framework ┘
```

- `LibraryA` 不引用 `LibraryB`，只在消息字段中填写 B 的程序集名、类型名和方法名。
- `Framework` 提供进程内发布/订阅，并根据 `CallMessage` 的字段反射调用 B。
- B 执行完成后，Framework 发布带相同 `Id` 的 `CallResult`，A 据此收到自己的调用结果。
- `DemoHost` 是必要的进程入口，负责让三个类库在同一进程内运行。

运行：

```powershell
dotnet run --project .\src\DemoHost\DemoHost.csproj
```

这是最简单的进程内方案，不适用于 A、B 位于两个独立进程或两台电脑的情况；独立进程需要命名管道、TCP、消息队列等传输方式。

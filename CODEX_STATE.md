# CODEX_STATE

- 当前为 .NET 10 的进程内 A/B 类库消息通信示例。
- `LibraryA` 与 `LibraryB` 仅共同引用 `Framework`，彼此不引用。
- `MessageBus` 提供发布/订阅；`ReflectionDispatcher` 按 `CallMessage` 字段反射调用 B，并发布 `CallResult`。
- `DemoHost` 同时引用三个类库，作为进程入口和演示程序。
- 当前设计仅支持同一进程；跨进程通信需要替换消息传输层。

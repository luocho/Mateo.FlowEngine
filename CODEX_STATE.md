# CODEX_STATE

- 当前为 .NET 10 的进程内 A/B 类库消息通信示例；A、B 共同依赖 `Framework`，彼此不引用。
- `TaskWrapper` 是事件聚合器/中介者，按 `Command`、`Reply`、`Event` 主题路由 `MateoMsg`。
- `IDispatcher` 有 `ReflectionDispatcher` 和 `MessageDispatcher` 两个实现；B 通过 `ISecs` 接口和反射加载。
- 当前 Command 路径只按程序集查找首个 `ISecs` 并调用 `RunAsync`，尚未使用 TypeName/MethodName/Arguments。
- Reply/Event 结构已存在，但 B 的结果尚未发布回 A，请求—响应链未闭环。
- `DESIGN_PATTERNS.md` 记录了当前模式、真实执行流、未完成部分和实现风险。
- 工作区当前另有 `src/LibraryA/TaskWrapper.cs`，与 Framework 中的同名类型发生程序集类型冲突。
- 最新构建失败：`CS0738` 1 个错误、3 个警告；未修改或删除该用户文件。

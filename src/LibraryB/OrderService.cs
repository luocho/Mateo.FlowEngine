using Framework;

namespace LibraryB;

public sealed class OrderService : ISecs
{
    public async Task<object?> RunAsync(object?[] args)
    {
        return await ProcessAsync(args);
    }

    public async Task<object?> ProcessAsync(object?[] args)
    {
        var orderNumber = args[0]?.ToString();
        var quantity = Convert.ToInt32(args[1]);
        await Task.Delay(1000); // 模拟处理时间
        return await Task.FromResult((object?)$"B 已处理订单 {orderNumber}，数量：{quantity}");
    }
}

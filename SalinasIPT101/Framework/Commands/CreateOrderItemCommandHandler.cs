using Domain.Commands;
using Domain.Models;

namespace Framework.Commands;

public class CreateOrderItemCommandHandler : ICommandHandler<CreateOrderItemCommand>
{
    private readonly OrderingDbContext _context;

    public CreateOrderItemCommandHandler(OrderingDbContext context)
    {
        _context = context;
    }

    public async Task ExecuteAsync(CreateOrderItemCommand command)
    {
        var orderItem = new OrderItem
        {
            ProductName = command.ProductName,
            Quantity = command.Quantity,
            Price = command.Price,
            OrderDate = DateTime.Now
        };

        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
    }
}

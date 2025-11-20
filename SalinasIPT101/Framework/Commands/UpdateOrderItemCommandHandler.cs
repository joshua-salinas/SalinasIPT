using Domain.Commands;
using Microsoft.EntityFrameworkCore;

namespace Framework.Commands;

public class UpdateOrderItemCommandHandler : ICommandHandler<UpdateOrderItemCommand>
{
    private readonly OrderingDbContext _context;

    public UpdateOrderItemCommandHandler(OrderingDbContext context)
    {
        _context = context;
    }

    public async Task ExecuteAsync(UpdateOrderItemCommand command)
    {
        var orderItem = await _context.OrderItems.FindAsync(command.Id);
        if (orderItem != null)
        {
            orderItem.ProductName = command.ProductName;
            orderItem.Quantity = command.Quantity;
            orderItem.Price = command.Price;

            await _context.SaveChangesAsync();
        }
    }
}

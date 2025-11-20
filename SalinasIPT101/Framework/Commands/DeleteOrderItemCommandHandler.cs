using Domain.Commands;

namespace Framework.Commands;

public class DeleteOrderItemCommandHandler : ICommandHandler<DeleteOrderItemCommand>
{
    private readonly OrderingDbContext _context;

    public DeleteOrderItemCommandHandler(OrderingDbContext context)
    {
        _context = context;
    }

    public async Task ExecuteAsync(DeleteOrderItemCommand command)
    {
        var orderItem = await _context.OrderItems.FindAsync(command.Id);
        if (orderItem != null)
        {
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}

using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(FakeStoreApiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderResponseDto>> GetAllByCostumerIdAsync(Guid costumerId, CancellationToken cancellationToken)
        {
            //subquery to get the total amount of products grouped by order
            var orderProducts = from orderProduct in _context.OrderProducts
                                join order in _context.Orders on orderProduct.OrderId equals order.Id
                                where order.CostumerId == costumerId
                                group orderProduct by orderProduct.OrderId into g
                                select new
                                {
                                    OrderId = g.Key,
                                    TotalProductsQuantity = g.Sum(x => x.ProductQuantity),
                                    TotalAmount = g.Sum(x => x.ProductPrice * x.ProductQuantity)
                                };

            var query = from order in _context.Orders
                        join orderProduct in orderProducts on order.Id equals orderProduct.OrderId
                        where order.CostumerId == costumerId
                        select new OrderResponseDto
                        {
                            Id = order.Id,
                            Number = order.Number,
                            CreatedAt = order.CreatedAt,
                            TotalAmount = orderProduct.TotalAmount,
                            TotalProductsQuantity = orderProduct.TotalProductsQuantity
                        };

            return await query.ToListAsync(cancellationToken);
        }
    }
} 
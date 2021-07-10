using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepo : IAsyncRepo<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}

using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implements
{

    public class OrderStatusRepository : GenericRepository<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}

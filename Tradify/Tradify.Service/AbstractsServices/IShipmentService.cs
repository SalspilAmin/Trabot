using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface IShipmentService : IShipmentRepository
    {
        public  Task<(string, int?, int?)> CreateShipment(int subOrderId);
        public  Task<string> UpdateShipmentStatus(int ShipmentId, ShipmentStatus status);

    }
}

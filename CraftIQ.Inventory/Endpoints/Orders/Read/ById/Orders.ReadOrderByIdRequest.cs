﻿using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Orders.Read.ById
{
    public class ReadOrderByIdRequest
    {
        [FromRoute]
        public Guid orderId { get; set; }
    }
}

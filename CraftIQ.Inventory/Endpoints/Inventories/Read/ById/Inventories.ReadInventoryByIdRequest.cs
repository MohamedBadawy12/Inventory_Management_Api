﻿using Microsoft.AspNetCore.Mvc;

namespace CraftIQ.Inventory.Endpoints.Inventories.Read.ById
{
    public class ReadInventoryByIdRequest
    {
        [FromRoute]
        public Guid inventoryId { get; set; }
    }
}

﻿namespace CraftIQ.Inventory.Endpoints.Products.Create
{
    public class CreateProductResponse
    {
        public Guid ProductId { get; set; }

        public CreateProductResponse(Guid productId)
        {
            ProductId = productId;
        }
    }
}

﻿using ECommerce.Domain.Commons;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities
{
    public class Product : Auditable
    {
        public long OwnerId { get; set; }
        public ProductCategory Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool CanDeliver { get; set; }
        public long HowManyLeft { get; set; }
        public string QRCode { get; set; }
        public string Description { get; set; }
    }
}

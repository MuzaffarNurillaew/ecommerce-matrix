﻿using ECommerce.Domain.Commons;
using ECommerce.Domain.Enums;
using System;

namespace ECommerce.Domain.Entities
{  
    public class Order : Auditable
    {        
        public long UserId { get; set; }
        public long ItemId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }        
    }
}

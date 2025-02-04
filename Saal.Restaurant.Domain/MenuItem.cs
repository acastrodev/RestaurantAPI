﻿namespace Saal.Restaurant.Domain
{
    public class MenuItem
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
    }
}
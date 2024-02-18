﻿using System.ComponentModel.DataAnnotations;

namespace GrowthApi.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}

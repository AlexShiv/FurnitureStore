﻿using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FurnitureStore.model
{
    [Table("Category")]
    public class Category
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [NotNull] public string Name { get; set; }

        [OneToMany] public List<Furniture> Furnitures { get; set; }
    }
}
﻿using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FurnitureStore.model
{
    [Table("ShoppingCard")]
    public class ShoppingCard
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [NotNull]
        [ForeignKey(typeof(Furniture), Unique = true)]
        public int FurnitureId { get; set; }

        [NotNull] public int Count { get; set; }
        
        [NotNull] public double FullPrise { get; set; }

        [OneToOne] public Furniture Furniture { get; set; }
        
        [NotNull]
        [ForeignKey(typeof(User))]
        public int UserId { get; set; }
    }
}
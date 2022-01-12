using System;
using SQLite;

namespace FurnitureStore.model
{
    [Table("Promo")]
    public class Promo
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [NotNull] public string Name { get; set; }
        [NotNull] public string Description { get; set; }
        [NotNull] public DateTime BeginDate { get; set; }
        [NotNull] public DateTime EndDate { get; set; }
        [NotNull] public string PhotoPath { get; set; }
    }
}
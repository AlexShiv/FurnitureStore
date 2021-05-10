using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FurnitureStore.model
{
    [Table("ShoppingCard")]
    public class ShoppingCard
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [NotNull, Unique, ForeignKey(typeof(Furniture))] public int FurnitureId { get; set; }
        [NotNull] public int Count { get; set; }

        [OneToOne]
        public Furniture Furniture { get; set; }
    }
}
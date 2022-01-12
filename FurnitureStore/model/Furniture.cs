using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FurnitureStore.model
{
    [Table("Furniture")]
    public class Furniture
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [NotNull] public string Name { get; set; }
        [NotNull] public string Description { get; set; }

        [NotNull]
        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }

        [NotNull] public double Length { get; set; }
        [NotNull] public double Height { get; set; }
        [NotNull] public double Width { get; set; }
        [NotNull] public double Price { get; set; }
        [NotNull] public string PhotoPath { get; set; }
    }
}
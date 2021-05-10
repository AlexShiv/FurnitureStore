using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FurnitureStore.model
{
    [Table("Address")]
    public class Address
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [NotNull] public string Name { get; set; }
        [NotNull, ForeignKey(typeof(City))] public int CityId { get; set; }
    }
}
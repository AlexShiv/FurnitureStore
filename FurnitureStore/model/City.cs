using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FurnitureStore.model
{
    [Table("City")]
    public class City
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [NotNull] public string Name { get; set; }

        [OneToMany]
        public List<Address> Addresses { get; set; }
    }
}
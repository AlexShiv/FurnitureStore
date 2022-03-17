using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FurnitureStore.model
{
    [Table("User")]
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [NotNull] public string FirstName { get; set; }
        [NotNull] public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        [NotNull] public string Phone { get; set; }
        [NotNull] public string Password { get; set; }
        [NotNull] public bool IsSubscribe { get; set; }
        
        [NotNull] public string Role { get; set; }
        
        [NotNull] [ForeignKey(typeof(Address))] public int AddressId { get; set; }
    }
}
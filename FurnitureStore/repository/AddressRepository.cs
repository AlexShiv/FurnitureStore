using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FurnitureStore.config;
using FurnitureStore.model;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace FurnitureStore.repository
{
    public class AddressRepository
    {
        private static AddressRepository database;
        private readonly SQLiteConnection _db;

        private AddressRepository(string fileName)
        {
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(docPath, fileName);
            _db = new SQLiteConnection(path);

            _db.CreateTable<Address>();            
            FillDataBaseIfEmpty();
        }

        public static AddressRepository GetInstance()
        {
            return database ?? (database = new AddressRepository(Constant.DB_NAME));
        }

        public IEnumerable<Address> Fetch()
        {
            return _db.Table<Address>().ToList();
        }

        public Address Fetch(int id)
        {
            return _db.Get<Address>(id);
        }

        public int Delete(int id)
        {
            return _db.Delete<Address>(id);
        }

        public int SaveItem(Address item)
        {
            if (item.Id != 0)
            {
                _db.Update(item);
                return item.Id;
            }

            return _db.Insert(item);
        }

        private void FillDataBaseIfEmpty()
        {
            if (Fetch().Any()) return;
            
            _db.InsertWithChildren(new Address()
            {
                Id = 1,
                Name = "ул. Московская 1",
                CityId = 1
            });            
            _db.InsertWithChildren(new Address()
            {
                Id = 2,
                Name = "ул. Московская 12",
                CityId = 1
            });     
            
            _db.InsertWithChildren(new Address()
            {
                Id = 3,
                Name = "ул. Питерская 4",
                CityId = 2

            });            
            _db.InsertWithChildren(new Address()
            {
                Id = 4,
                Name = "ул. Питерская 45",
                CityId = 2
            });        
            
            _db.InsertWithChildren(new Address()
            {
                Id = 5,
                Name = "ул. Астраханская 25",
                CityId = 3

            });            
            _db.InsertWithChildren(new Address()
            {
                Id = 6,
                Name = "ул. Астраханская 42",
                CityId = 3
            });
        }
    }
}
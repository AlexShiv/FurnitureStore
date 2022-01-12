using System;
using System.Collections.Generic;
using System.IO;
using FurnitureStore.config;
using FurnitureStore.model;
using SQLite;

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
    }
}
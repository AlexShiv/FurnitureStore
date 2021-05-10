using System;
using System.Collections.Generic;
using System.IO;
using FurnitureStore.config;
using FurnitureStore.model;
using SQLite;

namespace FurnitureStore.repository
{
    public class ShoppingCardRepository
    {
        readonly SQLiteConnection _db;

        private static ShoppingCardRepository database;

        private ShoppingCardRepository(string fileName)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(docPath, fileName);
            _db = new SQLiteConnection(path);

            _db.CreateTable<ShoppingCard>();
        }

        public static ShoppingCardRepository GetInstance()
        {
            return database ?? (database = new ShoppingCardRepository(Constant.DB_NAME));
        }

        public IEnumerable<ShoppingCard> Fetch()
        {
            return _db.Table<ShoppingCard>().ToList();
        }

        public ShoppingCard Fetch(int id)
        {
            return _db.Get<ShoppingCard>(id);
        }

        public int Delete(int id)
        {
            return _db.Delete<ShoppingCard>(id);
        }

        public int SaveItem(ShoppingCard item)
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
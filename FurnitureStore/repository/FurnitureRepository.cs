using System;
using System.Collections.Generic;
using System.IO;
using FurnitureStore.config;
using FurnitureStore.model;
using SQLite;

namespace FurnitureStore.repository
{
    public class FurnitureRepository
    {
        private static FurnitureRepository database;
        private readonly SQLiteConnection _db;

        private FurnitureRepository(string fileName)
        {
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(docPath, fileName);
            _db = new SQLiteConnection(path);

            _db.CreateTable<Furniture>();
        }

        public static FurnitureRepository GetInstance()
        {
            return database ?? (database = new FurnitureRepository(Constant.DB_NAME));
        }

        public IEnumerable<Furniture> Fetch()
        {
            return _db.Table<Furniture>().ToList();
        }

        public Furniture Fetch(int id)
        {
            return _db.Get<Furniture>(id);
        }

        public int Delete(int id)
        {
            return _db.Delete<Furniture>(id);
        }

        public int SaveItem(Furniture item)
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
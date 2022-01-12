using System;
using System.Collections.Generic;
using System.IO;
using FurnitureStore.config;
using FurnitureStore.model;
using SQLite;

namespace FurnitureStore.repository
{
    public class CityRepository
    {
        private static CityRepository database;
        private readonly SQLiteConnection _db;

        private CityRepository(string fileName)
        {
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(docPath, fileName);
            _db = new SQLiteConnection(path);

            _db.CreateTable<City>();
        }

        public static CityRepository GetInstance()
        {
            return database ?? (database = new CityRepository(Constant.DB_NAME));
        }

        public IEnumerable<City> Fetch()
        {
            return _db.Table<City>().ToList();
        }

        public City Fetch(int id)
        {
            return _db.Get<City>(id);
        }

        public int Delete(int id)
        {
            return _db.Delete<City>(id);
        }

        public int SaveItem(City item)
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
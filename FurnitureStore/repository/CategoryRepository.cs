using System;
using System.Collections.Generic;
using System.IO;
using FurnitureStore.config;
using FurnitureStore.model;
using SQLite;

namespace FurnitureStore.repository
{
    public class CategoryRepository
    {
        readonly SQLiteConnection _db;

        private static CategoryRepository database;

        private CategoryRepository(string fileName)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(docPath, fileName);
            _db = new SQLiteConnection(path);

            _db.CreateTable<Category>();
        }

        public static CategoryRepository GetInstance()
        {
            return database ?? (database = new CategoryRepository(Constant.DB_NAME));
        }

        public IEnumerable<Category> Fetch()
        {
            return _db.Table<Category>().ToList();
        }

        public Category Fetch(int id)
        {
            return _db.Get<Category>(id);
        }

        public int Delete(int id)
        {
            return _db.Delete<Category>(id);
        }

        public int SaveItem(Category item)
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
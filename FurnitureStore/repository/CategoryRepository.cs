using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FurnitureStore.config;
using FurnitureStore.model;
using SQLite;

namespace FurnitureStore.repository
{
    public class CategoryRepository
    {
        private static CategoryRepository database;
        private readonly SQLiteConnection _db;

        private CategoryRepository(string fileName)
        {
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(docPath, fileName);
            _db = new SQLiteConnection(path);

            _db.CreateTable<Category>();

            FillDataBaseIfEmpty();
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
            var category = _db.Get<Category>(id);
            // TODO не работает поиск с детьми
            // _db.GetChildren<Category>(category, false);
            return category;
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

        private void FillDataBaseIfEmpty()
        {
            if (Fetch().Any()) return;
            _db.Insert(new Category()
            {
                Id = 1,
                Name = "Диваны",
            });
            _db.Insert(new Category
            {
                Id = 2,
                Name = "Столы",
            });
        }
    }
}
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
            FillDataBaseIfEmpty();
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

        public List<Furniture> FetchByCategory(Category category)
        {
            return Fetch().ToList().FindAll(furniture => furniture.CategoryId == category.Id);
        }

        private void FillDataBaseIfEmpty()
        {
            if (Fetch().Any()) return;
            // Диваны
            _db.InsertWithChildren(new Furniture
            {
                Id = 1,
                Name = "Диван1",
                Description = "Описание дивана1",
                Height = 1,
                Length = 1,
                Width = 1,
                Price = 1000,
                CategoryId = 1,
                PhotoPath = "divan1.png"
            });
            _db.InsertWithChildren(new Furniture
            {
                Id = 2,
                Name = "Диван2",
                Description = "Описание дивана2",
                Height = 2,
                Length = 2,
                Width = 2,
                Price = 2000,
                CategoryId = 1,
                PhotoPath = "divan2.png"
            });               
            _db.InsertWithChildren(new Furniture
            {
                Id = 3,
                Name = "Divan3",
                Description = "Описание дивана3",
                Height = 3,
                Length = 3,
                Width = 3,
                Price = 3000,
                CategoryId = 1,
                PhotoPath = "divan2.png"
            });            
            
            // Кресла
            _db.InsertWithChildren(new Furniture
            {
                Id = 3,
                Name = "Стол1",
                Description = "Описание Стола1",
                Height = 3,
                Length = 3,
                Width = 3,
                Price = 3000,
                CategoryId = 2,
                PhotoPath = "stol1.png"
            });
            _db.InsertWithChildren(new Furniture
            {
                Id = 4,
                Name = "Стол2",
                Description = "Описание стола2",
                Height = 4,
                Length = 4,
                Width = 4,
                Price = 4000,
                CategoryId = 2,
                PhotoPath = "stol2.png"
            });
        }
    }
}
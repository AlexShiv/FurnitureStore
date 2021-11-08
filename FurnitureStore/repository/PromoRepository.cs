using System;
using System.Collections.Generic;
using System.IO;
using FurnitureStore.config;
using FurnitureStore.model;
using SQLite;

namespace FurnitureStore.repository
{
    public class PromoRepository
    {
        readonly SQLiteConnection _db;

        private static PromoRepository database;

        private PromoRepository(string fileName)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(docPath, fileName);
            _db = new SQLiteConnection(path);

            _db.CreateTable<Promo>();

            _db.Insert(new Promo()
            {
                Id = 1,
                Name = "Name1",
                Description = "Description1",
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(7),
                PhotoPath = "PhotoPath1"
            });
            _db.Insert(new Promo()
            {
                Id = 2,
                Name = "Name2",
                Description = "Description2",
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(3),
                PhotoPath = "PhotoPath2"
            });
        }

        public static PromoRepository GetInstance()
        {
            return database ?? (database = new PromoRepository(Constant.DB_NAME));
        }

        public IEnumerable<Promo> Fetch()
        {
            return _db.Table<Promo>().ToList();
        }

        public Promo Fetch(int id)
        {
            return _db.Get<Promo>(id);
        }

        public int Delete(int id)
        {
            return _db.Delete<Promo>(id);
        }

        public int SaveItem(Promo item)
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
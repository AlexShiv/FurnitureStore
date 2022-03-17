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
    public class PromoRepository
    {
        private static PromoRepository database;
        private readonly SQLiteConnection _db;

        private PromoRepository(string fileName)
        {
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(docPath, fileName);
            _db = new SQLiteConnection(path);

            _db.CreateTable<Promo>();

            FillDataBaseIfEmpty();
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
        
        private void FillDataBaseIfEmpty()
        {
            if (Fetch().Any()) return;
            _db.Insert(new Promo
            {
                Id = 1,
                Name = "День рождения!",
                Description = "Предоставляем скижку 10% за неделю до и после дня рождения",
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(7),
                PhotoPath = "PhotoPath1"
            });
            _db.Insert(new Promo
            {
                Id = 2,
                Name = "1 + 1 = 3",
                Description = "При покупке дивана и кресла, второе кресло в подарок",
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(3),
                PhotoPath = "PhotoPath2"
            });
        }

    }
}
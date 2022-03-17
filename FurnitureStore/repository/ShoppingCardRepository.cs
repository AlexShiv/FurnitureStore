using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FurnitureStore.config;
using FurnitureStore.model;
using SQLite;

namespace FurnitureStore.repository
{
    public class ShoppingCardRepository
    {
        private static ShoppingCardRepository database;
        private readonly SQLiteConnection _db;

        private ShoppingCardRepository(string fileName)
        {
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(docPath, fileName);
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

        public void AddOrIncrementCount(Furniture furniture)
        {
            ShoppingCard shoppingCard;
            try
            {
                shoppingCard = Fetch().First(card => card.FurnitureId == furniture.Id);
                shoppingCard.Count++;
                shoppingCard.FullPrise = shoppingCard.Count * furniture.Price;
                _db.Update(shoppingCard);
            }
            catch (InvalidOperationException)
            {
                shoppingCard = new ShoppingCard
                {
                    FurnitureId = furniture.Id,
                    Count = 1,
                    FullPrise = furniture.Price
                };
                SaveItem(shoppingCard);
            }
        }
    }
}
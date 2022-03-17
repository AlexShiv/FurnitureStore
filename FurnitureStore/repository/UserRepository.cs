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
    public class UserRepository
    {
        private static UserRepository database;
        private readonly SQLiteConnection _db;

        private UserRepository(string fileName)
        {
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(docPath, fileName);
            _db = new SQLiteConnection(path);

            _db.CreateTable<User>();
            FillDataBaseIfEmpty();
        }

        public static UserRepository GetInstance()
        {
            return database ?? (database = new UserRepository(Constant.DB_NAME));
        }

        public IEnumerable<User> Fetch()
        {
            return _db.Table<User>().ToList();
        }

        public User Fetch(int id)
        {
            return _db.Get<User>(id);
        }

        public int Delete(int id)
        {
            return _db.Delete<User>(id);
        }

        public int SaveItem(User item)
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
            
            _db.InsertWithChildren(new User()
            {
                Id = 1,
                FirstName = "Иван",
                LastName = "Иванов",
                Patronymic = "Иванович",
                Birthday = new DateTime(1990,1,1),
                Phone = "123",
                Password = "qwer",
                IsSubscribe = true,
                Role = Constant.USER_ROLE,
                AddressId = 1
            });    
            
            _db.InsertWithChildren(new User()
            {
                Id = 2,
                FirstName = "Менеджер",
                LastName = "Менеджеров",
                Patronymic = "Менедрежович",
                Birthday = new DateTime(1990,1,1),
                Phone = "456",
                Password = "qwer",
                IsSubscribe = true,
                Role = Constant.ADMIN_ROLE,
                AddressId = 5
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using FurnitureStore.config;
using FurnitureStore.model;
using SQLite;

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
    }
}
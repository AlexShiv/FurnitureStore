using System.Linq;
using FurnitureStore.config;
using FurnitureStore.model;
using FurnitureStore.repository;

namespace FurnitureStore.viewModel
{
    public static class VmUser
    {
        private static User _user;

        public static User User
        {
            get => _user;
            set => _user = value;
        }

        public static void ForgotUser()
        {
            _user = null;
        }

        public static bool CheckPassword(string login, string password)
        {
            _user = null;
            var userRepository = UserRepository.GetInstance();
            var users = userRepository.Fetch().ToList();
            if (!users.Exists(user => user.Phone == login && user.Password == password))
            {
                return false;
            }

            _user = users.First(user => user.Phone == login && user.Password == password);
            return true;
        }

        public static bool IsAdmin()
        {
            return _user != null && _user.Role == Constant.ADMIN_ROLE;
        }
    }
}
using AspNetCoreApi.Models;
using System.Collections.Generic;

namespace AspNetCoreApi.Repository
{
    public interface IUserRepository
    {
        int Add(User user);

        List<User> GetList();

        User GetUser(int id);

        int EditUser(User user);

        int DeleteUser(int id);
    }
}

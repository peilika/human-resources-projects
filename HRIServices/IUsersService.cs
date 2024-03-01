using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIServices
{
    public interface IUsersService
    {
        void Test();
        Task<Users> LoginAsync(Users users);
        Task<List<Users>> GetAll();
    }
}

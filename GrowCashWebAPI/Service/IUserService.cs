using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowCashWebAPI.Model;

namespace GrowCashWebAPI.Service
{
    public interface IUserService
    {
        UserModel Create(UserModel userModel);
        UserModel FindById(int id);
        List<UserModel> FindAll();
        UserModel Update(UserModel userModel);
        void Delete(int id);
    }
}
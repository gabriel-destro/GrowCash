using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowCashWebAPI.Model;

namespace GrowCashWebAPI.Service
{
    public interface IAccountService
    {
        AccountModel Create(AccountModel accountModel);
        AccountModel FindById(int id);
        List<AccountModel> FindAll(int idUser);
        AccountModel Update(AccountModel accountModel);
        void Delete(int id);
    }
}
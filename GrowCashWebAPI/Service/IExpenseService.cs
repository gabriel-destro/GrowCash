using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowCashWebAPI.Model;

namespace GrowCashWebAPI.Service
{
    public interface IExpenseService
    {
        ExpenseModel Create(ExpenseModel userModel);
        ExpenseModel FindById(int id);
        List<ExpenseModel> FindAll(int idAccont);
        ExpenseModel Update(ExpenseModel userModel);
        void Delete(int id);
    }
}
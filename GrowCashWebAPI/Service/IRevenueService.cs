using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowCashWebAPI.Model;

namespace GrowCashWebAPI.Service
{
    public interface IRevenueService
    {
        RevenueModel Create(RevenueModel revenueModel);
        RevenueModel FindById(int id);
        List<RevenueModel> FindAll(int idAccont);
        RevenueModel Update(RevenueModel revenueModel);
        void Delete(int id);
    }
}
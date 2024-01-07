using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowCashWebAPI.Model;
using GrowCashWebAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GrowCashWebAPI.Service.Implementations
{
    public class RevenueService : IRevenueService
    {
        private MySQLContext _context;

        public RevenueService(MySQLContext context)
        {
            _context = context;
        }
        
        public List<RevenueModel> FindAll(int idAccount)
        {
            var revenue = _context.Revenues.Where(p => p.Id_Account == idAccount).ToList();// ?? throw new Exception("Listas de receitas não encontradas");
            return revenue;
        }

        public RevenueModel FindById(int id)
        {
            return _context.Revenues.SingleOrDefault(p => p.Id.Equals(id)) ?? throw new Exception("Receitas não encontrado");
        }

        public RevenueModel Create(RevenueModel revenueModel)
        {
            try
            {
                _context.Add(revenueModel);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Lida com exceções específicas do Entity Framework.
                throw new Exception("Erro ao criar a receita no banco de dados", ex);
            }
            catch (Exception ex)
            {
                // Lida com outras exceções que podem ocorrer durante o processo de atualização.
                throw new Exception("Erro desconhecido ao atualizar o usuário", ex);
            }

            return revenueModel;
        }

        public RevenueModel Update(RevenueModel revenueModel)
        {
            var result = _context.Revenues.SingleOrDefault(p => p.Id.Equals(revenueModel.Id));

            if(result == null) throw new Exception("Usuário não encontrado");

            try
            {
                _context.Entry(result).CurrentValues.SetValues(revenueModel);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Lida com exceções específicas do Entity Framework.
                throw new Exception("Erro ao atualizar a receita no banco de dados", ex);
            }
            catch (Exception ex)
            {
                // Lida com outras exceções que podem ocorrer durante o processo de atualização.
                throw new Exception("Erro desconhecido ao atualizar o usuário", ex);
            }

            return revenueModel;
        }

        public void Delete(int id)
        {
            var result = _context.Revenues.SingleOrDefault(p => p.Id.Equals(id));
            if(result != null)
            {
                try
                {
                    _context.Revenues.Remove(result);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    // Lida com exceções específicas do Entity Framework.
                    throw new Exception("Erro ao deletar a receita no banco de dados", ex);
                }
                catch (Exception ex)
                {
                    // Lida com outras exceções que podem ocorrer durante o processo de atualização.
                    throw new Exception("Erro desconhecido ao atualizar o usuário", ex);
                }
            }
        }
    }
}
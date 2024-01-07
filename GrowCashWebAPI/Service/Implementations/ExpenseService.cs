using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowCashWebAPI.Model;
using GrowCashWebAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GrowCashWebAPI.Service.Implementations
{
    public class ExpenseService : IExpenseService
    {
        private MySQLContext _context;
        
        public ExpenseService(MySQLContext context)
        {
            _context = context;
        }

        public List<ExpenseModel> FindAll(int idUser)
        {
            return _context.Expenses.Where(p => p.Id_Account == idUser).ToList() ?? throw new Exception("Despesas não encontradas");
        }

        public ExpenseModel FindById(int id)
        {
            return _context.Expenses.SingleOrDefault(p => p.Id.Equals(id)) ?? throw new Exception("Despesa não encontrado");
        }

        public ExpenseModel Create(ExpenseModel expenseModel)
        {
            try
            {
                _context.Add(expenseModel);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Lida com exceções específicas do Entity Framework.
                throw new Exception("Erro ao criar a conta no banco de dados", ex);
            }
            catch (Exception ex)
            {
                // Lida com outras exceções que podem ocorrer durante o processo de atualização.
                throw new Exception("Erro desconhecido ao atualizar o usuário", ex);
            }

            return expenseModel;
        }

        public ExpenseModel Update(ExpenseModel expenseModel)
        {
            var result = _context.Expenses.SingleOrDefault(p => p.Id.Equals(expenseModel.Id));

            if(result == null) throw new Exception("Despesa não encontrado");

            try
            {
                _context.Entry(result).CurrentValues.SetValues(expenseModel);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Lida com exceções específicas do Entity Framework.
                throw new Exception("Erro ao atualizar despesa no banco de dados", ex);
            }
            catch (Exception ex)
            {
                // Lida com outras exceções que podem ocorrer durante o processo de atualização.
                throw new Exception("Erro desconhecido ao atualizar o usuário", ex);
            }

            return expenseModel;
        }

        public void Delete(int id)
        {
            var result = _context.Expenses.SingleOrDefault(p => p.Id.Equals(id));
            if(result != null)
            {
                try
                {
                    _context.Expenses.Remove(result);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    // Lida com exceções específicas do Entity Framework.
                    throw new Exception("Erro ao deletar despesa no banco de dados", ex);
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
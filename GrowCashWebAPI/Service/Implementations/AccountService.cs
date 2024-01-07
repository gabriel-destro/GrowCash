using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowCashWebAPI.Model;
using GrowCashWebAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GrowCashWebAPI.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private MySQLContext _context;
        private IRevenueService _revenueService;
        private IExpenseService _expenseService;
        
        public AccountService(
            MySQLContext context,
            IRevenueService revenueService,
            IExpenseService expenseService
        )
        {
            _context = context;
            _revenueService = revenueService;
            _expenseService = expenseService;
        }

        public List<AccountModel> FindAll(int idUser)
        {
            var accounts = _context.Accounts
            .Where(p => p.Id_User == idUser).ToList()
            .Select(account =>
            {
                account.Revenues = _revenueService.FindAll(account.Id).ToList();
                account.Expenses = _expenseService.FindAll(account.Id).ToList();
                return account;
            })
            .ToList();

        if (!accounts.Any())
        {
            throw new Exception("Contas não encontradas");
        }

        return accounts;
        }

        public AccountModel FindById(int id)
        {
            var account = _context.Accounts.SingleOrDefault(p => p.Id.Equals(id)) ?? throw new Exception("Conta não encontrado");
                account.Revenues = _revenueService.FindAll(account.Id).ToList();
                account.Expenses = _expenseService.FindAll(account.Id).ToList();
            return account;
        }

        public AccountModel Create(AccountModel accountModel)
        {
            try
            {
                _context.Add(accountModel);
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

            return accountModel;
        }

        public AccountModel Update(AccountModel accountModel)
        {
            var result = _context.Accounts.SingleOrDefault(p => p.Id.Equals(accountModel.Id));

            if(result == null) throw new Exception("Usuário não encontrado");

            try
            {
                _context.Entry(result).CurrentValues.SetValues(accountModel);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Lida com exceções específicas do Entity Framework.
                throw new Exception("Erro ao atualizar a conta no banco de dados", ex);
            }
            catch (Exception ex)
            {
                // Lida com outras exceções que podem ocorrer durante o processo de atualização.
                throw new Exception("Erro desconhecido ao atualizar o usuário", ex);
            }

            return accountModel;
        }

        public void Delete(int id)
        {
            var result = _context.Accounts.SingleOrDefault(p => p.Id.Equals(id));
            if(result != null)
            {
                try
                {
                    _context.Accounts.Remove(result);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    // Lida com exceções específicas do Entity Framework.
                    throw new Exception("Erro ao deletar a conta no banco de dados", ex);
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
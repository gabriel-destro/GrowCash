using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowCashWebAPI.Model;
using GrowCashWebAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace GrowCashWebAPI.Service.Implementations
{
    public class UserService : IUserService
    {
        private MySQLContext _context;

        public UserService(MySQLContext context)
        {
            _context = context;
        }

        public List<UserModel> FindAll()
        {
            return _context.Users.ToList();
        }

        public UserModel FindById(int id)
        {
            return _context.Users.SingleOrDefault(p => p.Id.Equals(id)) ?? throw new Exception("Usuário não encontrado");
        }

        public UserModel Create(UserModel userModel)
        {
            try
            {
                _context.Users.Add(userModel);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return userModel;
        }

        public UserModel Update(UserModel userModel)
        {
            // if (!Exist(userModel.Id))
            // {
            //     throw new Exception("Usuário não encontrado");
            // }

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(userModel.Id));

            if (result == null)
            {
                // A verificação já foi feita no início, então isso não deveria acontecer.
                throw new Exception("Usuário não encontrado");
            }

            try
            {
                _context.Entry(result).CurrentValues.SetValues(userModel);
                _context.SaveChanges();

                return userModel; // Retorna o userModel após a atualização bem-sucedida.
            }
            catch (DbUpdateException ex)
            {
                // Lida com exceções específicas do Entity Framework.
                throw new Exception("Erro ao atualizar o usuário no banco de dados", ex);
            }
            catch (Exception ex)
            {
                // Lida com outras exceções que podem ocorrer durante o processo de atualização.
                throw new Exception("Erro desconhecido ao atualizar o usuário", ex);
            }
        }

        public void Delete(int id)
        {
            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(id));
            if(result != null)
            {
                try
                {
                    _context.Users.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // private bool Exist(int id)
        // {
        //     return _context.Users.Any(p => p.Id.Equals(id));
        // }
    }
}
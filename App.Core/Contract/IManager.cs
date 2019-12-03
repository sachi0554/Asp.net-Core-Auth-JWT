using App.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contract
{
    public interface IManager
    {
        Task<bool> AddRole(string role);
        Task<bool> AssignRole(string email, string role);
        Task<bool> RemoveRole(string email, string role);
        Task<bool> AddClaims(UserClaims userClaims);
    }
}

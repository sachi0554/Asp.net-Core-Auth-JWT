using App.Core.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using App.Domain.Model;
using System.Security.Claims;
using App.Domain;
using System.Data.Entity;

namespace App.Core.Abstract
{
    public class Manager : IManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationContext _context;

        public Manager(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,  ApplicationContext  context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        public async Task<bool> AddRole(string roleName)
        {
            
            bool x = await _roleManager.RoleExistsAsync(roleName);
            if (x!=true)
            {
                var role = new IdentityRole();
                role.Name = roleName;
                var result = await _roleManager.CreateAsync(role);
                return result.Succeeded;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AssignRole(string email, string role )
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            //var User = new ApplicationUser();
            if (user != null)
            { 
               var result = await _userManager.AddToRoleAsync(user, role);
                return result.Succeeded;
            }else
            {
                return false;
            }
        }

        public async Task<bool> RemoveRole(string email, string role)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            //var User = new ApplicationUser();
            if (user != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, role);
                return result.Succeeded;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddClaims(UserClaims userClaims)
        {
            try
            {
                var data = new UserClaims
                {
                    Id = Guid.NewGuid().ToString(),
                    ClaimName = userClaims.ClaimName
                };

                var v = await _context.UserClaim.AddAsync(data);
                var res = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }    

           
        }

        public async Task<bool> AssignClaims(UserClaims userClaims)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(userClaims.Id);
                if(user != null)
                {
                    var claims = await _userManager.GetClaimsAsync(user);
                    var result = await _userManager.RemoveClaimsAsync(user, claims);
                    if(!result.Succeeded)
                    {
                        return false;
                    }
                    var claim = new Claim(userClaims.ClaimName, userClaims.ClaimName);
                    result = await _userManager.AddClaimAsync(user, claim);
                    if(result.Succeeded)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }


        }


    }
}

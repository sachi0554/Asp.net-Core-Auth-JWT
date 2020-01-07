using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Contract;
using App.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth20_V1.Controllers.v1
{
   
    /*[Authorize(Roles  = "Admin")]*/
    public class ManagerController : Controller
    {
        private readonly IManager _manager;

        public ManagerController(IManager manager )
        {
            _manager = manager;
        }
       /* [Authorize(Policy = "Crud")]*/
        [HttpPost("api/CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _manager.AddRole(roleName);
            if(result == true)
            {
                return Ok(new { Message = "Role created" });
            }
            else
            {
                return Ok(new { Message = "Role already exsits " });
            }
            
        }

        [HttpPost("api/AssignRole")]
        public async Task<IActionResult> AddRole(string email, string role)
        {
            var result = await _manager.AssignRole(email , role);
            if (result == true)
            {
                return Ok(new { Message = "Role assign to user" });
            }
            else
            {
                return Ok(new { Message = "Something went wrong " });
            }

        }

        [HttpPost("api/RemoveRole")]
        public async Task<IActionResult> RemoveRole(string email, string role)
        {
            var result = await _manager.AssignRole(email, role);
            if (result == true)
            {
                return Ok(new { Message = "Role assign to user" });
            }
            else
            {
                return Ok(new { Message = "Something went wrong " });
            }

        }

        [HttpPost("api/CreateClaim")]
        public async Task<IActionResult> CreateClaim(UserClaims userClaims)
        {
            var result = await _manager.AddClaims(userClaims);
            if (result == true)
            {
                return Ok(new { Message = "Claim created" });
            }
            else
            {
                return Ok(new { Message = "Something went wrong " });
            }

        }

        [HttpPost("api/AssignClaim")]
        public async Task<IActionResult> AssignClaim(UserClaims userClaims)
        {
            var result = await _manager.AssignClaims(userClaims);
            if (result == true)
            {
                return Ok(new { Message = "Claim assign" });
            }
            else
            {
                return Ok(new { Message = "Something went wrong " });
            }

        }

    }
}
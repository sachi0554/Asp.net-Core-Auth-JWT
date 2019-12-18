using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Contract;
using App.Core.Model;
using App.Core.ResponseFlow;
using Microsoft.AspNetCore.Mvc;

namespace Auth20_V1.Controllers.v1
{
    public class GameController : Controller
    {
        private IRepository<Game> _context;
        public GameController(IRepository<Game> repository)
        {
            _context = repository;
        }
        [HttpPost]
        [Route("api/game/create")]
        public async Task<IActionResult> Create([FromBody] Game game)
        {
            _context.Insert(game);
            await _context.Commit();
            return Ok(new SuccessResponse
            {
                Message = "Game Create Successful",
                Status = "201"
            }) ;
         
        }

        [HttpGet]
        [Route("api/game/AllGames")]
        public IActionResult Get()
        {
            return Ok( _context.Collection().ToList());
        }

        [HttpGet]
        [Route("api/game/Id")]
        public  IActionResult Get(string Id)
        {
            Game game = _context.Find(Id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(_context.Find(Id));
        }

        [HttpDelete]
        [Route("api/game/Id")]
        public async Task<IActionResult> Delete(string Id)
        {
            Game game = _context.Find(Id);
            if (game == null)
            {
                return NotFound();
            }
            else
            {
                _context.Delete(Id);
                await _context.Commit();
            }
        
            return Ok();
        }

        [HttpPatch]
        [Route("api/game/update")]
        public async Task<IActionResult> Update([FromBody] Game game, string id)
        {
            Game gameupdate = _context.Find(id);
            if (gameupdate == null)
            {
                return NotFound();
            }
            else
            {
                gameupdate.GameCode = game.GameCode;
                gameupdate.GameName = game.GameName;
                gameupdate.GamePoint = game.GamePoint;
                gameupdate.GameType = game.GameType;
                gameupdate.Game_Description = game.Game_Description;
               
                await _context.Commit();
            }
          
            return Ok();
        }
    }
}
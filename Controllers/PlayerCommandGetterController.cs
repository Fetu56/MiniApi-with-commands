using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlayerCommandGetterController : ControllerBase
    {

        private readonly ILogger<PlayerCommandGetterController> _logger;

        public PlayerCommandGetterController(ILogger<PlayerCommandGetterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<Player> Get(int commandId)
        {
            List<Command> command = CommandGetterController.GetCommands();
            try
            {
                if (commandId > command.Count || commandId < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return command[commandId].Players;
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
            
        }
        [HttpGet]
        [ActionName("GetByID")]
        public Player Get(int commandId, int id)
        {
            List<Command> command = CommandGetterController.GetCommands();
            try
            {
                if (commandId > command.Count || commandId < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (id > command[commandId].Players.Count || id < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return command[commandId].Players[id];
            }
            catch(ArgumentOutOfRangeException)
            {
                return null;
            }
        }
    }
}

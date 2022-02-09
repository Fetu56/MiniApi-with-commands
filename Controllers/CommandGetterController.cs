using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommandGetterController : Controller
    {
        private readonly ILogger<CommandGetterController> _logger;
        private static List<Command> Commands { get; set; }
        public static List<Command>  GetCommands()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Command>>(new HttpClient().GetStringAsync("https://localhost:44318/api/CommandGetter/GetAll").Result).ToList();
        }
        public static void UpdateCommands(List<Command> commands)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(commands);
                Commands = commands;
                System.IO.File.WriteAllText("commands.txt", JsonConvert.SerializeObject(Commands, Formatting.None));
            }
            catch (ArgumentOutOfRangeException){}
            
        }
        public CommandGetterController(ILogger<CommandGetterController> logger)
        {
            _logger = logger;
            Commands = new List<Command>();
            if(System.IO.File.Exists("commands.txt"))
            {
            Commands = JsonConvert.DeserializeObject<List<Command>>(System.IO.File.ReadAllText("commands.txt"));
            }
            else
            {
                Commands.Add(new Command(
            new List<Player>(){
            new Player(){BirthDate=new DateTime(2000, 3, 20), Name="Yaroslav", SurName="Svistun", CareereYears=3 },
            new Player(){BirthDate=new DateTime(1998, 3, 20), Name="Andrei", SurName="Apollov", CareereYears=2 },
            new Player(){BirthDate=new DateTime(2000, 3, 20), Name="Kirill", SurName="Urusov", CareereYears=1 },
            new Player(){BirthDate=new DateTime(2001, 3, 20), Name="Vitya", SurName="Krestov", CareereYears=2 },
            new Player(){BirthDate=new DateTime(2003, 3, 20), Name="Egor", SurName="Kosyak", CareereYears=4 }}, "NaTu"));
            }
            
        }

        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<Command> Get()
        {
            return Commands;
        }
        [HttpGet]
        [ActionName("GetByID")]
        public Command Get(int id)
        {
            try
            {
                if (id > Commands.Count || id < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return Commands[id];
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
            
        }
    }
}

using CST_350_Minesweeper.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Milestone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Milestone.Controllers
{
    [ApiController]
    [Route("api")]
    [TypeFilter(typeof(AuthenticationFilter))]

    public class GameBoardAPI : ControllerBase
    {
        static List<BoardModel> DBObjects = new List<BoardModel>();
        GameServices gameServices = new GameServices();

        [HttpGet("showSavedGames")]
        public ActionResult<IEnumerable<BoardModel>> Index()
        {
            List<BoardModel> savedGames = gameServices.GetAllGames().ToList();
            DBObjects = gameServices.GetAllGames().ToList();
            return savedGames;
        }

        [HttpGet("showSavedGame/{id}")]
        public ActionResult<BoardModel> SavedGames(int id)
        {
            return gameServices.GetSavedGame(id);   
        }

        [HttpGet("deleteOneGame/{id}")]
        public ActionResult<bool> DeleteGame(int id)
        {
            if (gameServices.GetSavedGame(id) != null)
            {

                return gameServices.DeleteSavedGame(id);
            }
            else
            {
                return false;
            }
        }

        [HttpGet("addOneGame/{board}")]
        public ActionResult<bool> AddGame(string board)
        {
            GameServices services = new();

            BoardModel model = JsonSerializer.Deserialize<BoardModel>(board);
            DBObjects.Add(model);
            
            return services.isSaved(model);
        }

    }
}

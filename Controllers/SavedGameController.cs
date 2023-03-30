using CST_350_Minesweeper.Services;
using Microsoft.AspNetCore.Mvc;
using Milestone.Controllers;

namespace CST_350_Minesweeper.Controllers
{
    public class SavedGameController : Controller
    {
        public IActionResult GameWith(int id)
        {
            var result = new GameBoardAPI().SavedGames(id).Value;

            return RedirectToAction("Index","Game", result);
        }
        public IActionResult Index()
        {
            var result = new GameBoardAPI().Index().Value;

            return View("Index", result);
        }
        public IActionResult DeleteGame(int id)
        {

            GameServices gameServices = new GameServices();
            

            if (gameServices.DeleteSavedGame(id))
            {

                return View("Index", new GameBoardAPI().Index().Value);
            }
            else
                return View("Index", new GameBoardAPI().Index().Value);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Milestone.Models;

namespace CST_350_Minesweeper.Services
{
    public class GameServices
    {

        GameDAO gameDAO = new GameDAO();

        public bool isSaved(BoardModel board) 
        {
            return gameDAO.SaveGame(board);
        }
        
        public BoardModel GetSavedGame(int id) 
        {
            return gameDAO.GetSavedGameFromDatabase(id);
        }

        public IEnumerable<BoardModel> GetAllGames()
        {
            return gameDAO.GetAllSavedGamesFromDatabase();
        }

        public bool DeleteSavedGame(int id)
        {
            return gameDAO.DeleteOneGameFromDatabase(id);
        }
    }
}

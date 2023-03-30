using CST_350_Minesweeper.Models;

namespace CST_350_Minesweeper.Services
{
    public class LoginService
    {
        LoginDAO loginDAO = new LoginDAO();

        public User IsValid(User user)
        {
            return loginDAO.FindUserByNameAndPassword(user);
        }

        public User GetCurrentLoggedInUser()
        {
            return loginDAO.GetUser();
        }

        public int GetUserID()
        {
            return loginDAO.GetCurrentUserID();
        }
     
    }
}
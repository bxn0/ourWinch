using ourWinch.Models;

namespace ourWinch.Services
{
    public class UserService
    {
        // Mock user data for illustration. Never hard-code users or plain-text passwords like this in real applications.
        private List<ApplicationUser> mockUsers = new List<ApplicationUser>
        {
            new ApplicationUser { Id = 1, Username = "admin", Password = "password123" }
        };

        public ApplicationUser Authenticate(string username, string password)
        {
            return mockUsers.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }

}

namespace InterviewPreezieAPI
{
    public class DataStore
    {
        private static List<Users> _users;

        public DataStore()
        {
            _users = new List<Users>
        {
            new Users {  DisplayName = "Test User 1" , Email="user1@test.com", Password="123"},
            new Users { DisplayName = "Test User 2" , Email="user2@test.com", Password="1234" },
            new Users {  DisplayName = "Test User 3" , Email="user3@test.com", Password="12345" }
        };
        }

        public async Task AddUser(Users users)
        {
            _users.Add(users);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            var usersWithoutPassword = _users.Select(u => new Users
            {
                Email = u.Email,
                DisplayName = u.DisplayName,
                Password = "****"
            }).OrderBy(x=>x.DisplayName); ;

            return await Task.FromResult(usersWithoutPassword);
        }
        public async Task<Users> GetUserByEmail(string Email)
        {
            return await Task.FromResult(_users.FirstOrDefault(u => u.Email == Email));
        }
        public async Task<Users> Update(Users updatedUser)
        {
            var existingUser = _users.FirstOrDefault(u => u.Email == updatedUser.Email);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.Password = updatedUser.Password;
            existingUser.DisplayName = updatedUser.DisplayName;

            return existingUser;
        }

    }
}

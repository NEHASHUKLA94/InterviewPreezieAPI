using InterviewPreezieAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Errors.Model;
using System.ComponentModel.DataAnnotations;
using static InterviewPreezieAPI.Queries.UserCommand;

namespace InterviewPreezieAPI.Handler
{
   
    public class UsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<Users>>
    {
        private readonly DataStore _dataStore;
        public UsersHandler(DataStore dataStore) => _dataStore = dataStore;
        public async Task<IEnumerable<Users>> Handle(GetUsersQuery request,
            CancellationToken cancellationToken) => await _dataStore.GetAllUsers();

        public class AddUsersHandler : IRequestHandler<AddUserCommand>
        {
            private readonly DataStore _dataStore;

            public AddUsersHandler(DataStore dataStore) => _dataStore = dataStore;

            public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
            {
                if (!IsValidPassword(request.Users.Password))
                {
                    throw new ValidationException("Password must be at least 8 characters with at least 1 uppercase and 1 numeric character.");
                }
                await _dataStore.AddUser(request.Users);

                return;
            }
        }


        public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
        {
            private readonly DataStore _dataStore;

            public UpdateUserHandler(DataStore dataStore) => _dataStore = dataStore;
            public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _dataStore.GetUserByEmail(request.Users.Email);

                if (user == null)
                {
                    throw new NotFoundException($"User with ID {request.Users.Email} not found.");
                }
                if (string.IsNullOrEmpty(request.Users.Password))
                {
                    throw new ValidationException("Password cannot be empty.");
                }
                if (!IsValidPassword(request.Users.Password))
                {
                    throw new ValidationException("Password must be at least 8 characters with at least 1 uppercase and 1 numeric character.");
                }
               
                    user.Password = request.Users.Password;
               
                user.DisplayName = request.Users.DisplayName;
                await _dataStore.Update(user);

                return;// Unit.Value;
            }
        }
        private static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (password.Length < 8)
            {
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            return true;
        }

    }
}

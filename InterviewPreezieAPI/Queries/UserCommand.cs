using MediatR;

namespace InterviewPreezieAPI.Queries
{
    public class UserCommand
    {
        public record AddUserCommand(Users Users) : IRequest;
        public record UpdateUserCommand(Users Users) : IRequest;
    }
}

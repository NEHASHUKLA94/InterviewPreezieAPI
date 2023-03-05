using MediatR;

namespace InterviewPreezieAPI.Queries
{
    
        public record GetUsersQuery() : IRequest<IEnumerable<Users>>;
    
}

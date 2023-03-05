using InterviewPreezieAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static InterviewPreezieAPI.Queries.UserCommand;

namespace InterviewPreezieAPI.Controllers
{
    [ApiController]
  
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
      

        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet]
        [Route("api/users")]
        public async Task<ActionResult> GetUsers()
        {
            var products = await _mediator.Send(new GetUsersQuery());
            return Ok(products);
        }

        [HttpPost]
        [Route("api/users")]
        public async Task<ActionResult> AddUsers([FromBody] Users user)
        {
            {
                await _mediator.Send(new AddUserCommand(user));
                return StatusCode(201);
            }
        }
        [HttpPut]
        [Route("api/usersId")]
        public async Task<IActionResult> UpdateUser( [FromBody] Users user)
        {
            //user.Email = emailid;
            await _mediator.Send(new UpdateUserCommand(user));
            return NoContent();
        }

    }
}
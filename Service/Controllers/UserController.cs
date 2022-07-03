using Microsoft.AspNetCore.Mvc;
using Service.Auth.Attribute;
using Service.Models;
using Service.Services;

namespace Service
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService service)
        {
                _userService = service;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var acc = await _userService.Authenticate(model.Username, model.Password);
            if(acc is null)
                return NotFound();
            return Ok(acc);

        }

        [HttpGet("UserInfo")]
        public ContentResult UserInfo(int id)
        {
            return Content(_userService.UserInfo(id, out var user) 
                ? $"<div><p>Id: {user.Id}</p><p>Name: {user.Name}</p><p>Status: {user.Status}</p></div>" 
                : $"<div><p>No user with id {id} found</p></div>", "text/html");
        }
        [Authorize]
        [HttpPost("CreateUser")]
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public async Task<object> CreateUser([FromBody] Request request)
        {
            if (!await _userService.Create(request.User))
            {
                return new Response()
                {
                    Success = false, ErrorId = 1, ErrorMsg = $"User with id {request.User.Id} already exists"
                };
            }

            return new Response()
            {
                Success = true,
                ErrorId = 0,
                User = request.User
            };

        }
        [Authorize]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("RemoveUser")]
        public object Delete([FromBody]DeleteBody deleteBody)
        {
            if( _userService.Remove(deleteBody.RemoveUser.Id, out var deletedUser))
            {
                return new
                {
                    Msg = "User was removed",
                    Success = true,
                    deletedUser
                };
            }

            return new
            {
                ErrorId = 2,
                Msg = "User not found",
                Success = false
            };
        }
        [Authorize]
        [HttpPost("SetStatus")]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        public async Task<User> SetStatus([FromForm] SetStatus setStatus)
        {
           var user = await _userService.SetStatus(setStatus.Id, setStatus.status);
           return user;
        }
    }
}

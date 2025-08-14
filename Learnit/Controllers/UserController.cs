using Application.Dto.JWT;
using Application.Dto.User;
using Application.Repositories;
using Application.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthNest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserAuth _userAuth;
        public UserController(UserAuth userAuth)
        {
            _userAuth = userAuth;
        }

        [HttpPost]
        [Route("SignUp")]
        [AllowAnonymous]
        public async Task<Response<JWTResponse>> SignUpUser([FromBody] UserSignUpDto request)
            => await _userAuth.SignUpUser(request);

    }
}

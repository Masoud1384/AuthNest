using Application.Dto.JWT;
using Application.Dto.User;
using Application.Repositories;
using Application.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace AuthNest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserAuth _userAuth;
        private readonly UserService _userService;
        public UserController(UserAuth userAuth, UserService userService)
        {
            _userAuth = userAuth;
            _userService = userService;
        }

        [HttpPost]
        [Route("SignUp")]
        [AllowAnonymous]
        public async Task<Response<JWTResponse>> SignUpUser([FromBody] UserSignUpDto request)
            => await _userAuth.SignUpUser(request);

        [HttpPut]
        [Route("UpdateUserInfo")]
        [Authorize]
        public async Task<Response<bool>> UpdateUserInfo([FromBody] UpdateUserDto request)
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            request.UserName = !string.IsNullOrEmpty(authHeader) ? 
                JWTHelper.GetClaimFromToken(authHeader, "unique_name") ?? string.Empty : string.Empty;
            return await _userService.UpdateUser(request);
        }

    }
}

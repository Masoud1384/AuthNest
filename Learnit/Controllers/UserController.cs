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
        {
            return await _userAuth.SignUpUser(request);
        }


        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            var result = await _userAuth.LoginUser(request);
            return Ok(new { AccessToken = result.JWT, RefreshToken = result.RefreshToken });
        }

        [HttpPost]
        [Route("RefreshAccessToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshAccessToken([FromBody] UserRefreshAccessTokenDto request)
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            request.AccessToken = authHeader.StartsWith("Bearer ") ? authHeader.Substring("Bearer ".Length) : authHeader;
            var result = await _userAuth.Refresh(request);
            return Ok(new { AccessToken = result.AccessToken });
        }
    }
}

using HRManagement.Services.ServiceInterfaces;
using HRManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountsService _accountsService;
        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationViewModel userInfo)
        {
            return new OkObjectResult(await _accountsService.RegisterUser(userInfo));
        }
        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginViewModel loginInput)
        {
            return new OkObjectResult(await _accountsService.LoginUser(loginInput));
        }
        [HttpPost]
        [Route("CreateNewTokens")]
        public async Task<IActionResult> CreateNewTokensForUser([FromBody]string refreshToken)
        {
            var result = await _accountsService.RefreshAccesToken(refreshToken);
            if (result == null)
            {
                return Unauthorized();
            }
            return new OkObjectResult(result);
        }
    }
}

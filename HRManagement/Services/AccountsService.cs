using AutoMapper;
using HRManagement.CustomExceptions;
using HRManagement.Models;
using HRManagement.Services.ServiceInterfaces;
using HRManagement.Services.TokenService;
using HRManagement.ValidateData;
using HRManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace HRManagement.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _inMemoryCache;
        public AccountsService(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager,
        IMapper mapper, JwtTokenService jwtTokenService, IMemoryCache inMemoryCache)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _inMemoryCache = inMemoryCache;
        }
        public async Task<UserViewModel> RegisterUser(UserRegistrationViewModel userInfo)
        {
            ValidateOnNull<UserRegistrationViewModel>.ValidateDataOnNull(userInfo);

            if (userInfo.Password != userInfo.ConfirmPassword)
            {
                throw new CouldNotRegisterUserException("The password and confirmation password do not match.");
            }

            var user = new UserModel { IdentifyNumber = userInfo.IdentifyNumber, UserName = userInfo.UserName, FirstName = userInfo.FirstName, LastName = userInfo.LastName, BirthDate = userInfo.BirthDate, Email = userInfo.Email };
            var result = await _userManager.CreateAsync(user, userInfo.Password);

            if (result.Succeeded)
            {
                return _mapper.Map<UserViewModel>(await _userManager.FindByEmailAsync(userInfo.Email));
            }
            StringBuilder errorMessage = new StringBuilder();
            foreach (var error in result.Errors.ToList())
            {
                errorMessage.AppendLine(error.Description);
            }
            throw new CouldNotRegisterUserException(errorMessage.ToString());
        }
        public async Task<UserLoginResponseViewModel> LoginUser(UserLoginViewModel loginInput)
        {
            var user = await _userManager.FindByNameAsync(loginInput.UserName);
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginInput.Password, false);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var accessToken = await _jwtTokenService.GenerateJwtAccessToken(user, roles);
                var refreshToken = await _jwtTokenService.GenerateJwtRefreshToken();
                await _userManager.SetAuthenticationTokenAsync(user, "JwtBearer", "Access Token", accessToken);
                var loggedUser = _mapper.Map<UserLoginResponseViewModel>(user);
                loggedUser.AccessToken = accessToken;
                loggedUser.RefreshToken = refreshToken;
                _inMemoryCache.Set(refreshToken, user.Id, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMonths(6)
                });

                return loggedUser;
            }
            if (!result.Succeeded)
            {
                throw new FailedToLoginException("Login Failed Incorrect Credintials");
            }
            return new UserLoginResponseViewModel();
        }
        public async Task<UserLoginResponseViewModel> RefreshAccesToken(string refreshToken)
        {
            if (await _jwtTokenService.ValidateRefreshToken(refreshToken))
            {
                var userId = _inMemoryCache.Get(refreshToken);
                var user = await _userManager.FindByIdAsync(userId.ToString());
                var roles = await _userManager.GetRolesAsync(user);
                var accessToken = await _jwtTokenService.GenerateJwtAccessToken(user, roles);
                var newRefreshToken = await _jwtTokenService.GenerateJwtRefreshToken();

                await _userManager.SetAuthenticationTokenAsync(user, "JwtBearer", "Access Token", accessToken);
                _inMemoryCache.Remove(refreshToken);
                _inMemoryCache.Set(newRefreshToken, user.Id, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMonths(6)
                });
                var loggedUser = _mapper.Map<UserLoginResponseViewModel>(user);
                loggedUser.AccessToken = accessToken;
                loggedUser.RefreshToken = newRefreshToken;
                return loggedUser;
            }
            return new UserLoginResponseViewModel();
        }
    }
}

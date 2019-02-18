using DotNetCore.AspNetCore;
using DotNetCore.Extensions;
using DotNetCore.Objects;
using DotNetCoreArchitecture.Application;
using DotNetCoreArchitecture.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Web
{
    [ApiController]
    [RouteController]
    public class UsersController : ControllerBase
    {
        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        private IUserService UserService { get; }

        [HttpPost]
        public async Task<ActionIDataResult<long>> AddAsync(AddUserModel addUserModel)
        {
            var result = await UserService.AddAsync(addUserModel);

            return new ActionIDataResult<long>(result);
        }

        [HttpDelete("{userId}")]
        public Task<IResult> DeleteAsync(long userId)
        {
            return UserService.DeleteAsync(userId);
        }

        [HttpGet]
        public Task<IEnumerable<UserModel>> ListAsync()
        {
            return UserService.ListAsync();
        }

        [HttpGet("{userId}")]
        public Task<UserModel> SelectAsync(long userId)
        {
            return UserService.SelectAsync(userId);
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<ActionIDataResult<TokenModel>> SignInAsync(SignInModel signInModel)
        {
            var result = await UserService.SignInJwtAsync(signInModel);

            return new ActionIDataResult<TokenModel>(result);
        }

        [HttpPost("SignOut")]
        public Task SignOutAsync()
        {
            var signOutModel = new SignOutModel(User.Id());

            return UserService.SignOutAsync(signOutModel);
        }

        [HttpPut("{userId}")]
        public async Task<ActionIResult> UpdateAsync(long userId, UpdateUserModel updateUserModel)
        {
            updateUserModel.UserId = userId;

            var result = await UserService.UpdateAsync(updateUserModel);

            return new ActionIResult(result);
        }
    }
}

using BlogPlatform.Application.Managers.UserManager.Params;
using BlogPlatform.Application.Managers.UserManager.Resut;
using BlogPlatform.Domain.AgregatesModel.UserAgregate;
using BlogPlatform.Infrastructure.HttpServices.Auth;
using External.Result.Base;
using External.Result.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.UserManager
{
    public class UserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthService _authService;
        public UserManager(IUserRepository userRepository, AuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<UpdateUserResult> UpdateUser(UpdateUserParams updateUserParams)
        {
            var validateErrorResult = ParamsValidator.Validate(updateUserParams);
            if (validateErrorResult != null)
            {
                return new UpdateUserResult(validateErrorResult);
            }
            var result = await _authService.IsValidUser(new External.DTOs.Auth.Models.Request.IsValidRequest() 
            {
                UserEmail = updateUserParams.Email,
                UserId = updateUserParams.AuthResourceUserId
            });

            if (!result.IsSuccess)
            {
                return new UpdateUserResult(new Error(result.Error.ErrorMessages, (ErrorType)result.Error.ErrorType));
            }
            _userRepository.AddOrUpdate(new User() 
            {
                AuthResourceUserId = updateUserParams.AuthResourceUserId,
                Email = updateUserParams.Email,
                FirstName = updateUserParams.FirstName,
                LastName = updateUserParams.LastName,
                Gender = updateUserParams.Gender,
                BirthDate = updateUserParams.BirthDate,
                PhoneNumber = updateUserParams.PhoneNumber
            });
            return new UpdateUserResult();
        }
        
        public IsUserSetupedResult IsUserSetuped(IsUserSetupedParams isUserSetupedParams)
        {
            var validateErrorResult = ParamsValidator.Validate(isUserSetupedParams);
            if (validateErrorResult != null)
            {
                return new IsUserSetupedResult(false, validateErrorResult);
            }
            var user = _userRepository.GetByAuthResourceUserId(isUserSetupedParams.AuthResourceUserId);
            if(user == null)
            {
                return new IsUserSetupedResult(false);
            }
            return new IsUserSetupedResult(true);
        }
    }
}

using BlogPlatform.Application.Managers.UserManager.Params;
using BlogPlatform.Application.Managers.UserManager.Resut;
using BlogPlatform.Domain.AgregatesModel.UserAgregate;
using BlogPlatform.Infrastructure.HttpServices.Auth;
using External.Options.BlogPlatform;
using External.Result.Base;
using External.Result.Helpers;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<AttachmentServiceOptions> _attachmentServiceOptions;
        public UserManager(IUserRepository userRepository, AuthService authService, IOptions<AttachmentServiceOptions> attachmentServiceOptions)
        {
            _userRepository = userRepository;
            _authService = authService;
            _attachmentServiceOptions = attachmentServiceOptions;
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

            var user = _userRepository.GetByAuthResourceUserId(updateUserParams.AuthResourceUserId);
            
            if(user != null)
            {
                user.FirstName = updateUserParams.FirstName;
                user.LastName = updateUserParams.LastName;
                user.Gender = updateUserParams.Gender;
                user.BirthDate = updateUserParams.BirthDate;
                user.PhoneNumber = updateUserParams.PhoneNumber;
                _userRepository.AddOrUpdate(user);
                return new UpdateUserResult();
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
        public GetUserResult GetUser(GetUserParams getUserParams)
        {
            var validateErrorResult = ParamsValidator.Validate(getUserParams);
            if (validateErrorResult != null)
            {
                return new GetUserResult(null, validateErrorResult);
            }
            var user = _userRepository.GetByAuthResourceUserId(getUserParams.AuthResourceUserId);
            if (user == null)
            {
                return new GetUserResult(null, new Error($"User does not exist.", ErrorType.Validation));
            }

            return new GetUserResult(new Resut.Childs.User() 
            {
                AuthResourceUserId = user.AuthResourceUserId,
                BirthDate = user.BirthDate,
                Blog = user.Blog,
                BlogId = user.BlogId,
                Email = user.Email,
                FirstName = user.FirstName,
                Gender = user.Gender,
                Id = user.Id,
                ImageName = user.ImageName,
                ImageUrl = _attachmentServiceOptions.Value.AttachmentUrlPath + user.ImageName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                IsVerified = user.IsVerified
            });
        }

        public SaveUserImageResult SaveUserImage(SaveUserImageParams saveUserImageParams)
        {
            var validateErrorResult = ParamsValidator.Validate(saveUserImageParams);
            if (validateErrorResult != null)
            {
                return new SaveUserImageResult(validateErrorResult);
            }
            var _blogUser = _userRepository.GetByAuthResourceUserId(saveUserImageParams.AuthResourceUserId);
            if(_blogUser == null)
            {
                _userRepository.AddOrUpdate(new User()
                {
                    AuthResourceUserId = saveUserImageParams.AuthResourceUserId,
                    Email = saveUserImageParams.Email,
                    ImageName = saveUserImageParams.ImageName
                });
                return new SaveUserImageResult();
            }
            _blogUser.ImageName = saveUserImageParams.ImageName;
            _userRepository.AddOrUpdate(_blogUser);
            return new SaveUserImageResult();
        }

    }
}

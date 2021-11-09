﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Apects.Caching;
using Core.Apects.Validation;

using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService,ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
        
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetUserByMail(userForLoginDto.Email);
            if (userToCheck.Data==null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.Data.PasswordHash,userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            if (userToCheck.Data.Status==false)
            {
                return new ErrorDataResult<User>(Messages.UserStatusIsInactive);
            }
            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }
        [ValidationAspect(typeof(UserForRegisterDtoValidator), Priority = 1)]
        [CacheRemoveAspect("IUserService.Get")]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            var result = UserExist(userForRegisterDto.Email);
            if (result.Success==false)
            {
                return new ErrorDataResult<User>(result.Message);
            }
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CompanyName = userForRegisterDto.CompanyName,
                UserType = userForRegisterDto.UserType,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExist(string email)
        {
            var result = _userService.GetUserByMail(email);
            if (result.Data!=null)
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }
            return new SuccessResult();
        }
        public IResult ChangePassword(UserForChangePasswordDto updatedUser)
        {
            UserForLoginDto checkedUser = new UserForLoginDto
            {
                Email = updatedUser.Email,
                Password = updatedUser.OldPassword
            };
            var loginResult = Login(checkedUser);
            if (loginResult.Success)
            {
                var user = loginResult.Data;
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(updatedUser.NewPassword, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _userService.Update(user);
                return new SuccessResult(Messages.PasswordChanged);
            }

            return new ErrorResult(loginResult.Message);
        }
    }
}

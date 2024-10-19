using DynamicFormBuilder.Application.Common;
using DynamicFormBuilder.Application.DTO;
using DynamicFormBuilder.Application.IRepositories;
using DynamicFormBuilder.Application.IServices;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DynamicFormBuilder.Domain.Entities;

namespace DynamicFormBuilder.Application.Servics
{
    public class UserService : IUserService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;

        public UserService(IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }


        public async Task<IResponseResult<RegisterResponseModel>> SignUp(RegisterRequestModel registerRequest)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registerRequest.Email))
                {
                    return new ResponseResult<RegisterResponseModel>
                    {
                        Status = ResultStatus.Failed,
                        Errors = new List<string> { "Email is required" }
                    };
                }

                if (!IsValidEmail(registerRequest.Email))
                {
                    return new ResponseResult<RegisterResponseModel>
                    {
                        Status = ResultStatus.Failed,
                        Errors = new List<string> { "Invalid email format" }
                    };
                }

                if (string.IsNullOrWhiteSpace(registerRequest.Password) || registerRequest.Password.Length < 6)
                {
                    return new ResponseResult<RegisterResponseModel>
                    {
                        Status = ResultStatus.Failed,
                        Errors = new List<string> { "Password must be at least 6 characters long" }
                    };
                }

                if (registerRequest.Password != registerRequest.ConfirmPassword)
                {
                    return new ResponseResult<RegisterResponseModel>
                    {
                        Status = ResultStatus.Failed,
                        Errors = new List<string> { "Password and Retyped Password do not match" }
                    };
                }

                var _alreadyExsist = _repositoryUnitOfWork.Users.Find(x => x.Email.Equals(registerRequest.Email)).ToList();
                if (_alreadyExsist.Any())
                {
                    return new ResponseResult<RegisterResponseModel>
                    {
                        Status = ResultStatus.Failed,
                        Errors = new List<string> { "The email already exists" }
                    };
                }

                var user = new User()
                {
                    CreatedBy = registerRequest.CreatedBy,
                    Email = registerRequest.Email,
                    CreatedDate = DateTime.Now,
                    Password = HashPassword(registerRequest.Password),
                };

                await _repositoryUnitOfWork.Users.AddAsync(user);

                return new ResponseResult<RegisterResponseModel>
                {
                    Status = ResultStatus.Success,
                    Data = new RegisterResponseModel() { Id = user.Id, Email = user.Email }
                };
            }
            catch (Exception ex)
            {
                return new ResponseResult<RegisterResponseModel>
                {
                    Status = ResultStatus.Failed,
                    Errors = new List<string> { ex.Message ?? "An error occurred while adding the SignUp" },
                    Data = null
                };
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Encoding.UTF8.GetString(bytes);
            }
        }



        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }

        public async Task<IResponseResult<SignInResponseModel>> SignIn(SignInRequestModel registerRequest)
        {
            try
            {


                var user = _repositoryUnitOfWork.Users.Find(u => u.Email == registerRequest.Email).FirstOrDefault();

                if (user == null)
                {
                    // User with this email does not exist
                    return new ResponseResult<SignInResponseModel>
                    {
                        Status = ResultStatus.Failed,
                        Errors = new List<string> { "Email not found." },
                        Data = null
                    };
                }

                // Step 2: Verify the password (assuming passwords are hashed)
                if (!VerifyPassword(registerRequest.Password, user.Password))
                {
                    // Password is incorrect
                    return new ResponseResult<SignInResponseModel>
                    {
                        Status = ResultStatus.Failed,
                        Errors = new List<string> { "Incorrect password." },
                        Data = null
                    };
                }

                // Step 3: If email and password match, create the response
                var signInResponse = new SignInResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    CreatedBy = user.CreatedBy,
                };

                return new ResponseResult<SignInResponseModel>
                {
                    Status = ResultStatus.Success,
                    Data = signInResponse
                };
            }
            catch (Exception ex)
            {
                return new ResponseResult<SignInResponseModel>
                {
                    Status = ResultStatus.Failed,
                    Errors = new List<string> { ex.Message ?? "An error occurred while adding the SignIn" },
                    Data = null
                };
            }
        }


    }
}

using Google.Apis.Auth;
using PerfumeStore.Repository;
using PerfumeStore.Repository.Model;
using PerfumeStore.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.Service
{
    public class AuthService
    {
        private readonly UnitOfWork _unitOfWork;

        public AuthService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User GetUserByEmail(string email)
        {
            // Fetch a perfume by the 'Name' field using GetByCondition
            return _unitOfWork.Users.GetByCondition(p => p.Email == email);
        }

        public User HandleGoogleLogin(GoogleJsonWebSignature.Payload payload)
        {
            // Check if the user already exists in the database
            var user = _unitOfWork.Users.GetByCondition(u => u.Email == payload.Email);

            if (user != null)
            {
                // User exists, update last login time
                user.LastLogin = DateTime.Now;
                _unitOfWork.Users.Update(user);
            }
            else
            {
                // User doesn't exist, create a new one
                user = new User
                {
                    Email = payload.Email,
                    FirstName = payload.Name,
                    LastLogin = DateTime.Now
                };

                _unitOfWork.Users.Create(user);
            }

            _unitOfWork.Save();  // Save changes to the database
            return user;
        }

        public async Task<BusinessModel.UserAuthModel> AuthenticateGoogleUser(string email, string name)
        {
            var user = _unitOfWork.Users.GetByCondition(u => u.Email == email);

            if (user == null)
            {
                user = new Repository.Model.User
                {
                    UserId = Guid.NewGuid(),
                    Email = email,
                    FirstName = name,
                    LastName = "",
                    PasswordHash = "",
                    Role = "Customer",
                    Metadata = "",
                    Phone = "",
                    ProfileUrl = "",
                    Status = "isActive",
                    DateCreated = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    LastLogin = DateTime.UtcNow
                };

                _unitOfWork.Users.Create(user);
            }
            else
            {
                user.LastLogin = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;
                user.FirstName = name;
                user.LastName = "";
                _unitOfWork.Users.Update(user);
            }

            await _unitOfWork.SaveAsync();

            return new UserAuthModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                LastLoginAt = (DateTime)user.LastLogin
            };
        }
    }
}

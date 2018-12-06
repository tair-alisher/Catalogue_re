using Catalogue_re.BLL.DTO;
using Catalogue_re.BLL.Infrastructure;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.DAL.Entities;
using Catalogue_re.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Catalogue_re.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork { get; set; }

        public UserService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDTO)
        {
            ApplicationUser user = await _unitOfWork.UserManager.FindByNameAsync(userDTO.UserName);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDTO.Email, UserName = userDTO.UserName };
                var result = await _unitOfWork.UserManager.CreateAsync(user, userDTO.Password);
                if (result.Errors.Count() > 0)
                {
                    if (result.Errors.Contains($"Name {user.UserName} is already taken."))
                        return new OperationDetails(false, "Пользователь с таким логином уже существует.", "UserName");
                    else if (result.Errors.Any(x => x.Contains("Password")))
                        return new OperationDetails(false, "Ненадежный пароль", "Password");

                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                await _unitOfWork.UserManager.AddToRoleAsync(user.Id, userDTO.Role);
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true, "Регистрация пройдена успшено.", "");
            }
            else
                return new OperationDetails(false, "Пользователь с таким логином уже существует.", "UserName");
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDTO)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await _unitOfWork.UserManager.FindAsync(userDTO.UserName, userDTO.Password);
            if (user != null)
                claim = await _unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task<OperationDetails> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            ApplicationUser user = await _unitOfWork.UserManager.FindByIdAsync(changePasswordDTO.UserId);
            var oldPasswordConfirmation = await _unitOfWork.UserManager.CheckPasswordAsync(user, changePasswordDTO.OldPassword);
            if (!oldPasswordConfirmation)
                return new OperationDetails(false, "Старый пароль неверен.", "OldPassword");

            IdentityResult result = await _unitOfWork.UserManager.ChangePasswordAsync(
                changePasswordDTO.UserId,
                changePasswordDTO.OldPassword,
                changePasswordDTO.NewPassword);

            if (result.Errors.Count() > 0)
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

            await _unitOfWork.SaveAsync();

            return new OperationDetails(true, "Вы смениил пароль.", "");
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}

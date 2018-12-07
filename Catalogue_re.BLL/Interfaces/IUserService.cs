using Catalogue_re.BLL.DTO;
using Catalogue_re.BLL.Infrastructure;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Catalogue_re.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDTO);
        Task<ClaimsIdentity> Authenticate(UserDTO userDTO);
        Task<OperationDetails> ChangePassword(ChangePasswordDTO changePasswordDTO);
    }
}

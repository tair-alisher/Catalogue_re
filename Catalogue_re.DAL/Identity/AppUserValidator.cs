using Catalogue_re.DAL.Entities;
using Microsoft.AspNet.Identity;
namespace Catalogue_re.DAL.Identity
{
    public class AppUserValidator : UserValidator<ApplicationUser>
    {
        private readonly ApplicationUserManager _userManager;
        public AppUserValidator(ApplicationUserManager manager) : base(manager)
        {
            AllowOnlyAlphanumericUserNames = false;
            _userManager = manager;
        }
    }
}

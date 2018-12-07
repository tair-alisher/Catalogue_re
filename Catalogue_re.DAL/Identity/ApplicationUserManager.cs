using Catalogue_re.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace Catalogue_re.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store) { }
    }
}

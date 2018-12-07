using Catalogue_re.BLL.DTO;
using Catalogue_re.BLL.Infrastructure;
using Catalogue_re.BLL.Interfaces;
using Catalogue_re.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Catalogue_re.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDTO = new UserDTO { UserName = model.UserName, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDTO);
                if (claim == null)
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
        
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDTO = new UserDTO
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    Role = "admin"
                };
                OperationDetails result = await UserService.Create(userDTO);
                if (result.Success)
                    return RedirectToAction("Login");
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }

            return View(model);
        }

        [Authorize(Roles = "manager, admin")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager, admin")]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO
                {
                    UserId = User.Identity.GetUserId(),
                    OldPassword = model.OldPassword,
                    NewPassword = model.NewPassword
                };
                OperationDetails result = await UserService.ChangePassword(changePasswordDTO);
                if (result.Success)
                    return RedirectToRoute(new
                    {
                        controller = "Home",
                        action = "Index"
                    });
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }

            return View(model);
        }
    }
}
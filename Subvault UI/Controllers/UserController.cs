using Subvault_Domain.Exceptions;
using Subvault_UI.BusinessLogic;
using Subvault_UI.Models;
using System.Web.Mvc;

namespace Subvault_UI.Controllers {

    public class UserController : Controller {

        private UserManager userManager;

        public UserController(UserManager userManager) {
            this.userManager = userManager;
        }

        public void Login(LoginViewModel login) {
            TempData["Login"] = login;

            try {
                if (!userManager.AuthenticateUser(login.Username, login.Password)) {
                    TempData["UsernameOrPasswordIncorrect"] = true;
                } else {
                    Session["Login"] = userManager.GetUserSession(login.Username);
                }
            } catch (UserDoesNotExistException udnee) {
                TempData["UserDoesNotExist"] = udnee.Message;
            }

            Response.Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ViewResult Register() {
            return View();
        }

        [HttpPost]
        public ViewResult Register(RegisteringUser registeringUser) {
            if (ModelState.IsValid) {
                try {
                    userManager.RegisterUser(registeringUser);
                } catch (UserAlreadyExistsException uaee) {
                    ViewBag.UserAlreadyExists = uaee.Message;
                    return View();
                } catch (PasswordNotEqualToPasswordRepeatException pnetpre) {
                    ViewBag.PasswordRepeatNotTheSame = pnetpre.Message;
                    return View();
                }

                return View("ThanksForRegistering");
            } else {
                return View();
            }
        }
    }
}
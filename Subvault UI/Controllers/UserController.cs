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

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Catches the login request and redirects back to the page the request came from
        /// </summary>
        /// <param name="login">Model containing the username and password</param>
        public void Login(LoginViewModel login) {
            TempData["Login"] = login;

            try {
                //If the password is correct
                if (!userManager.AuthenticateUser(login.Username, login.Password)) {
                    TempData["UsernameOrPasswordIncorrect"] = true;
                } else {
                    //Set login session
                    Session["Login"] = userManager.GetUserSession(login.Username);
                }
            } catch (UserDoesNotExistException udnee) {
                TempData["UserDoesNotExist"] = udnee.Message;
            }

            Response.Redirect(Request.UrlReferrer.ToString());
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Logs out the user and redirects the user to the homepage
        /// </summary>
        public void Logout() {
            //Set login session to null
            Session["Login"] = null;

            //Return to home
            Response.Redirect("/");
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Returns the view to register a user
        /// </summary>
        /// <returns>The view</returns>
        [HttpGet]
        public ViewResult Register() {
            return View();
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Catches the user register request
        /// </summary>
        /// <param name="registeringUser">The user to be registered</param>
        /// <returns>The ThanksForRegistering view if everything is correct. Otherwise it will return to the register page.</returns>
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
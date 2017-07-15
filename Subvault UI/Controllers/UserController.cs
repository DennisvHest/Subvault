using Subvault_Domain;
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
            Logger.Log.InfoFormat(Logger.Format + "Login request for user: " + login.Username, GetType().ToString());

            TempData["Login"] = login;

            try {
                //If the password is correct
                if (!userManager.AuthenticateUser(login.Username, login.Password)) {
                    Logger.Log.InfoFormat(Logger.Format + "Login request for user: " + login.Username + " failed, because username or password was incorrect", GetType().ToString());
                    TempData["UsernameOrPasswordIncorrect"] = true;
                } else {
                    //Set login session
                    Logger.Log.InfoFormat(Logger.Format + "Login request for user: " + login.Username + " was successful", GetType().ToString());
                    Session["Login"] = userManager.GetUserSession(login.Username);
                }
            } catch (UserDoesNotExistException udnee) {
                Logger.Log.InfoFormat(Logger.Format + "Login request for user: " + login.Username + " failed, because a user with that username does not exist", GetType().ToString());
                TempData["UserDoesNotExist"] = udnee.Message;
            }

            Response.Redirect(Request.UrlReferrer.ToString());
        }

        /// <author>Dennis van Hest</author>
        /// <summary>
        /// Logs out the user and redirects the user to the homepage
        /// </summary>
        public void Logout() {
            Logger.Log.InfoFormat(Logger.Format + "Logout request for user: " + ((UserSessionViewModel)Session["Login"]).Username, GetType().ToString());

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
            Logger.Log.InfoFormat(Logger.Format + "GET request for Register", GetType().ToString());
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
            Logger.Log.InfoFormat(Logger.Format + "POST request for Register for user: " + registeringUser.ToString(), GetType().ToString());

            if (ModelState.IsValid) {
                try {
                    userManager.RegisterUser(registeringUser);
                } catch (UserAlreadyExistsException uaee) {
                    Logger.Log.InfoFormat(Logger.Format + "POST request for Register for user: " + registeringUser.ToString() + " failed, because the user with that username already exists", GetType().ToString());
                    ViewBag.UserAlreadyExists = uaee.Message;
                    return View();
                } catch (PasswordNotEqualToPasswordRepeatException pnetpre) {
                    Logger.Log.InfoFormat(Logger.Format + "POST request for Register for user: " + registeringUser.ToString() + " failed, because the password did not match the password repeat", GetType().ToString());
                    ViewBag.PasswordRepeatNotTheSame = pnetpre.Message;
                    return View();
                }

                Logger.Log.InfoFormat(Logger.Format + "POST request for Register for user: " + registeringUser.ToString() + " was successful", GetType().ToString());

                return View("ThanksForRegistering");
            } else {
                return View();
            }
        }
    }
}
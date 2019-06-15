//-----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace FundooApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using BussinessLayer.Interfaces;
    using Common.Models;
    using FundooNote.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Application User Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]    
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// The application user
        /// </summary>
        private readonly IApplicationControl applicationUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="applicationUser">The application user.</param>
        public AccountController(IApplicationControl applicationUser)
        {
            this.applicationUser = applicationUser;
        }

        /// <summary>
        /// Registers the specified application user model.
        /// </summary>
        /// <param name="applicationUserModel">The application user model.</param>
        /// <returns>return result</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(ApplicationUserModel applicationUserModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this.applicationUser.UserRegisterAsync(applicationUserModel);
                    return this.Ok(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return this.BadRequest();
        }

        /// <summary>
        ///  Logins the specified application login model.
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns>returns status code</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(ApplicationLoginModel model)
         {
            try  
            {
                var result = await this.applicationUser.LoginAsync(model);
                if (result.Success == true)
                {
                    return this.Ok(new { result });
                }

                    return this.BadRequest();
            }
            catch (Exception e)
            {
               throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Faces the book login.
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns>returns status</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("fblogin")]
        public async Task<IActionResult> FaceBookLogin(SocialModel model)
        {
            var result = await this.applicationUser.FaceBookLoginAsync(model);
            if (result != null)
            {
                return this.Ok(new { result });
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Forgot the specified forgot password model.
        /// </summary>
        /// <param name="forgotPasswordModel">The forgot password model.</param>
        /// <returns>return status code</returns>
        [HttpPost]
        [Route("forgotPassword")]
        public IActionResult Forgot(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                var result = this.applicationUser.ForgotPasswordAsync(forgotPasswordModel);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Resets the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return status code</returns>
        [HttpPost]
        [Route("resetPassword")]
        public async Task<bool> Reset(ResetPasswordModel model)
        {
            try
            {
                var result = await this.applicationUser.ResetPasswordAsync(model);
                if (result)
                {
                    return result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Profiles the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userid">The user id.</param>
        /// <returns>returns result</returns>
        [HttpPost]
        [Route("profile/{userid}")]
        public IActionResult Profile(IFormFile file, string userid)
        {
            Console.WriteLine(file);
            if (file == null)
            {
                return this.NotFound("The file couldn't be found");
            }

            var result = this.applicationUser.ProfilePicture(file, userid);

            return this.Ok(new { result });
            }

        /// <summary>
        /// Profiles the URL.
        /// </summary>
        /// <param name="userid">The user id.</param>
        /// <returns>returns response</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("url/{userid}")]
        public IActionResult ProfileUrl(string userid)
        {
            try
            {
                var result = this.applicationUser.ProfileUrl(userid);
                return this.Ok(new { result });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        /// <summary>
        /// API for push notification
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>returns status</returns>
        [HttpPost]
        [Route("notification")]
        [AllowAnonymous]
        public async Task<IActionResult> PushNotification(NotificationModel notification)
        {
            try
            { 
                var token = await this.applicationUser.GetToken(notification);
                return this.Ok(new { token });
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
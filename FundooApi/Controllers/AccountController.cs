//-----------------------------------------------------------------------
// <copyright file="ApplicationUserController.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace FundooApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using BussinessLayer.Interfaces;
    using FundooNote.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Application User Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// The application user
        /// </summary>
        private readonly IApplicationControl applicationUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserController"/> class.
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
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(ApplicationUserModel applicationUserModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this.applicationUser.PostApplicationUserAsync(applicationUserModel);
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
        /// Logins the specified application login model.
        /// </summary>
        /// <param name="applicationLoginModel">The application login model.</param>
        /// <returns>return status code</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(ApplicationLoginModel applicationLoginModel)
        {
            try  
            {
                var result = await this.applicationUser.LoginAsync(applicationLoginModel);
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
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
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
        [HttpGet]
        [Route("url/{userid}")]
        public Task<string> ProfileUrl(string userid)
        {
            var result = this.applicationUser.ProfileUrl(userid);
            return result;
        }
    }
}
//-----------------------------------------------------------------------
// <copyright file="ApplicationUserServices.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------

namespace BussinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using BussinessLayer.Interfaces;
    using Common.Models;
    using FundooNote.Interfaces;
    using FundooNote.Models;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Interface;

    /// <summary>
    /// Application User Service
    /// </summary>
    /// <seealso cref="BussinessLayer.Interfaces.IApplicationControl" />
    public class ApplicationUserServices : IApplicationControl
    {
        /// <summary>
        /// The application repository
        /// </summary>
        private readonly IRepository applicationRepository;

        /// <summary>
        /// The email sender
        /// </summary>
        private readonly IEmailSender emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserServices"/> class.
        /// </summary>
        /// <param name="applicationRepository">The application repository.</param>
        /// <param name="emailSender">The email sender.</param>
        public ApplicationUserServices(IRepository applicationRepository, IEmailSender emailSender)
        {
            this.applicationRepository = applicationRepository;
            this.emailSender = emailSender;
        }

        /// <summary>
        /// Posts the application user asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return boolean</returns>
        public async Task<bool> UserRegisterAsync(ApplicationUserModel model)
        {
            //// Method to call the repository
            await this.applicationRepository.CreateAsync(model);
            return true;
        }

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return string
        /// </returns>
        public async Task<dynamic> LoginAsync(ApplicationLoginModel model)
        {
            //// Method to call the repository
            return await this.applicationRepository.LoginPage(model);
        }

        /// <summary>
        /// Faces the book login asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// returns dynamic
        /// </returns>
        public Task<dynamic> FaceBookLoginAsync(SocialModel model)
        {
            //// Method to call the repository
            return this.applicationRepository.FaceBookLoginAsync(model);
        }

        /// <summary>
        /// Forgot the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return boolean</returns>
        public bool ForgotPasswordAsync(ForgotPasswordModel model)
        {
            //// Verify the user by its email
            var result = this.applicationRepository.FindByEmailAsync(model);
            if (result != null)
            {
                var code = this.applicationRepository.GeneratePasswordResetTokenAsync(model);
                var callbackUrl = "http://localhost:4200/reset-password?code=" + code;
                this.emailSender.SendEmailAsync(model.Username, "Reset Password", $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return boolean</returns>
        public async Task<bool> ResetPasswordAsync(ResetPasswordModel model)
        {
            //// Verify the user to reset the password
            var result = await this.applicationRepository.ResetPasswordAsync(model);
            return result;
        }

        /// <summary>
        /// Profiles the picture.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userid">The identifier.</param>
        /// <returns>
        /// returns string
        /// </returns>
        public string ProfilePicture(IFormFile file, string userid)
        {
            //// Method to call the repository
            var result = this.applicationRepository.Image(file, userid);
            return result;
        }

        /// <summary>
        /// Profiles the URL.
        /// </summary>
        /// <param name="userid">The user id.</param>
        /// <returns>
        /// returns list
        /// </returns>
        public IList<ApplicationUser> ProfileUrl(string userid)
        {
            //// Method to call the repository for ProfileUrl
            var result = this.applicationRepository.ProfileUrl(userid);
            return result;
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>
        /// returns string
        /// </returns>
        public async Task<int> GetToken(NotificationModel notification)
        {
            //// Method to pass the token for notification
            var result = await this.applicationRepository.PassToken(notification);
            ////using (var client = new HttpClient())
            ////{
            ////    client.BaseAddress = new Uri("https://fcm.googleapis.com/");
            ////    client.DefaultRequestHeaders.Accept.Clear();
            ////    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ////    HTTP GET
            ////    HttpResponseMessage response = await client.GetAsync("fcm/send");
            ////}
            return result;
        }
    }
}
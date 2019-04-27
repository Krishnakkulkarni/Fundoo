//-----------------------------------------------------------------------
// <copyright file="ApplicationUserServices.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------

namespace BussinessLayer.Services
{
    using System;
    using System.Threading.Tasks;
    using BussinessLayer.Interfaces;
    using FundooNote.Interfaces;
    using FundooNote.Models;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Options;
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
        /// <param name="appSettings">The application settings.</param>
        /// <param name="emailSender">The email sender.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public ApplicationUserServices(IRepository applicationRepository, IEmailSender emailSender, IDistributedCache distributedCache)
        {
            this.applicationRepository = applicationRepository;
            this.emailSender = emailSender;
        }

        /// <summary>
        /// Posts the application user asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return boolean</returns>
        public async Task<bool> PostApplicationUserAsync(ApplicationUserModel model)
        {
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
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> LoginAsync(ApplicationLoginModel model)
        {
            string result = await this.applicationRepository.LoginPage(model);
            return result;
        }

        /// <summary>
        /// Forgot the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return boolean</returns>
        public bool ForgotPasswordAsync(ForgotPasswordModel model)
        {
            var result = this.applicationRepository.FindByEmailAsync(model);
            if (result != null)
            {
                var code = this.applicationRepository.GeneratePasswordResetTokenAsync(model);
                var callbackUrl = "http://localhost:4200/reset-password?code=" + code;
                this.emailSender.SendEmailAsync(model.Email, "Reset Password", $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
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
           var result = await this.applicationRepository.ResetPasswordAsync(model);
            return result;
        }
    }
}
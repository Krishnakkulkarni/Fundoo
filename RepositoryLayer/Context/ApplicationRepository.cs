//-----------------------------------------------------------------------
// <copyright file="ApplicationRepository.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace RepositoryLayer.Context
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using FundooNote.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Interface;
   
    /// <summary>
    /// Application Repository class
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.IRepository" />
    public class ApplicationRepository : IRepository
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> usermanager;

        /// <summary>
        /// The application settings
        /// </summary>
        private readonly AppSetting appSettings;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRepository"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public ApplicationRepository(UserManager<ApplicationUser> userManager, IOptions<AppSetting> appSettings, IDistributedCache distributedCache)
        {
            this.usermanager = userManager;
            this.appSettings = appSettings.Value;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Checks the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return boolean
        /// </returns>
        public async Task<bool> CheckPasswordAsync(ApplicationLoginModel model)
        {
            var result = await this.usermanager.FindByNameAsync(model.UserName);
            var user = await this.usermanager.CheckPasswordAsync(result, model.Password);
            return user;
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="applicationUserModel">The application user model.</param>
        /// <returns>
        /// return Task
        /// </returns>
        public Task CreateAsync(ApplicationUserModel applicationUserModel)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = applicationUserModel.UserName,
                Email = applicationUserModel.Email,
                FirstName = applicationUserModel.FirstName,
                LastName = applicationUserModel.LastName
            };
            var result = this.usermanager.CreateAsync(user, applicationUserModel.Password);
            return result;
        }
         
        /// <summary>
        /// Finds the by name asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return Task
        /// </returns>
        public Task FindByNameAsync(ApplicationLoginModel model)
        {
            var result = this.usermanager.FindByNameAsync(model.UserName);
            return result;
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return string
        /// </returns>
        public async Task<string> LoginPage(ApplicationLoginModel model)
        {
            var user = await this.usermanager.FindByNameAsync(model.UserName);
            if (user != null && await this.usermanager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {new Claim ("UserID", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.JWT_Secrete)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                var cacheKey = token;
                this.distributedCache.GetString(cacheKey);
                this.distributedCache.SetString(cacheKey, token);
                return token;
            }

            return "invalid user";
        }

        /// <summary>
        /// Finds the by email asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return Task
        /// </returns>
        public Task FindByEmailAsync(ForgotPasswordModel model)
        {
            var result = this.usermanager.FindByEmailAsync(model.Email);
            return result;
        }

        /// <summary>
        /// Generates the password reset token asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return string
        /// </returns>
        public async Task<string> GeneratePasswordResetTokenAsync(ForgotPasswordModel model)
        {
            var result = await this.usermanager.FindByEmailAsync(model.Email);
            var user = await this.usermanager.GenerateEmailConfirmationTokenAsync(result);
            return user;
        }

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// return object
        /// </returns>
        public async Task<bool> ResetPasswordAsync(ResetPasswordModel model)
        {
            var userEmail = await this.usermanager.FindByEmailAsync(model.Email);
            if (userEmail == null)
            {
                return false;
            }
            else
            { 
                var token = await this.usermanager.GeneratePasswordResetTokenAsync(userEmail);
                var result = await this.usermanager.ResetPasswordAsync(userEmail, token, model.Password);
                if (result.Succeeded)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

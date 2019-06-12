//-----------------------------------------------------------------------
// <copyright file="ApplicationRepository.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace RepositoryLayer.Context
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Common.Models;
    using FundooNote.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Interface;
    using ServiceStack.Redis;

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
        /// The authentication
        /// </summary>
        private readonly Authentication authentication;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRepository"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public ApplicationRepository(UserManager<ApplicationUser> userManager, IOptions<AppSetting> appSettings, Authentication authentication)
        {
            this.usermanager = userManager;
            this.appSettings = appSettings.Value;
            this.authentication = authentication;
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
        public async Task<dynamic> LoginPage(ApplicationLoginModel model)
        {
            var user = await this.usermanager.FindByNameAsync(model.UserName);
            if (user != null && await this.usermanager.CheckPasswordAsync(user, model.Password))
            {
                ////using(var redis = new RedisClient())
                ////{
                ////    if(redis.Get(model.UserName)==null)
                ////   {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim("UserID", user.Id.ToString()) }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.JWT_Secrete)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                model.Success = true;
                model.Token = token;
                model.Userid = user.Id;
                return model;
                ////        if(tokenDescriptor != null)
                ////        {
                ////            redis.Set(model.UserName, tokenDescriptor);
                ////        }
                ////        return token;
                ////    }
                ////    else
                ////    {
                ////        var redis1 = redis.Get<ApplicationUserModel>(model.UserName);
                ////        return redis1.ToString();
                ////    }
                ////}
            }
            else
            {
                model.Success = false;
                return model;
            }
        }

        /// <summary>
        /// Faces the book login asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>returns response</returns>
        public async Task<dynamic> FaceBookLoginAsync(SocialModel model)
        {
            var user = await this.usermanager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ApplicationUser socialUser = new ApplicationUser()
                {
                    UserName = model.UserName
                };
                 var socialuser = this.usermanager.CreateAsync(socialUser);
                
                 var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]{ new Claim("UserID", socialuser.Id.ToString())}),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.JWT_Secrete)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return token;
            }
            else
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]{ new Claim("UserID", user.Id.ToString())}),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.JWT_Secrete)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return token;
            }
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
            var result = this.usermanager.FindByEmailAsync(model.Username);
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
            var result = await this.usermanager.FindByEmailAsync(model.Username);
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

        /// <summary>
        /// Images the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userid">The identifier.</param>
        /// <returns>return string</returns>
        public string Image(IFormFile file, string userid)
        {
            try
            {
                var stream = file.OpenReadStream();
                var name = file.FileName;
                Account account = new Account("db4wyl94g", "645173152293519", "hBF7yF3HzJGByBvdWnzfR_kegmI");
                Cloudinary cloudinary = new Cloudinary(account);
                var imageUploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream)
                };
                var uploadResult = cloudinary.Upload(imageUploadParams);
                var data = this.authentication.ApplicationUsers.Where(t => t.Id == userid).FirstOrDefault();
                data.Profile = uploadResult.Uri.ToString();

                this.authentication.Update(data);
                this.authentication.SaveChanges();

                return data.Profile;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public IList<ApplicationUser> ProfileUrl(string userid)
        {
            //var list = new List<ApplicationUser>();
            var note = from notes in this.authentication.ApplicationUsers where (notes.Id == userid) select notes;
           
            return note.ToArray();
        }
        /// <summary>
        /// Profiles the URL.
        /// </summary>
        /// <param name="userid">The user id.</param>
        /// <returns>
        /// returns response
        /// </returns>
        //public async Ilist<ApplicationUserModel> ProfileUrl(string userid)
        //{
        //    var data = await this.usermanager.FindByIdAsync(userid);
        //    return data.Profile.ToString();
        //}
    }
}

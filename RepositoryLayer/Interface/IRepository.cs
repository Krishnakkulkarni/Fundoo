//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Models;
    using FundooNote.Models;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Repository Interface
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="applicationUserModel">The application user model.</param>
        /// <returns>return Task</returns>
        Task CreateAsync(ApplicationUserModel applicationUserModel);

        /// <summary>
        /// Finds the by name asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return Task</returns>
        Task FindByNameAsync(ApplicationLoginModel model);

        /// <summary>
        /// Checks the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return boolean</returns>
        Task<bool> CheckPasswordAsync(ApplicationLoginModel model);

        /// <summary>
        /// Faces the book login asynchronous.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <returns></returns>
        Task<dynamic> FaceBookLoginAsync(SocialModel Model);

        /// <summary>
        /// Finds the by email asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return Task</returns>
        Task FindByEmailAsync(ForgotPasswordModel model);

        /// <summary>
        /// Generates the password reset token asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return string</returns>
        Task<string> GeneratePasswordResetTokenAsync(ForgotPasswordModel model);

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return object</returns>
        Task<bool> ResetPasswordAsync(ResetPasswordModel model);

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return string</returns>
        Task<dynamic> LoginPage(ApplicationLoginModel model);

        /// <summary>
        /// Images the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userid">The identifier.</param>
        /// <returns>returns string</returns>
        string Image(IFormFile file, string userid);

        IList<ApplicationUser> ProfileUrl(string userid);
        
        Task<string> PassToken(NotificationModel notification);
    }
}
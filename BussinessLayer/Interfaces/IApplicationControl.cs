//-----------------------------------------------------------------------
// <copyright file="IApplicationControl.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace BussinessLayer.Interfaces
{
    using System.Threading.Tasks;
    using FundooNote.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Application Control Interface
    /// </summary>
    public interface IApplicationControl
    {
        /// <summary>
        /// Posts the application user asynchronous.
        /// </summary>
        /// <param name="applicationUserModel">The application user model.</param>
        /// <returns>return boolean</returns>
        Task<bool> PostApplicationUserAsync(ApplicationUserModel applicationUserModel);

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return string</returns>
        Task<dynamic> LoginAsync(ApplicationLoginModel model);

        /// <summary>
        /// Forgot the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return boolean</returns>
        bool ForgotPasswordAsync(ForgotPasswordModel model);

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return boolean</returns>
        Task<bool> ResetPasswordAsync(ResetPasswordModel model);

        /// <summary>
        /// Profiles the picture.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userid">The identifier.</param>
        /// <returns>returns string</returns>
        string ProfilePicture(IFormFile file, string userid);

        /// <summary>
        /// Profiles the URL.
        /// </summary>
        /// <param name="userid">The user id.</param>
        /// <returns>returns response</returns>
        Task<string> ProfileUrl(string userid);
    }
}

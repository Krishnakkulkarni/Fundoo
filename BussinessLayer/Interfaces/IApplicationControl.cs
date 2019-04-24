//-----------------------------------------------------------------------
// <copyright file="IApplicationControl.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace BussinessLayer.Interfaces
{
    using System.Threading.Tasks;
    using FundooNote.Models;

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
        Task<string> LoginAsync(ApplicationLoginModel model);

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
        bool ResetPasswordAsync(ResetPasswordModel model);
    }
}

//-----------------------------------------------------------------------
// <copyright file="IEmailSender.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace FundooNote.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// IEmail Sender Interface
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <returns>return Task</returns>
        Task SendEmailAsync(string email, string subject, string message);     
    }
}

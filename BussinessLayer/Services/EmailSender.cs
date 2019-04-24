//-----------------------------------------------------------------------
// <copyright file="EmailSender.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace FundooNote.Services
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using FundooNote.Interfaces;
    using FundooNote.Models;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Email sender class
    /// </summary>
    /// <seealso cref="FundooNotes.Interfaces.IEmailSender" />
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// The email model
        /// </summary>
        private readonly EmailModel emailmodel;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="emailModel">The email model.</param>
        public EmailSender(IOptions<EmailModel> emailModel)
        {
            this.emailmodel = emailModel.Value;
        }

        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <returns>return Task</returns>
        /// <exception cref="Exception">Throw exception</exception>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var networkCredential = new NetworkCredential(this.emailmodel.UserID, this.emailmodel.Password);
                var mail = new MailMessage()
                {
                    From = new MailAddress(this.emailmodel.UserID, this.emailmodel.FromAddress),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };
                mail.To.Add(new MailAddress(email));

                var client = new SmtpClient()
                {
                    Port = this.emailmodel.SMTPPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = this.emailmodel.SmtpClient,
                    EnableSsl = true,
                    Credentials = networkCredential
                };
                client.Send(mail);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return Task.CompletedTask;
        }
    }
}

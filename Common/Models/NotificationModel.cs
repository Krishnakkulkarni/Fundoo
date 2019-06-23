//-----------------------------------------------------------------------
// <copyright file="NotificationModel.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using FundooNote.Models;

    /// <summary>
    /// Class for Notification Model
    /// </summary>
    public class NotificationModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        [ForeignKey("ApplicationUser")]
        [Required]
        public string Userid { get; set; }

        /// <summary>
        /// Gets or sets the notification token.
        /// </summary>
        /// <value>
        /// The notification token.
        /// </value>
        [Required]
        public string NotificationToken { get; set; }

        /// <summary>
        /// Gets or sets the created data.
        /// </summary>
        /// <value>
        /// The created data.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the application user.
        /// </summary>
        /// <value>
        /// The application user.
        /// </value>
        public ApplicationUser ApplicationUser { get; set; }
    }
}

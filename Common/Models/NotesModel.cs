﻿//-----------------------------------------------------------------------
// <copyright file="NotesModel.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace Common.Models
{
    using FundooNote.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Notes Model class
    /// </summary>
    public class NotesModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
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
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [ForeignKey("ApplicationUser")]
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is archive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is archive; otherwise, <c>false</c>.
        /// </value>
        public bool IsArchive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is trash.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is trash; otherwise, <c>false</c>.
        /// </value>
        public bool IsTrash { get; set; }

        /// <summary>
        /// Gets or sets the reminder.
        /// </summary>
        /// <value>
        /// The reminder.
        /// </value>
        public DateTime Reminder { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the application user.
        /// </summary>
        /// <value>
        /// The application user.
        /// </value>
        public ApplicationUser ApplicationUser { get; set; }
    }
}

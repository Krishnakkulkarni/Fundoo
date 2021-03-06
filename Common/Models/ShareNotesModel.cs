﻿//-----------------------------------------------------------------------
// <copyright file="ShareNotesModel.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace Common.Models
{
    using System;

    /// <summary>
    /// Share Notes model class
    /// </summary>
    public class ShareNotesModel
    {
        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        public int NoteId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the take a note.
        /// </summary>
        /// <value>
        /// The take a note.
        /// </value>
        public string TakeANote { get; set; }

        /// <summary>
        /// Gets or sets the sender mail.
        /// </summary>
        /// <value>
        /// The sender mail.
        /// </value>
        ////public string Sendermail { get; set; }

        /// <summary>
        /// Gets or sets the receiver email.
        /// </summary>
        /// <value>
        /// The receiver email.
        /// </value>
        //// public string ReceiverEmail { get; set; }
    }
}

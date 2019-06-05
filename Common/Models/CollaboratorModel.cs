//-----------------------------------------------------------------------
// <copyright file="CollaboratorModel.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace Common.Models
{
    using System;
    using FundooNote.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CollaboratorModel
    {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the user identifier.
            /// </summary>
            /// <value>
            /// The user identifier.
            /// </value>
            [ForeignKey("ApplicationUser")]
            public string UserId { get; set; }

            /// <summary>
            /// Gets or sets the note identifier.
            /// </summary>
            /// <value>
            /// The note identifier.
            /// </value>
            [ForeignKey("NotesModel")]
            public int NoteId { get; set; }

            /// <summary>
            /// Gets or sets the sender email.
            /// </summary>
            /// <value>
            /// The sender email.
            /// </value>
            [EmailAddress]
            public string SenderEmail { get; set; }

            /// <summary>
            /// Gets or sets the receiver email.
            /// </summary>
            /// <value>
            /// The receiver email.
            /// </value>
            [EmailAddress]
            public string ReceiverEmail { get; set; }

            ///// <summary>
            ///// Gets or sets the application user.
            ///// </summary>
            ///// <value>
            ///// The application user.
            ///// </value>
            ////public virtual ApplicationUser ApplicationUser { get; set; }

            ///// <summary>
            ///// Gets or sets the notes model.
            ///// </summary>
            ///// <value>
            ///// The notes model.
            ///// </value>
            ////public virtual NotesModel NotesModel { get; set; }
    }
}

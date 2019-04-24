//-----------------------------------------------------------------------
// <copyright file="ApplicationLoginModel.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace FundooNote.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// User Login Model class
    /// </summary>
    public class ApplicationLoginModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required, MinLength(3), MaxLength(10)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required, MinLength(3), MaxLength(10)]
        public string Password { get; set; }
    }
}

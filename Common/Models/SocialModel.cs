//-----------------------------------------------------------------------
// <copyright file="SocialModel.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace Common.Models
{
    using System;

    /// <summary>
    /// Social Model class
    /// </summary>
    public class SocialModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the social identifier.
        /// </summary>
        /// <value>
        /// The social identifier.
        /// </value>
        public int SocialId { get; set; }
    }
}

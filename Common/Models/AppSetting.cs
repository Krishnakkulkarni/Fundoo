//-----------------------------------------------------------------------
// <copyright file="AppSetting.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace FundooNote.Models
{
    /// <summary>
    /// JWT class
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// Gets or sets the JWT secrete.
        /// </summary>
        /// <value>
        /// The JWT secrete.
        /// </value>
        public string JWT_Secrete { get; set; }

        /// <summary>
        /// Gets or sets the client URL.
        /// </summary>
        /// <value>
        /// The client URL.
        /// </value>
        public string Client_URL { get; set; }
    }
}

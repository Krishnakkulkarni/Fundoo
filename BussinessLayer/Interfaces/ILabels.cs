//-----------------------------------------------------------------------
// <copyright file="ILabels.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace BussinessLayer.Interfaces
{
    using Common.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interface ILabels
    /// </summary>
    public interface ILabels
    {
        /// <summary>
        /// Adds the labels.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        string AddLabels(LabelsModel label);

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        List<LabelsModel> GetLabels(string userId);

        /// <summary>
        /// Updates the labels.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        string UpdateLabels(LabelsModel label, int id);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        string DeleteLabel(int id);
    }
}

//-----------------------------------------------------------------------
// <copyright file="ILablesRepository.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using Common.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interface of ILablesRepository
    /// </summary>
    public interface ILablesRepository
    {
        /// <summary>
        /// Adds the labels.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        string AddLabels([FromBody] LabelsModel label);

        /// <summary>
        /// Updates the labels.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        string UpdateLabels(LabelsModel label, int id);

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        List<LabelsModel> GetLabels(string userId);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        string DeleteLabel(int id);
    }
}

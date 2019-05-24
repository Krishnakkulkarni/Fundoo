
namespace BussinessLayer.Services
{
    using BussinessLayer.Interfaces;
    using Common.Models;
    using RepositoryLayer.Context;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LabelsBussiness : ILabels
    {
        /// <summary>
        /// The labels repository
        /// </summary>
        private readonly LabelsRepository labelsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelBusiness"/> class.
        /// </summary>
        /// <param name="labelsRepository">The labels repository.</param>
        public LabelsBussiness(LabelsRepository labelsRepository)
        {
            this.labelsRepository = labelsRepository;
        }

        /// <summary>
        /// Adds the labels.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>
        /// returns string
        /// </returns>
        public string AddLabels(LabelsModel label)
        {
            var result = this.labelsRepository.AddLabels(label);
            return result;
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// returns string
        /// </returns>
        public string DeleteLabel(int id)
        {
            return this.labelsRepository.DeleteLabel(id);
        }

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns string
        /// </returns>
        public List<LabelsModel> GetLabels(string userId)
        {
            return this.labelsRepository.GetLabels(userId);
        }

        /// <summary>
        /// Updates the labels.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="newlabel">The new label.</param>
        /// <returns>
        /// returns string
        /// </returns>
        public string UpdateLabels(int id, string newlabel)
        {
            return this.labelsRepository.UpdateLabels(id, newlabel);
        }
    }
}

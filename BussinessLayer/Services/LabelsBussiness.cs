
namespace BussinessLayer.Services
{
    using BussinessLayer.Interfaces;
    using Common.Models;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LabelsBussiness : ILabels
    {
        /// <summary>
        /// The labels repository
        /// </summary>
        private readonly ILablesRepository labelsRepository;

        
        public LabelsBussiness(ILablesRepository labelsRepository)
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
        public string UpdateLabels(LabelsModel label, int id)
        {
            return this.labelsRepository.UpdateLabels(label,id);
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
    }
}

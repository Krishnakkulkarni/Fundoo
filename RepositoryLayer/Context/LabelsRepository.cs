//-----------------------------------------------------------------------
// <copyright file="LabelsRepository.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace RepositoryLayer.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using RepositoryLayer.Interface;

    /// <summary>
    /// Labels Repository class 
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.ILablesRepository" />
    public class LabelsRepository : ILablesRepository
    {
        /// <summary>
        /// The authentication
        /// </summary>
        private readonly Authentication authentication;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelsRepository"/> class.
        /// </summary>
        /// <param name="authentication">The authentication.</param>
        public LabelsRepository(Authentication authentication)
        {
            this.authentication = authentication;
        }

        /// <summary>
        /// Adds the labels.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>
        /// returns string
        /// </returns>
        /// <exception cref="Exception">throws exception</exception>
        public string AddLabels(LabelsModel label)
        {
            try
            {
                var addLabel = new LabelsModel()
                {
                    UserId = label.UserId,
                    Label = label.Label
                };
                this.authentication.Labels.Add(addLabel);
                var result = this.authentication.SaveChanges();
                return result.ToString();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        
        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns list</returns>
        /// <exception cref="Exception">throws exception</exception>
        public List<LabelsModel> GetLabels(string userId)
        {
            try
            {
                var list = new List<LabelsModel>();
                var labels = from t in this.authentication.Labels where t.UserId == userId select t;
                foreach (var items in labels)
                {
                    list.Add(items);
                }

                return list;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Updates the labels.
        /// </summary>
        /// <param name="label">The identifier.</param>
        /// <param name="id">The new label.</param>
        /// <returns>returns string</returns>
        /// <exception cref="Exception">throws exception</exception>
        public string UpdateLabels(LabelsModel label, int id)
        {
            LabelsModel labels = this.authentication.Labels.Where(t => t.Id == id).FirstOrDefault();
            labels.Label = label.Label;
            try
            {
                var result = this.authentication.SaveChanges();
                return result.ToString();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>returns string</returns>
        /// <exception cref="Exception">throws exception</exception>
        public string DeleteLabel(int id)
        {
            LabelsModel label = this.authentication.Labels.Where(t => t.Id == id).FirstOrDefault();
            try
            {
                this.authentication.Labels.Remove(label);
                var result = this.authentication.SaveChanges();
                return result.ToString();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="LabelsController.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace FundooApi.Controllers
{
    using BussinessLayer.Interfaces;
    using BussinessLayer.Services;
    using Common.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelsController : ControllerBase
    {
        /// <summary>
        /// The labels business
        /// </summary>
        private readonly ILabels labelsBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelsController"/> class.
        /// </summary>
        /// <param name="labelsBusiness">The labels business.</param>
        public LabelsController(ILabels labelsBusiness)
        {
            this.labelsBusiness = labelsBusiness;
        }

        /// <summary>
        /// Adds the labels.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>returns response</returns>
        [HttpPost]
        [Route("addLabel")]
        public IActionResult AddLabels(LabelsModel label)
        {
            var result = this.labelsBusiness.AddLabels(label);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(new { result });
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns response</returns>
        [HttpGet]
        [Route("getLabel/{userId}")]
        public IActionResult GetLabel(string userId)
        {
            IList<LabelsModel> result = this.labelsBusiness.GetLabels(userId);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(new { result });
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="label">The identifier.</param>
        /// <param name="id">The newlabel.</param>
        /// <returns>returns response</returns>
        [HttpPut]
        [Route("updateLabel/{id}")]
        public IActionResult UpdateLabel(LabelsModel label, int id)
        {
            var result = this.labelsBusiness.UpdateLabels(label, id);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(new { result });
        }

        /// <summary>
        /// Deletelabels the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>returns response</returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Deletelabel(int id)
        {
            var result = this.labelsBusiness.DeleteLabel(id);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(new { result });
        }
    }
}
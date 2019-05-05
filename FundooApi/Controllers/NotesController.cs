//-----------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace FundooApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BussinessLayer.Interfaces;
    using Common.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Notes Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// The notes creation
        /// </summary>
        private readonly INotes notesCreation;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesCreation">The notes creation.</param>
        public NotesController(INotes notesCreation)
        {
            this.notesCreation = notesCreation;
        }

        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>return result</returns>
        [HttpPost]
        [Route("addNotes")]
        public async Task<IActionResult> CreateNotes(NotesModel notesModel)
        {
                var result = await this.notesCreation.Create(notesModel);
                if (result == 1)
                {
                    return this.Ok();
                }
                else
                {
                    return this.BadRequest();
                }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>return result</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteNotes(int id)
        {
            try
            {
                var result = await this.notesCreation.Delete(id);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return result</returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateNotes(NotesModel notesModel, int id)
        {
            try
            {
                var result = await this.notesCreation.Change(notesModel, id);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Views all.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return notes</returns>
        [HttpGet]
        [Route("{UserId}")]
        public IActionResult ViewAll(Guid userId)
        {
            try
            {
                IList<NotesModel> note = this.notesCreation.AccessNotes(userId);
                return this.Ok(note);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Images the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("image/{id}")]
        public IActionResult Image(IFormFile file, int id)
        {
            Console.WriteLine(file);
            if (file == null)
            {
                return this.NotFound("The file couldn't be found");
            }

            var result = this.notesCreation.BrowseImage(file, id);
            return this.Ok(new { result });
        }
    }
}
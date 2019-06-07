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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Notes Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
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
                    return this.Ok(result);
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
        [AllowAnonymous]
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
                return this.Ok(new { result });
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
        public IActionResult ViewAll(string userId)
        {
            try
            {
                IList<NotesModel> note = this.notesCreation.AccessNotes(userId);
                return this.Ok(new { note });
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

        /// <summary>
        /// Archives the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("archive/{userId}")]
        public IActionResult ArchiveNotes(string userId)
        {
            IList<NotesModel> result = this.notesCreation.Archive(userId);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(new { result });
        }

        /// <summary>
        /// Trashes the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("trash/{userId}")]
        public IActionResult TrashNotes(string userId)
        {
            IList<NotesModel> result = this.notesCreation.Trash(userId);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(new { result });
        }

        /// <summary>
        /// Reminders the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns response</returns>
        [HttpGet]
        [Route("reminder/{userId}")]
        public IActionResult Reminder(string userId)
        {
            IList<NotesModel> result = this.notesCreation.Reminder(userId);
            if (result == null)
            {
                return this.NotFound("no reminder");
            }

            return this.Ok(new { result });
        }

        /// <summary>
        /// Adds the collaborator to note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("addCollaborator")]
        public IActionResult AddCollaboratorToNote(CollaboratorModel model)
        {
            try
            {
                var result = this.notesCreation.AddCollaboratorToNote(model);
                if (result == null)
                {
                    return this.NotFound("Collaborator not added");
                }

                return this.Ok(new { result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Removes the collaborator to note.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("removeCollaborator/{id}")]
        public IActionResult RemoveCollaboratorToNote(int id)
        {
            try
            {
                var result = this.notesCreation.RemoveCollaboratorToNote(id);
                if (result == null)
                {
                    return this.NotFound("Collaborator not found");
                }

                return this.Ok(new { result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Collaborators the note.
        /// </summary>
        /// <param name="receiverEmail">The receiver email.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getCollaborator/{receiverEmail}")]
        public IActionResult CollaboratorNote(string receiverEmail)
        {
            try
            {
                var result = this.notesCreation.CollaboratorNote(receiverEmail);
                if (result == null)
                {
                    return this.NotFound("Collaborator not found");
                }

                return this.Ok(new { result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Updates the collaborater.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="receiverEmail">The receiver email.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateCollaborater")]
        public async Task<IActionResult> UpdateCollaborater(NotesModel model, int id, string receiverEmail)
        {
            try
            {
                //HttpContext.User.Claims.Where(x => x.Type == "UserId").SingleOrDefault();
                var result = await this.notesCreation.UpdateCollaborater(model, id, receiverEmail);
                return this.Ok(result);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Adds the note label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>returns result</returns>
        [HttpPost]
        [Route("notelabel")]
        public IActionResult AddNoteLabel(NoteLabelModel label)
        {
            var result = this.notesCreation.AddNotesLabel(label);
            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(new { result });
        }

        ///// <summary>
        ///// Gets the note label.
        ///// </summary>
        ///// <param name="userId">The user identifier.</param>
        ///// <returns>returns result</returns>
        //[HttpGet]
        //[Route("notelabel/{userId}")]
        //public IActionResult GetNoteLabel(Guid userId)
        //{
        //    IList<NoteLabelModel> result = this.notesCreation.GetNotesLabel(userId);
        //    if (result == null)
        //    {
        //        return this.NotFound();
        //    }

        //    return this.Ok(new { result });
        //}

        ///// <summary>
        ///// Deletes the note label.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns>returns result</returns>
        //[HttpDelete]
        //[Route("notelabel/{id}")]
        //public IActionResult DeleteNotelabel(int id)
        //{
        //    var result = this.notesCreation.DeleteNotesLabel(id);
        //    if (result == null)
        //    {
        //        return this.NotFound();
        //    }

        //    return this.Ok(new { result });
        //}
    }
}
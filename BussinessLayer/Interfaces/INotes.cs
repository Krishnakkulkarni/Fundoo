//-----------------------------------------------------------------------
// <copyright file="INotes.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace BussinessLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Models;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// INotes interface
    /// </summary>
    public interface INotes
    {
        /// <summary>
        /// Creates the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>return integer</returns>
        Task<int> Create(NotesModel notesModel);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>return integer</returns>
        Task<int> Delete(int id);

        /// <summary>
        /// Changes the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return integer</returns>
        Task<int> Change(NotesModel notesModel, int id);

        IList<NotesModel> AccessNotes(string userId);

        /// <summary>
        /// Browses the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return string</returns>
        string BrowseImage(IFormFile file, int id);

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return list</returns>
        IList<NotesModel> Archive(string userId);

        /// <summary>
        /// Trashes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return list</returns>
        IList<NotesModel> Trash(string userId);

        /// <summary>
        /// Reminders the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns list</returns>
        IList<NotesModel> Reminder(string userId);

        /// <summary>
        /// Adds the collaborator to note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>returns string</returns>
        string AddCollaboratorToNote(CollaboratorModel model);

        /// <summary>
        /// Removes the collaborator to note.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>returns string</returns>
        string RemoveCollaboratorToNote(int id);

        /// <summary>
        /// Collaborators the note.
        /// </summary>
        /// <param name="receiverEmail">The receiver email.</param>
        /// <returns>returns string</returns>
        IList<NotesModel> CollaboratorNote(string receiverEmail);

        Task<int> UpdateCollaborater(NotesModel model, int id, string receiverEmail);

        string AddNotesLabel(NoteLabelModel model);
    }
}

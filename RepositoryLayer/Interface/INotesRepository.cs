//-----------------------------------------------------------------------
// <copyright file="INotesRepository.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Models;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// INotes Repository interface
    /// </summary>
    public interface INotesRepository
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>return string</returns>
        void AddNotes(NotesModel notesModel);

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>return integer</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Removes the notes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void RemoveNotes(int id);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        void UpdateNotes(NotesModel notesModel, int id);

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return NotesModel</returns>
        IList<NotesModel> GetNotes(Guid userId);

        /// <summary>
        /// Images the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return string</returns>
        string Image(IFormFile file, int id);

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return list</returns>
        IList<NotesModel> Archive(Guid userId);

        /// <summary>
        /// Trashes the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return list</returns>
        IList<NotesModel> TrashNote(Guid userId);
    }
}

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

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return NotesModel</returns>
        IList<NotesModel> AccessNotes(Guid userId);
    }
}

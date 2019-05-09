//-----------------------------------------------------------------------
// <copyright file="NotesCreation.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace BussinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BussinessLayer.Interfaces;
    using Common.Models;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Interface;

    /// <summary>
    /// Notes Creation
    /// </summary>
    /// <seealso cref="BussinessLayer.Interfaces.INotes" />
    public class NotesCreation : INotes
    {
        /// <summary>
        /// The notes repository
        /// </summary>
        private readonly INotesRepository notesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesCreation"/> class.
        /// </summary>
        /// <param name="notesRepository">The notes repository.</param>
        public NotesCreation(INotesRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }

        /// <summary>
        /// Creates the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>return integer</returns>
        public Task<int> Create(NotesModel notesModel)
        {
            this.notesRepository.AddNotes(notesModel);
            var result = this.notesRepository.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>return integer</returns>
        public Task<int> Delete(int id)
        {
            this.notesRepository.RemoveNotes(id);
            var result = this.notesRepository.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Changes the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return integer</returns>
        public Task<int> Change(NotesModel notesModel, int id)
        {
            this.notesRepository.UpdateNotes(notesModel, id);
            var result = this.notesRepository.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return NotesModel</returns>
        public IList<NotesModel> AccessNotes(Guid userId)
        {
            return this.notesRepository.GetNotes(userId);
        }

        /// <summary>
        /// Browses the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return string</returns>
        public string BrowseImage(IFormFile file, int id)
        {
            var result = this.notesRepository.Image(file, id);
            return result;
        }

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return list</returns>
        public IList<NotesModel> Archive(Guid userId)
        {
            return this.notesRepository.Archive(userId);
        }

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return list</returns>
        public IList<NotesModel> Trash(Guid userId)
        {
            return this.notesRepository.TrashNote(userId);
        }
    }
}

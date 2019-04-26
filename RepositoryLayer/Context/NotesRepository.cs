//-----------------------------------------------------------------------
// <copyright file="NotesRepository.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace RepositoryLayer.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Models;
    using RepositoryLayer.Interface;

    /// <summary>
    /// Notes Repository class
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.INotesRepository" />
    public class NotesRepository : INotesRepository
    {
        /// <summary>
        /// Gets the authentication.
        /// </summary>
        /// <value>
        /// The authentication.
        /// </value>
        private readonly Authentication authentication;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesRepository"/> class.
        /// </summary>
        /// <param name="authentication">The authentication.</param>
        public NotesRepository(Authentication authentication)
        {
            this.authentication = authentication;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        public void AddNotes(NotesModel notesModel)
        {
            var notes = new NotesModel()
            {
                UserId = notesModel.UserId,
                Title = notesModel.Title,
                Description = notesModel.Description,
                CreatedDate = notesModel.CreatedDate,
                ModifiedDate = notesModel.ModifiedDate
            };
            var result = this.authentication.NotesModel.Add(notes);
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>return integer</returns>
        public Task<int> SaveChangesAsync()
        {
            var result = this.authentication.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Removes the notes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void RemoveNotes(int id)
        {
            var note = this.authentication.NotesModel.Where<NotesModel>(c => c.Id.Equals(id)).FirstOrDefault();
            var result = this.authentication.NotesModel.Remove(note);
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        public void UpdateNotes(NotesModel model, int id)
        {
            NotesModel notes = this.authentication.NotesModel.Where<NotesModel>(c => c.Id.Equals(id)).FirstOrDefault();
            notes.Title = model.Title;
            notes.Description = model.Description;
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return NotesModel</returns>
        public IList<NotesModel> GetNotes(Guid userId)
        {
            var list = new List<NotesModel>();
            var note = from notes in this.authentication.NotesModel where notes.UserId == userId orderby notes.UserId descending select notes;
            foreach (var item in note)
            {
                list.Add(item);
            }

            return note.ToArray();
        }
    }
}

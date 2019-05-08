﻿//-----------------------------------------------------------------------
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
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Common.Models;
    using Microsoft.AspNetCore.Http;
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
        /// <returns>return string</returns>
        public string AddNotes(NotesModel notesModel)
        {
            var notes = new NotesModel()
            {
                UserId = notesModel.UserId,
                Title = notesModel.Title,
                Description = notesModel.Description,
                Color = notesModel.Color,
                CreatedDate = notesModel.CreatedDate,
                ModifiedDate = notesModel.ModifiedDate
            };
            var result = this.authentication.NotesModel.Add(notes);
            return null;
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
            notes.Color = model.Color;
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

        /// <summary>
        /// Images the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return string</returns>
        public string Image(IFormFile file, int id)
        {
            var stream = file.OpenReadStream();
            var name = file.FileName;
            Account account = new Account("db4wyl94g", "645173152293519", "hBF7yF3HzJGByBvdWnzfR_kegmI");
            Cloudinary cloudinary = new Cloudinary(account);
            var imageUploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, stream)
            };
            var uploadResult = cloudinary.Upload(imageUploadParams);
            var data = this.authentication.NotesModel.Where(t => t.Id == id).FirstOrDefault();
            data.Image = uploadResult.Uri.ToString();
            int result = 0;
            try
            {
                result = this.authentication.SaveChanges();
                return data.Image;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
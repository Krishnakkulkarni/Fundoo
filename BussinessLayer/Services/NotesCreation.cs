﻿//-----------------------------------------------------------------------
// <copyright file="NotesCreation.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace BussinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BussinessLayer.Interfaces;
    using Common.Models;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Interface;
    using ServiceStack.Redis;

    /// <summary>
    /// Notes Creation
    /// </summary>
    /// <seealso cref="BussinessLayer.Interfaces.INotes" />
    public class NotesCreation : INotes
    {
        /// <summary>
        /// The identifier.
        /// </summary>
        public const string Redisdata = "Notes_";

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
            //using (var redis = new RedisClient())
            //{
            //    redis.Remove(Redisdata + notesModel.UserId);
            //}

            //this.AccessNotes(notesModel.UserId);
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
            //using (var redis = new RedisClient())
            //{
            //    redis.Remove(Redisdata + notesModel.UserId);
            //}

            //this.AccessNotes(notesModel.UserId);
            return result;
        }

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns NotesModel</returns>
        public (IList<NotesModel>,IList<CollaboratorModel>) AccessNotes(string userId)
        {
            //var cacheKey = Redisdata + userId.ToString();
            //using (var redis = new RedisClient())
            //{
            //    if (redis.Get(cacheKey) == null)
            //    {
            //        var notes = this.notesRepository.GetNotes(userId);
            //        if (notes != null)
            //        {
            //            redis.Set(cacheKey, notes);
            //        }

            //        return notes.ToArray();
            //    }
            //    else
            //    {
                    var notes = this.notesRepository.GetNotes(userId);
                    //var redisNotes = redis.Get<List<NotesModel>>(cacheKey);
                    return notes;
            //    }
            //}
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
        public IList<NotesModel> Archive(string userId)
        {
            return this.notesRepository.Archive(userId);
        }

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return list</returns>
        public IList<NotesModel> Trash(string userId)
        {
            return this.notesRepository.TrashNote(userId);
        }

        /// <summary>
        /// Reminders the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns list
        /// </returns>
        public IList<NotesModel> Reminder(string userId)
        {
            return this.notesRepository.Reminder(userId);
        }

        /// <summary>
        /// Adds the collaborator to note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>returns string</returns>
        public string AddCollaboratorToNote(CollaboratorModel model)
        {
            return this.notesRepository.AddCollaboratorToNote(model);
        }

        /// <summary>
        /// Removes the collaborator to note.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// return string
        /// </returns>
        public string RemoveCollaboratorToNote(int id)
        {
            return this.notesRepository.RemoveCollaboratorToNote(id);
        }

        /// <summary>
        /// Collaborators the note.
        /// </summary>
        /// <param name="receiverEmail">The receiver email.</param>
        /// <returns>
        /// return string
        /// </returns>
        ////public IList<ShareNotesModel> CollaboratorNote(string receiverEmail)
        ////{
        ////    return this.notesRepository.CollaboratorNote(receiverEmail);
        ////}

        /// <summary>
        /// Updates the collaborator.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="receiverEmail">The receiver email.</param>
        /// <returns>returns integer</returns>
        public Task<int> UpdateCollaborater(NotesModel model, int id, string receiverEmail)
        {
            this.notesRepository.UpdateCollaborater(model, id, receiverEmail);
            var result = this.notesRepository.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Adds the notes label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>returns string</returns>
        public string AddNotesLabel(NoteLabelModel model)
        {
            var result = this.notesRepository.AddNoteLabel(model);
            return result;
        }
    }
}

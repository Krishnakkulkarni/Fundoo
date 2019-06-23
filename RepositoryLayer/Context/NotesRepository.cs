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
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Common.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
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
            //// Creating an instance to add notes
            var notes = new NotesModel()
            {
                UserId = notesModel.UserId,
                Title = notesModel.Title,
                Description = notesModel.Description,
                Label = notesModel.Label,
                Color = notesModel.Color,
                IsArchive = notesModel.IsArchive,
                IsTrash = notesModel.IsTrash,
                Reminder = notesModel.Reminder,
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
            //// update database
            var result = this.authentication.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Removes the notes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void RemoveNotes(int id)
        {
            //// LinQ query to remove notes by comparing its note id
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
            //// query to update notes by comparing its note id
            NotesModel notes = this.authentication.NotesModel.Where<NotesModel>(c => c.Id.Equals(id)).FirstOrDefault();
            notes.Title = model.Title;
            notes.Description = model.Description;
            notes.Label = model.Label;
            notes.Color = model.Color;
            notes.IsArchive = model.IsArchive;
            notes.IsTrash = model.IsTrash;
            notes.Reminder = model.Reminder;
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns NotesModel</returns>
        public (IList<NotesModel>,IList<CollaboratorModel>) GetNotes(string userId)
        {
            //// query to Get all notes by comparing its userid
          //  var note = from notes in this.authentication.NotesModel where notes.UserId.Equals(userId) && notes.IsArchive == false && notes.IsTrash == false orderby notes.Id descending select notes;
            

            var Noteslist = new List<NotesModel>();
            var CollList = new List<CollaboratorModel>();

            var note = from notes in this.authentication.NotesModel where (notes.UserId.Equals(userId) && notes.IsTrash == false && notes.IsArchive == false) orderby notes.UserId descending select notes;
            foreach (var notes in note)
            {
                Noteslist.Add(notes);
            }

            var user = from users in authentication.ApplicationUsers where users.Id == userId select users;
            foreach (var users in user)
            {
                var noteJoin = from u in this.authentication.NotesModel
                               join c in this.authentication.Collaborator on u.UserId equals c.UserId
                               where u.Id == c.NoteId && (c.ReceiverEmail == users.UserName)

                               select new NotesModel
                               {
                                   Id = u.Id,
                                   UserId = u.UserId,
                                   Title = u.Title,
                                   Description = u.Description,
                                   Color = u.Color,
                                   Label = u.Label,
                                   Image = u.Image,
                               };
                var collaboratorjoin = from u in this.authentication.NotesModel
                                       join c in this.authentication.Collaborator on u.UserId equals c.UserId
                                       where u.Id == c.NoteId
                                       select new CollaboratorModel
                                       {
                                           Id = c.Id,
                                           UserId = c.UserId,
                                           NoteId = c.NoteId,
                                           ReceiverEmail = c.ReceiverEmail,
                                           SenderEmail = c.SenderEmail
                                       };
                foreach (var notesJoin in noteJoin)
                {
                    Noteslist.Add(notesJoin);
                }
                foreach (var collaboratorsJoin in collaboratorjoin)
                {
                    CollList.Add(collaboratorsJoin);
                }
            }
            return (Noteslist.ToArray(),CollList.ToArray());
        }

        /// <summary>
        /// Images the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return string</returns>
        public string Image(IFormFile file, int id)
        {
            try
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

                this.authentication.Update(data);
                this.authentication.SaveChanges();

                return data.Image;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns list</returns>
        public IList<NotesModel> Archive(string userId)
        {
            var list = new List<NotesModel>();
            //// LinQ query to Get archived notes by comparing its userid
            var note = from notes in this.authentication.NotesModel where (notes.UserId == userId) && (notes.IsArchive == true) && (notes.IsTrash == false) select notes;
            foreach (var data in note)
            {
                list.Add(data);
            }

            return list;
        }

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns list</returns>
        public IList<NotesModel> TrashNote(string userId)
        {
            var list = new List<NotesModel>();
            //// LinQ query to Get trashed notes by comparing its userid
            var note = from notes in this.authentication.NotesModel where (notes.UserId == userId) && (notes.IsTrash == true) select notes;
            foreach (var data in note)
            {
                list.Add(data);
            }

            return list;
        }

        /// <summary>
        /// Reminders the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns list</returns>
        public IList<NotesModel> Reminder(string userId)
        {
            var list = new List<NotesModel>();
            //// LinQ query to Get reminder on note by comparing its userid
            var notesData = from notes in this.authentication.NotesModel where (notes.UserId == userId) && (notes.Reminder != null || !notes.Reminder.Equals("0001-01-01 00:00:00.0000000")) select notes;
            foreach (var data in notesData)
            {
                list.Add(data);
            }

            return list;
        }

        /// <summary>
        /// Method to add Collaborator
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns>returns string</returns>
        public string AddCollaboratorToNote(CollaboratorModel model)
        {
            try
            {
                var data = from t in this.authentication.Collaborator where t.UserId == model.UserId && t.NoteId == model.NoteId select t;
                var newdata = new CollaboratorModel()
                {
                    UserId = model.UserId,
                    NoteId = model.NoteId,
                    SenderEmail = model.SenderEmail,
                    ReceiverEmail = model.ReceiverEmail
                };
                this.authentication.Collaborator.Add(newdata);
                var result = this.authentication.SaveChanges();
                return result.ToString();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Method to remove Collaborator
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>returns string</returns>
        public string RemoveCollaboratorToNote(int id)
        {
            try
            {
                var data = this.authentication.Collaborator.Where<CollaboratorModel>(t => t.Id == id).FirstOrDefault();
                this.authentication.Collaborator.Remove(data);
                var result = this.authentication.SaveChanges();
                return result.ToString();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Collaborators the note.
        /// </summary>
        /// <param name="receiverEmail">The receiver email.</param>
        /// <returns>
        /// returns list
        /// </returns>
        /// <exception cref="Exception">error message</exception>
        ////public IList<ShareNotesModel> CollaboratorNote(string receiverEmail)
        ////{
        ////    try
        ////    {
        ////        var collaboratorData = new List<NotesModel>();
        ////        var sharednotes = new List<ShareNotesModel>();
        ////        var data = from coll in this.authentication.Collaborator
        ////                   where coll.ReceiverEmail == receiverEmail
        ////                   select new
        ////                   {
        ////                       coll.SenderEmail,
        ////                       coll.NoteId
        ////                   };
        ////        foreach (var result in data)
        ////        {
        ////            var collnotes = from notes in this.authentication.NotesModel
        ////                            where notes.Id == result.NoteId
        ////                            select new ShareNotesModel
        ////                            {
        ////                                NoteId = notes.Id,
        ////                                Title = notes.Title,
        ////                                TakeANote = notes.Description,
        ////                            };
        ////            foreach (var collaborator in collnotes)
        ////            {
        ////                sharednotes.Add(collaborator);
        ////            }
        ////        }

        ////        return sharednotes.ToArray();
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        throw new Exception(e.Message);
        ////    }
        ////}

        /// <summary>
        /// Updates the collaborator.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="receiverEmail">The receiver email.</param>
        public void UpdateCollaborater([FromBody]NotesModel model, int id, string receiverEmail)
        {
            var data = from coll in this.authentication.Collaborator
                       where coll.ReceiverEmail == receiverEmail && coll.UserId == model.UserId
                       select new { coll.NoteId };

            foreach (var result in data)
            {
                NotesModel collnotes = this.authentication.NotesModel.Where<NotesModel>(t => t.Id == id).FirstOrDefault();
                collnotes.Title = model.Title;
                collnotes.Description = model.Description;
            }
        }

        /// <summary>
        /// Adds the notes label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>returns string</returns>
        /// <exception cref="Exception">throws exception</exception>
        public string AddNoteLabel([FromBody] NoteLabelModel model)
        {
            try
            {
                var labelData = from t in this.authentication.NoteLabel where t.UserId == model.UserId select t;
                foreach (var datas in labelData.ToList())
                {
                    if (datas.NoteId == model.NoteId && datas.LableId == model.LableId)
                    {
                        return false.ToString();
                    }
                }

                var data = new NoteLabelModel
                {
                    LableId = model.LableId,
                    NoteId = model.NoteId,
                    UserId = model.UserId
                };
                int result = 0;
                this.authentication.NoteLabel.Add(data);
                result = this.authentication.SaveChanges();
                return result.ToString();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}

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
        /// <returns>return string</returns>
        public void AddNotes(NotesModel notesModel)
        {
            var notes = new NotesModel()
            {
                UserId = notesModel.UserId,
                Title = notesModel.Title,
                Description = notesModel.Description,
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
            notes.IsArchive = model.IsArchive;
            notes.IsTrash = model.IsTrash;
            notes.Reminder = model.Reminder;
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns NotesModel</returns>
        public IList<NotesModel> GetNotes(string userID)
        {
            var note = from notes in this.authentication.NotesModel where notes.UserId.Equals(userID) && notes.IsArchive == false && notes.IsTrash == false orderby notes.Id descending select notes;
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

                int result = this.authentication.SaveChanges();
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
            var notesData = from notes in this.authentication.NotesModel where (notes.UserId == userId) && (notes.Reminder != null) select notes;
            foreach (var data in notesData)
            {
                list.Add(data);
            }

            return list;
        }

        public string AddCollaboratorToNote([FromBody] CollaboratorModel model)
        {
            try
            {
                var data = from t in this.authentication.Collaborator where t.UserId == model.UserId select t;
                foreach (var item in data.ToList())
                {
                    if (item.NoteId.Equals(model.NoteId) && item.ReceiverEmail.Equals(model.ReceiverEmail))
                    {
                        return false.ToString();
                    }
                }

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

        
        public IList<NotesModel> CollaboratorNote(string receiverEmail)
        {
            try
            {
                var sharednotes = new List<NotesModel>();
                var data = from coll in this.authentication.Collaborator
                           where coll.ReceiverEmail == receiverEmail
                           select new
                           {
                               coll.SenderEmail,
                               coll.NoteId
                           };
                foreach (var result in data)
                {
                    var collnotes = from notes in this.authentication.NotesModel
                                    where notes.Id == result.NoteId
                                    select new NotesModel
                                    {
                                        Id = notes.Id,
                                        Title = notes.Title,
                                        Description = notes.Description,
                                    };
                    foreach (var collaborator in collnotes)
                    {
                        sharednotes.Add(collaborator);
                    }
                }

                return sharednotes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

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

        ///// <summary>
        ///// Adds the labels.
        ///// </summary>
        ///// <param name="label">The label.</param>
        ///// <returns>returns string</returns>
        ///// <exception cref="Exception">throws exception</exception>
        //public string AddLabels([FromBody] LabelsModel label)
        //{
        //    var addLabel = new LabelsModel()
        //    {
        //        UserId = label.UserId,
        //        Label = label.Label
        //    };
        //    try
        //    {
        //        this.authentication.Labels.Add(addLabel);
        //        var result = this.authentication.SaveChanges();
        //        return result.ToString();
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }
        //}

        ///// <summary>
        ///// Gets the labels.
        ///// </summary>
        ///// <param name="userId">The user identifier.</param>
        ///// <returns>returns list</returns>
        ///// <exception cref="Exception">throws exception</exception>
        //public List<LabelsModel> GetLabels(Guid userId)
        //{
        //    try
        //    {
        //        var list = new List<LabelsModel>();
        //        var labels = from t in this.authentication.Labels where t.UserId == userId select t;
        //        foreach (var items in labels)
        //        {
        //            list.Add(items);
        //        }

        //        return list;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }
        //}

        ///// <summary>
        ///// Updates the labels.
        ///// </summary>
        ///// <param name="label">The label.</param>
        ///// <param name="id">The identifier.</param>
        ///// <returns>returns string</returns>
        ///// <exception cref="Exception">throws exception</exception>
        //public string UpdateLabels([FromBody] LabelsModel label, int id)
        //{
        //    LabelsModel labels = this.authentication.Labels.Where(t => t.Id == id).FirstOrDefault();
        //    labels.Label = label.Label;
        //    try
        //    {
        //        var result = this.authentication.SaveChanges();
        //        return result.ToString();
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }
        //}

        ///// <summary>
        ///// Deletes the label.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns>returns string</returns>
        ///// <exception cref="Exception">throws exception</exception>
        //public string DeleteLabel(int id)
        //{
        //    LabelsModel label = this.authentication.Labels.Where(t => t.Id == id).FirstOrDefault();
        //    try
        //    {
        //        this.authentication.Labels.Remove(label);
        //        var result = this.authentication.SaveChanges();
        //        return result.ToString();
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }
        //}

        ///// <summary>
        ///// Adds the notes label.
        ///// </summary>
        ///// <param name="model">The model.</param>
        ///// <returns>returns string</returns>
        ///// <exception cref="Exception">throws exception</exception>
        //public string AddNotesLabel([FromBody] NoteLabelModel model)
        //{
        //    try
        //    {
        //        var labelData = from t in this.authentication.NoteLabel where t.UserId == model.UserId select t;
        //        foreach (var datas in labelData.ToList())
        //        {
        //            if (datas.NoteId == model.NoteId && datas.LableId == model.LableId)
        //            {
        //                return false.ToString();
        //            }
        //        }

        //        var data = new NoteLabelModel
        //        {
        //            LableId = model.LableId,
        //            NoteId = model.NoteId,
        //            UserId = model.UserId
        //        };
        //        int result = 0;
        //        this.authentication.NoteLabel.Add(data);
        //        result = this.authentication.SaveChanges();
        //        return result.ToString();
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }
        //}

        ///// <summary>
        ///// Gets the notes label.
        ///// </summary>
        ///// <param name="userId">The user identifier.</param>
        ///// <returns>returns list of label</returns>
        ///// <exception cref="Exception">throws exception</exception>
        //public List<NoteLabelModel> GetNotesLabel(Guid userId)
        //{
        //    var list = new List<NoteLabelModel>();
        //    var labelData = from t in this.authentication.NoteLabel where t.UserId == userId select t;
        //    try
        //    {
        //        foreach (var data in labelData)
        //        {
        //            list.Add(data);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }

        //    return list;
        //}

        ///// <summary>
        ///// Deletes the notes label.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns>returns string</returns>
        ///// <exception cref="Exception">throws exception</exception>
        //public string DeleteNotesLabel(int id)
        //{
        //    var label = this.authentication.NoteLabel.Where<NoteLabelModel>(t => t.Id == id).FirstOrDefault();

        //    try
        //    {
        //        this.authentication.NoteLabel.Remove(label);
        //        var result = this.authentication.SaveChanges();
        //        return result.ToString();
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }
        //}
    }
}

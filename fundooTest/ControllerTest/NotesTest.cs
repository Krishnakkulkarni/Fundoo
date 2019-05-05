//-----------------------------------------------------------------------
// <copyright file="NotesTest.cs" company="Bridgelabz">
//     Company @ 2019 </copyright>
// <creator name = "Krishna Kulkarni" />
//-----------------------------------------------------------------------
namespace FundooTest.ControllerTest
{
    using System;
    using BussinessLayer.Services;
    using Common.Models;
    using Moq;
    using RepositoryLayer.Interface;
    using Xunit;

    /// <summary>
    /// Notes test case
    /// </summary>
    public class NotesTest
    {
        /// <summary>
        /// Adds this instance.
        /// </summary>
        [Fact]
        public void Add()
        {
            ////arrange
            var service = new Mock<INotesRepository>();
            var notes = new NotesCreation(service.Object);
            var addNotes = new NotesModel()
            {
                Id = 1,
                Title = "alphabetes",
                Description = "abcdefgh",
                UserId = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            ////act
            var data = notes.Create(addNotes);

            ////assert
            Assert.NotNull(data);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        [Fact]
        public void Update()
        {
            ////arrange
            var service = new Mock<INotesRepository>();
            var notes = new NotesCreation(service.Object);
            var addNotes = new NotesModel()
            {
                Id = 0,
                Title = "Title",
                Description = "Description",
                UserId = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            ////act
            var data = notes.Change(addNotes, 2);

            ////assert
            Assert.NotNull(data);
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        [Fact]
        public void DeleteNote()
        {
            // arrange
            var service = new Mock<INotesRepository>();
            var notes = new NotesCreation(service.Object);

            ////act
            var data = notes.Delete(6);

            ////assert
            Assert.NotNull(data);
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        [Fact]
        public void GetNotes()
        {
            // arrange
            var service = new Mock<INotesRepository>();
            var notes = new NotesCreation(service.Object);

            ////act
            var data = notes.AccessNotes(Guid.NewGuid());

            ////assert
            Assert.Null(data);
        }
    }
}
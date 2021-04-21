using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using StudentsApp.Core;
using StudentsApp.Core.Models;
using StudentsApp.Core.Repositories;
using NUnit.Framework;

namespace StudentsApp.BLL.Tests
{
    [TestFixture]
    public class DeleteMarkTests
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<IMarkRepository> markRepo, Dictionary<int, Mark> dbCollectionMark) GetMocks()
        {
            var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var markRepo = new Mock<IMarkRepository>(MockBehavior.Strict);
            var studentRepo = new Mock<IStudentRepository>(MockBehavior.Strict);
            var dbCollectionMark = new Dictionary<int, Mark>
            {
                [26] = new Mark
                {
                    Id = 26,
                    StudentId = 26,
                    Grade = 2
                },
                [27] = new Mark
                {
                    Id = 27,
                    StudentId = 27,
                    Grade = 2
                }
            };
            
            var dbCollectionStudents = new Dictionary<int, Student>
            {
                [26] = new Student
                {
                    Id = 26,
                    Name = "Teacher"
                },
                [27] = new Student
                {
                    Id = 27,
                    Name = "Other teacher"
                }
            };

            unitOfWork.SetupGet(e => e.Marks).Returns(markRepo.Object);
            unitOfWork.SetupGet(e => e.Students).Returns(studentRepo.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);
            
            markRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                     .ReturnsAsync((int id) => dbCollectionMark.ContainsKey(id));
            markRepo.Setup(e => e.Remove(It.IsAny<Mark>()))
                     .Callback((Mark newMark) => { dbCollectionMark.Remove(newMark.Id); });
            
            studentRepo.Setup(e => e.IsExists(It.IsAny<int>()))
                      .ReturnsAsync((int id) => dbCollectionStudents.ContainsKey(id));
            studentRepo.Setup(e => e.Remove(It.IsAny<Student>()))
                      .Callback((Student newStudent) => { dbCollectionStudents.Remove(newStudent.Id); });

            return (unitOfWork, markRepo, dbCollectionMark);
        }

        [Test]
        public async Task DeleteMark_TargetItem_Success()
        {
            // Arrange
            var (unitOfWork, markRepo, dbCollectionMark) = GetMocks();
            var service = new MarkService(unitOfWork.Object);
            var mark = new Mark
            {
                Id = 26,
                Grade = 2
            };

            // Act
            await service.DeleteMark(mark);
            
            // Assert
            Assert.IsFalse(dbCollectionMark.ContainsKey(26));
        }

        [Test]
        public void DeleteMark_ItemDoesNotExists_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, markRepo, dbCollectionMark) = GetMocks();
            var service = new MarkService(unitOfWork.Object);
            var mark = new Mark
            {
                Id = 0,
                Grade = 2
            };

            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteMark(mark));
        }
    }
}
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
    public class CreateMarkTests
    {
        private static (Mock<IUnitOfWork> unitOfWork, Mock<IMarkRepository> markRepo, Dictionary<int, Mark> dbCollection) GetMocks()
        {
            var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            var markRepo = new Mock<IMarkRepository>(MockBehavior.Strict);
            var dbCollection = new Dictionary<int, Mark>
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

            unitOfWork.SetupGet(e => e.Marks).Returns(markRepo.Object);
            unitOfWork.Setup(e => e.CommitAsync()).ReturnsAsync(0);
            
            markRepo.Setup(e => e.AddAsync(It.IsAny<Mark>()))
                     .Callback((Mark newMark) => { dbCollection.Add(newMark.Id, newMark); })
                     .Returns((Mark _) => Task.CompletedTask);

            return (unitOfWork, markRepo, dbCollection);
        }
        
        [Test]
        public async Task CreateMark_FullInfo_Success()
        {
            // Arrange
            var (unitOfWork, markRepo, dbCollection) = GetMocks();
            var service = new MarkService(unitOfWork.Object);
            var mark = new Mark
            {
                Id = 28,
                Grade = 2
            };

            // Act
            await service.CreateMark(mark);

            // Assert
            Assert.IsTrue(dbCollection.ContainsKey(mark.Id));
        }
        
        [Test]
        public void CreateMark_NullObject_NullReferenceException()
        {
            // Arrange
            var (unitOfWork, markRepo, dbCollection) = GetMocks();
            var service = new MarkService(unitOfWork.Object);

            // Act + Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await service.CreateMark(null));
        }
    }
}
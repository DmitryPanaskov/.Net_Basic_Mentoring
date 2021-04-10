using FileSystemVisitor.Enums;
using FileSystemVisitor.Library;
using FileSystemVisitor.Library.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.IO;

namespace FileSystemValidator.Tests
{
    [TestFixture]
    public class FileSystemProcessingStrategyTests
    {
        private IFileSystemProcessingStrategy _strategy;
        private Mock<FileSystemInfo> _fileSystemInfoMock;

        [SetUp]
        public void TestInit()
        {
            _strategy = new FileSystemProcessingStrategy();
            _fileSystemInfoMock = new Mock<FileSystemInfo>();
        }

        [Test]
        public void ItemFindedCall()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int delegatesCallCount = 0;

            // Assert
            _strategy.ProcessItemFinded(fileSystemInfo, null, (s, e) => delegatesCallCount++, null, OnEvent);

            // Arrange
            Assert.AreEqual(1, delegatesCallCount);
        }

        [Test]
        public void FilteredItemFindedCall()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int delegatesCallCount = 0;

            // Assert
            _strategy.ProcessItemFinded(
                fileSystemInfo, info => true, (s, e) => delegatesCallCount++, (s, e) => delegatesCallCount++, OnEvent);

            // Arrange
            Assert.AreEqual(2, delegatesCallCount);
        }

        [Test]
        public void ItemNotPassFilter()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int delegatesCallCount = 0;

            // Assert
            _strategy.ProcessItemFinded(
                fileSystemInfo, info => false, (s, e) => delegatesCallCount++, (s, e) => delegatesCallCount++, OnEvent);

            // Arrange
            Assert.AreEqual(1, delegatesCallCount);
        }

        [Test]
        public void ItemFinded_ContinueSearchAction()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;

            // Assert
            var action = _strategy.ProcessItemFinded(fileSystemInfo, null, (s, e) => { }, null, OnEvent);

            // Arrange
            Assert.AreEqual(ActionType.ContinueSearch, action);
        }

        [Test]
        public void FilteredItemFinded_ContinueSearchAction()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;

            // Assert
            var action = _strategy.ProcessItemFinded(
                fileSystemInfo, info => true, (s, e) => { }, (s, e) => { }, OnEvent);

            // Arrange
            Assert.AreEqual(ActionType.ContinueSearch, action);
        }

        [Test]
        public void FindedItemSkipped_SkipElementAction()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int delegatesCallCount = 0;

            // Assert
            var action = _strategy.ProcessItemFinded(
                fileSystemInfo, info => true, (s, e) =>
                {
                    delegatesCallCount++;
                    e.ActionType = ActionType.SkipElement;
                }, (s, e) => delegatesCallCount++, OnEvent);

            // Arrange
            Assert.AreEqual(ActionType.SkipElement, action);
            Assert.AreEqual(1, delegatesCallCount);
        }

        [Test]
        public void FilteredFindedItemSkipped_SkipElementAction()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int delegatesCallCount = 0;

            // Assert
            var action = _strategy.ProcessItemFinded(
                fileSystemInfo, info => true,
                (s, e) => delegatesCallCount++,
                (s, e) =>
                {
                    delegatesCallCount++;
                    e.ActionType = ActionType.SkipElement;
                }, OnEvent);

            // Arrange
            Assert.AreEqual(ActionType.SkipElement, action);
            Assert.AreEqual(2, delegatesCallCount);
        }

        [Test]
        public void FindedItemStopped_StopSearchAction()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int delegatesCallCount = 0;

            // Assert
            var action = _strategy.ProcessItemFinded(
                fileSystemInfo, info => true, (s, e) =>
                {
                    delegatesCallCount++;
                    e.ActionType = ActionType.StopSearch;
                }, (s, e) => delegatesCallCount++, OnEvent);

            // Arrange
            Assert.AreEqual(ActionType.StopSearch, action);
            Assert.AreEqual(1, delegatesCallCount);
        }

        [Test]
        public void FilteredFindedItemStopped_StopSearchAction()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int delegatesCallCount = 0;

            // Assert
            var action = _strategy.ProcessItemFinded(
                fileSystemInfo, info => true,
                (s, e) => delegatesCallCount++,
                (s, e) =>
                {
                    delegatesCallCount++;
                    e.ActionType = ActionType.StopSearch;
                }, OnEvent);

            // Arrange
            Assert.AreEqual(ActionType.StopSearch, action);
            Assert.AreEqual(2, delegatesCallCount);
        }

        private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)
        {
            someEvent?.Invoke(this, args);
        }
    }
}

using System;
using System.IO;
using FileSystemVisitor.Enums;
using FileSystemVisitor.Library;
using FileSystemVisitor.Library.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace FileSystemValidatorTests
{
    [TestFixture]
    public class FileSystemProcessingStrategyTests
    {
        private IFileSystemProcessingStrategy _strategy;
        private Mock<FileSystemInfo> _fileSystemInfoMock;

        [OneTimeSetUp]
        public void TestInit()
        {
            _strategy = new FileSystemProcessingStrategy();
            _fileSystemInfoMock = new Mock<FileSystemInfo>();
        }

        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsNullAndItemIsNullAndFilteredItemIsNull_ShouldReturnActionTypeIsContinueSearch()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                null,
                null,
                null,
                OnEvent);

            // Arrange
            actionType.Should().BeEquivalentTo(ActionType.ContinueSearch);
        }

        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsNullAndItemIsNotNullAndFilteredItemIsNull_ShouldReturnActionTypeIsContinueSearchAndItemDelegateIsOne()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                null,
                (s, e) => itemDelegate++,
                null,
                OnEvent);

            // Arrange
            Assert.AreEqual(1, itemDelegate);
            actionType.Should().BeEquivalentTo(ActionType.ContinueSearch);
        }

        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsNullAndItemIsNullAndFilteredItemIsNull_ShouldReturnActionTypeIsContinueSearchAndItemDelegateIsOne()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                null,
                null,
                (s, e) => itemDelegate++,
                OnEvent);

            // Arrange
            itemDelegate.Should().Be(0);
            actionType.Should().BeEquivalentTo(ActionType.ContinueSearch);
        }


        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsNullAndItemAndFilteredItemsAreNotNull_ShouldReturnActionTypeIsContinueSearchAndItemDelegateIsOne()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                null,
                (s, e) => itemDelegate++,
                (s, e) => itemDelegate++,
                OnEvent);

            // Arrange
            itemDelegate.Should().Be(1);
            actionType.Should().BeEquivalentTo(ActionType.ContinueSearch);
        }

        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsTrueAndItemIsNotNullAndFilteredItemIsNull_ShouldReturnActionTypeIsContinueSearchAndItemDelegateIsOne()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                (filter) => true,
                (s, e) => itemDelegate++,
                null,
                OnEvent);

            // Arrange
            itemDelegate.Should().Be(1);
            actionType.Should().BeEquivalentTo(ActionType.ContinueSearch);
        }

        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsTrueAndItemIsNullAndFilteredItemIsNull_ShouldReturnActionTypeIsContinueSearchAndItemDelegateIsOne()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                (filter) => true,
                null,
                (s, e) => itemDelegate++,
                OnEvent);

            // Arrange
            itemDelegate.Should().Be(1);
            actionType.Should().BeEquivalentTo(ActionType.ContinueSearch);
        }


        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsTrueAndItemAndFilteredItemsAreNotNull_ShouldReturnActionTypeIsContinueSearchAndItemDelegateIsTwo()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                (filter) => true,
                (s, e) => itemDelegate++,
                (s, e) => itemDelegate++,
                OnEvent);

            // Arrange
            itemDelegate.Should().Be(2);
            actionType.Should().BeEquivalentTo(ActionType.ContinueSearch);
        }

        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsFalseAndItemIsNotNullAndFilteredItemIsNull_ShouldReturnActionTypeIsContinueSearchAndItemDelegateIsOne()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                (filter) => false,
                (s, e) => itemDelegate++,
                null,
                OnEvent);

            // Arrange
            itemDelegate.Should().Be(1);
            actionType.Should().BeEquivalentTo(ActionType.SkipElement);
        }

        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsFalseAndItemIsNullAndFilteredItemIsNull_ShouldReturnActionTypeIsContinueSearchAndItemDelegateIsOne()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                (filter) => true,
                null,
                (s, e) => itemDelegate++,
                OnEvent);

            // Arrange
            itemDelegate.Should().Be(1);
            actionType.Should().BeEquivalentTo(ActionType.ContinueSearch);
        }

        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsFalseAndItemAndFilteredItemsAreNotNull_ShouldReturnActionTypeIsContinueSearchAndItemDelegateIsTwo()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                (filter) => true,
                (s, e) => itemDelegate++,
                (s, e) => itemDelegate++,
                OnEvent);

            // Arrange
            itemDelegate.Should().Be(2);
            actionType.Should().BeEquivalentTo(ActionType.ContinueSearch);
        }

        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsTrueAndItemAndFilteredItemsAreNotNull_ShouldReturnActionTypeIsSkipElementAndItemDelegateIsOne()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                filter => true,
                (s, e) =>
                {
                    itemDelegate++;
                    e.ActionType = ActionType.SkipElement;
                },
                (s, e) => itemDelegate++,
                OnEvent);

            // Arrange
            itemDelegate.Should().Be(1);
            actionType.Should().BeEquivalentTo(ActionType.SkipElement);
        }

        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsFalseAndItemAndFilteredItemsAreNotNull_ShouldReturnActionTypeIsSkipElementAndItemDelegateIsOne()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                filter => true,
                (s, e) =>
                {
                    itemDelegate++;
                    e.ActionType = ActionType.SkipElement;
                },
                (s, e) => itemDelegate++,
                OnEvent);

            // Arrange
            itemDelegate.Should().Be(1);
            actionType.Should().BeEquivalentTo(ActionType.SkipElement);
        }

        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsTrueAndItemAndFilteredItemsAreNotNull_ShouldReturnActionTypeIsStopSearchAndItemDelegateIsOne()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                filter => true,
                (s, e) =>
                {
                    itemDelegate++;
                    e.ActionType = ActionType.StopSearch;
                },
                (s, e) => itemDelegate++,
                OnEvent);

            // Arrange
            itemDelegate.Should().Be(1);
            actionType.Should().BeEquivalentTo(ActionType.StopSearch);
        }

        [Test]
        public void CallProcessOfSearchingItem_WhenFilterIsTrueAndItemAndFilteredItemsAreNotNull_ShouldReturnActionTypeIsStopSearchAndItemDelegateIsTwo()
        {
            // Act
            FileSystemInfo fileSystemInfo = _fileSystemInfoMock.Object;
            int itemDelegate = 0;

            // Assert
            var actionType = _strategy.ProcessOfSearchingItem(
                fileSystemInfo,
                filter => true,
                (s, e) => itemDelegate++,
                (s, e) =>
                {
                    itemDelegate++;
                    e.ActionType = ActionType.StopSearch;
                }, OnEvent);

            // Arrange
            itemDelegate.Should().Be(2);
            actionType.Should().BeEquivalentTo(ActionType.StopSearch);
        }

        private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)
        {
            someEvent?.Invoke(this, args);
        }
    }
}

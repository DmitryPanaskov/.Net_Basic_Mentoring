using System;
using System.IO;
using System.Linq;
using FileSystemVisitor.Library.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace FileSystemVisitorTests
{
    using FileSystemVisitor = FileSystemVisitor.Library.FileSystemVisitor;

    [TestFixture]
    public class FileSystemVisitorTests
    {
        string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\src\Module3\", "Cars"));

        [Test]
        public void GetFileSystemInfoSequence_WhenPathIsNull_ThrowDirectoryNotFoundException()
        {
            // Arrange
            var visitor = new FileSystemVisitor(null);

            // Act
            Action act = () => visitor.GetFileSystemInfoSequence().First();

            // Assert
            act.Should().Throw<DirectoryNotFoundException>().WithMessage($"Directory not found: {null}");
        }

        [Test]
        public void GetFileSystemInfoSequence_WhenFilterIsNull_ShouldReturnEnumerable()
        {
            // Arrange
            var visitor = new FileSystemVisitor(path);

            // Act
            var act = visitor.GetFileSystemInfoSequence().First();

            // Assert
            act.Name.Should().BeEquivalentTo("BMW");
        }

        [Test]
        public void GetFileSystemInfoSequence_WhenFilterIsNullWithEvents_ShouldReturnEnumerable()
        {
            // Arrange
            var visitor = new FileSystemVisitor(path);

            visitor.FilteredDirectoryFound += (s, e) =>
                {
                    e.ActionType = ActionType.StopSearch;
                };

            visitor.FilteredFileFound += (s, e) =>
                {
                    e.ActionType = ActionType.SkipElement;
                };

            // Act
            var act = visitor.GetFileSystemInfoSequence().ToList();

            // Assert
            act.Should().Contain(x => x.Name == "BMW");
            act.Should().Contain(x => x.Name == "Mercedes-Benz");
            act.Should().Contain(x => x.Name == "VAG");
            act.Should().Contain(x => x.Name == "q7.txt");
            act.Should().Contain(x => x.Name == "q5.txt");
        }

        [Test]
        public void GetFileSystemInfoSequence_WhenWasSkippedDirectory_ShouldReturnEnumerableWithoutTheDirectory()
        {
            // Arrange
            var visitor = new FileSystemVisitor(path, x => x.Name == "VAG");
            var checkerDirectory = visitor.GetFileSystemInfoSequence().ToList().Any(x => x.Name == "VAG");

            visitor.GetFileSystemInfoSequence();

            visitor.FilteredDirectoryFound += (s, e) =>
                {
                    e.ActionType = ActionType.SkipElement;
                };

            // Act
            var act = visitor.GetFileSystemInfoSequence().ToList();

            // Assert
            checkerDirectory.Should().BeTrue();
            act.Should().NotContain(x => x.Name == "VAG");
        }

        [Test]
        public void GetFileSystemInfoSequence_WhenWasStoppedDirectory_ShouldReturnEnumerableWithoutTheDirectory()
        {
            // Arrange
            var visitor = new FileSystemVisitor(path, x => x.Name == "BMW");
            var list = visitor.GetFileSystemInfoSequence().ToList();
            var checkerDirectories = list.Any(x => x.Name == "BMW") &&
                                   list.Any(x => x.Name == "Mercedes-Benz") &&
                                   list.Any(x => x.Name == "VAG") &&
                                   list.Any(x => x.Name == "q5.txt") &&
                                   list.Any(x => x.Name == "q7.txt");

            visitor.GetFileSystemInfoSequence();

            visitor.FilteredDirectoryFound += (s, e) =>
                {
                    e.ActionType = ActionType.StopSearch;
                };

            // Act
            var act = visitor.GetFileSystemInfoSequence().ToList();

            // Assert
            checkerDirectories.Should().BeTrue();
            act.Should().NotContain(x => x.Name == "BMW");
            act.Should().NotContain(x => x.Name == "Mercedes-Benz");
            act.Should().NotContain(x => x.Name == "VAG");
            act.Should().NotContain(x => x.Name == "q7.txt");
            act.Should().NotContain(x => x.Name == "q5.txt");
        }

        [Test]
        public void GetFileSystemInfoSequence_WhenWasSkippedFile_ShouldReturnEnumerableWithoutTheElement()
        {
            // Arrange
            var visitor = new FileSystemVisitor(path, x => x.Name == "e39.txt");
            var checkerFile = visitor.GetFileSystemInfoSequence().ToList().Any(x => x.Name == "e39.txt");

            visitor.GetFileSystemInfoSequence();

            visitor.FilteredFileFound += (s, e) =>
                {
                    e.ActionType = ActionType.SkipElement;
                };

            // Act
            var act = visitor.GetFileSystemInfoSequence().ToList();

            // Assert
            checkerFile.Should().BeTrue();
            act.Should().NotContain(x => x.Name == "e39.txt");
        }

        [Test]
        public void GetFileSystemInfoSequence_WhenWasStoppedFile_ShouldReturnEnumerableWithoutTheElement()
        {
            // Arrange
            var visitor = new FileSystemVisitor(path, x => x.Name == "q5.txt");
            var list = visitor.GetFileSystemInfoSequence().ToList();
            var checkerFiles = list.Any(x => x.Name == "q5.txt")
                              && list.Any(x => x.Name == "q7.txt");

            visitor.GetFileSystemInfoSequence();

            visitor.FilteredFileFound += (s, e) =>
                {
                    e.ActionType = ActionType.StopSearch;
                };

            // Act
            var act = visitor.GetFileSystemInfoSequence().ToList();

            // Assert
            checkerFiles.Should().BeTrue();
            act.Should().NotContain(x => x.Name == "q5.txt");
            act.Should().NotContain(x => x.Name == "q7.txt");
        }
    }
}

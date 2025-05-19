using System;
using ApplicantTracking.Domain.Models;
using Xunit;

namespace ApplicantTracking.UnitTests.Domain
{
    public class CandidateTests
    {
        /// <summary>
        /// Verifies that creating a <see cref="Candidate"/> via its constructor:
        /// - Initializes the <see cref="Candidate.Name"/>, <see cref="Candidate.Surname"/>, <see cref="Candidate.Birthdate"/> and <see cref="Candidate.Email"/> properties with the supplied values,
        /// - Sets the <see cref="Candidate.Id"/> to the provided identifier,
        /// - Assigns <see cref="Candidate.CreatedAt"/> to a timestamp at or before the current UTC time,
        /// - And leaves <see cref="Candidate.LastUpdatedAt"/> as null.
        /// </summary>
        
        [Fact]
        public void Constructor_Should_Initialize_Properties_And_CreatedAt()
        {
            // Arrange
            var id = new int();
            var name = "Kobe";
            var surname = "Bryant";
            var birthdate = new DateTime(1978, 8, 23);
            var email = "kb_legend@gmail.com.com";

            // Act
            var candidate = new Candidate(id, name, surname, birthdate, email);

            // Assert
            Assert.Equal(name, candidate.Name);
            Assert.Equal(surname, candidate.Surname);
            Assert.Equal(birthdate, candidate.Birthdate);
            Assert.Equal(email, candidate.Email);
            Assert.Equal(id, candidate.Id);             
            Assert.True(candidate.CreatedAt <= DateTime.UtcNow);
            Assert.Null(candidate.LastUpdatedAt);
        }
        /// <summary>
        /// Verifies that calling <see cref="Candidate.Update(string, string, DateTime, string)"/>:
        /// - Updates the <see cref="Candidate.Name"/>, <see cref="Candidate.Surname"/>, <see cref="Candidate.Birthdate"/>, and <see cref="Candidate.Email"/> properties to the new values,
        /// - Sets <see cref="Candidate.LastUpdatedAt"/> to a non-null timestamp,
        /// - And ensures that <see cref="Candidate.LastUpdatedAt"/> is strictly greater than the original <see cref="Candidate.CreatedAt"/> timestamp.
        /// </summary>
        [Fact]
        public void Update_Should_Change_Properties_And_SetLastUpdatedAt()
        {
            // Arrange
            var candidate = new Candidate(new int(), "Kobe", "Bryant", new DateTime(2000, 1, 1), "kb_legend@gmail.com");
            var beforeUpdate = candidate.CreatedAt;

            // Act
            candidate.Update("Kobe Bean", "Bryant", new DateTime(2001, 2, 2), "mamba_mentality@gmail.com");

            // Assert
            Assert.Equal("Kobe Bean", candidate.Name);
            Assert.Equal("Bryant", candidate.Surname);
            Assert.Equal(new DateTime(2001, 2, 2), candidate.Birthdate);
            Assert.Equal("mamba_mentality@gmail.com", candidate.Email);
            Assert.NotNull(candidate.LastUpdatedAt);
            Assert.True(candidate.LastUpdatedAt > beforeUpdate);
        }


        /// <summary>
        /// Ensures that two <see cref="Candidate"/> instances with the same <see cref="EntityBase.Id"/> are considered equal
        /// and produce the same hash code.
        /// </summary>
        [Fact]
        public void Equals_Should_Return_True_For_Same_Id()
        {
            // Arrange
            var c1 = new Candidate(new int(), "A", "B", DateTime.Today, "a@b.com");
            var c2 = new Candidate(new int(), "X", "Y", DateTime.Today, "x@y.com");

            typeof(Entity)
                .GetProperty("Id", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
                .SetValue(c1, 100);
            typeof(Entity)
                .GetProperty("Id", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
                .SetValue(c2, 100);

            // Act & Assert
            Assert.True(c1.Equals(c2));
            Assert.Equal(c1.GetHashCode(), c2.GetHashCode());
        }
    }
}

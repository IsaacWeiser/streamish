using Streamish.Controllers;
using Streamish.Models;
using Streamish.test.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Streamish.test
{
    public class UserProfileControllerTests 
    {

        [Fact]
        public void Get_Returns_All_UserProfiles()
        {
            // Arrange 
            var profileCount = 20;
            var profiles = CreateTestProfiles(profileCount);

            var repo = new InMemoryUserProfileRepository(profiles);
            var controller = new UserProfileController(repo);

            // Act 
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUserProfiles = Assert.IsType<List<UserProfile>>(okResult.Value);

            Assert.Equal(profileCount, actualUserProfiles.Count);
            Assert.Equal(profiles, actualUserProfiles);
        }

        [Fact]
        public void Get_By_Id_Returns_NotFound_When_Given_Unknown_id()
        {
            // Arrange 
            var profiles = new List<UserProfile>(); // no profiles

            var repo = new InMemoryUserProfileRepository(profiles);
            var controller = new UserProfileController(repo);

            // Act
            var result = controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_By_Id_Returns_UserProfile_With_Given_Id()
        {
            // Arrange
            var testUserProfileId = 99;
            var profiles = CreateTestProfiles(5);
            profiles[0].Id = testUserProfileId; // Make sure we know the Id of one of the profiles

            var repo = new InMemoryUserProfileRepository(profiles);
            var controller = new UserProfileController(repo);

            // Act
            var result = controller.Get(testUserProfileId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUserProfile = Assert.IsType<UserProfile>(okResult.Value);

            Assert.Equal(testUserProfileId, actualUserProfile.Id);
        }

        [Fact]
        public void Post_Method_Adds_A_New_UserProfile()
        {
            // Arrange 
            var profileCount = 20;
            var profiles = CreateTestProfiles(profileCount);

            var repo = new InMemoryUserProfileRepository(profiles);
            var controller = new UserProfileController(repo);

            // Act
            var newUserProfile = new UserProfile()
            {
                Name = "Name",
                Email = "Email",
                ImageUrl = "http://youtube.url?v=1234",
                DateCreated = DateTime.Today,
               
            };

            controller.Post(newUserProfile);

            // Assert
            Assert.Equal(profileCount + 1, repo.InternalData.Count);
        }

        [Fact]
        public void Put_Method_Returns_BadRequest_When_Ids_Do_Not_Match()
        {
            // Arrange
            var testUserProfileId = 99;
            var profiles = CreateTestProfiles(5);
            profiles[0].Id = testUserProfileId; // Make sure we know the Id of one of the profiles

            var repo = new InMemoryUserProfileRepository(profiles);
            var controller = new UserProfileController(repo);

            var profileToUpdate = new UserProfile()
            {
                Id = testUserProfileId,
                Name = "Updated!",
                Email = "Updated!",
                ImageUrl = "asdfqwerty",
                DateCreated = DateTime.Today,
                
            };
            var someOtherUserProfileId = testUserProfileId + 1; // make sure they aren't the same

            // Act
            var result = controller.Put(someOtherUserProfileId, profileToUpdate);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Put_Method_Updates_A_UserProfile()
        {
            // Arrange
            var testUserProfileId = 99;
            var profiles = CreateTestProfiles(5);
            profiles[0].Id = testUserProfileId; // Make sure we know the Id of one of the profiles

            var repo = new InMemoryUserProfileRepository(profiles);
            var controller = new UserProfileController(repo);

            var profileToUpdate = new UserProfile()
            {
                Id = testUserProfileId,
                Name = "Updated!",
                ImageUrl = "Updated!",
                DateCreated = DateTime.Today,
                
            };

            // Act
            controller.Put(testUserProfileId, profileToUpdate);

            // Assert
            var profileFromDb = repo.InternalData.FirstOrDefault(p => p.Id == testUserProfileId);
            Assert.NotNull(profileFromDb);
            Assert.Equal(profileToUpdate.Name, profileFromDb.Name);
            Assert.Equal(profileToUpdate.ImageUrl, profileFromDb.ImageUrl);           
            Assert.Equal(profileToUpdate.DateCreated, profileFromDb.DateCreated);
            
        }

        [Fact]
        public void Delete_Method_Removes_A_UserProfile()
        {
            // Arrange
            var testUserProfileId = 99;
            var profiles = CreateTestProfiles(5);
            profiles[0].Id = testUserProfileId; // Make sure we know the Id of one of the profiles

            var repo = new InMemoryUserProfileRepository(profiles);
            var controller = new UserProfileController(repo);

            // Act
            controller.Delete(testUserProfileId);

            // Assert
            var videoFromDb = repo.InternalData.FirstOrDefault(p => p.Id == testUserProfileId);
            Assert.Null(videoFromDb);
        }

        private List<UserProfile> CreateTestProfiles(int count)
        {
            var profiles = new List<UserProfile>();
            for (var i = 1; i <= count; i++)
            {
                profiles.Add(new UserProfile()
                {
                    Id = i,
                    Name = $"Name {i}",
                    ImageUrl = $"ImageUrl {i}",
                    DateCreated = DateTime.Today.AddDays(-i)
                });
            }
            return profiles;
        }

        private UserProfile CreateTestUserProfile(int id)
        {
            return new UserProfile()
            {
                Id = id,
                Name = $"User {id}",
                Email = $"user{id}@example.com",
                DateCreated = DateTime.Today.AddDays(-id),
                ImageUrl = $"http://user.url/{id}",
            };

        }
        
    }
}

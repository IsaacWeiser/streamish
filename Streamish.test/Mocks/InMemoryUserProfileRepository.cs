using Streamish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streamish.Repositories;

namespace Streamish.test.Mocks
{
    internal class InMemoryUserProfileRepository : IUserProfileRepository
    {
        private readonly List<UserProfile> _data;

        public List<UserProfile> InternalData
        {
            get
            {
                return _data;
            }
        }

        public InMemoryUserProfileRepository(List<UserProfile> startingData)
        {
            _data = startingData;
        }

        public void Add(UserProfile profile)
        {
            var lastUserProfile = _data.Last();
            profile.Id = lastUserProfile.Id + 1;
            _data.Add(profile);
        }

        public void Delete(int id)
        {
            var profileToDelete = _data.FirstOrDefault(p => p.Id == id);
            if (profileToDelete == null)
            {
                return;
            }

            _data.Remove(profileToDelete);
        }

        public List<UserProfile> GetAll()
        {
            return _data;
        }

        public UserProfile GetById(int id)
        {
            return _data.FirstOrDefault(p => p.Id == id);
        }

        public void Update(UserProfile profile)
        {
            var currentUserProfile = _data.FirstOrDefault(p => p.Id == profile.Id);
            if (currentUserProfile == null)
            {
                return;
            }

            currentUserProfile.Name = profile.Name;
            currentUserProfile.Email = profile.Email;
            currentUserProfile.DateCreated = profile.DateCreated;
            currentUserProfile.ImageUrl = profile.ImageUrl;
            currentUserProfile.DateCreated = profile.DateCreated;
        }

        public UserProfile GetUserVids(int id)
        {
            throw new NotImplementedException();
        }

    }
}

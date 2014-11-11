using System.Collections.Generic;
using Nancy.CollectionJson.Demo.ViewModels;

namespace Nancy.CollectionJson.Demo.Models
{
    public interface IFriendRepository
    {
        IEnumerable<Friend> GetAll();

        Friend Get(int id);

        int Add(Friend friend);

        void Update(Friend friend);

        void Remove(int id);

        IEnumerable<Friend> Search(string name);
    }
}

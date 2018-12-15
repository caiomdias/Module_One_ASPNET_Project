using AtWebAsp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtWebAsp.Interfaces
{
    interface IFriend
    {
        List<Friend> ListFriends();
		bool AddFriend(Friend friend);
		bool DeleteFriend(int friendId);
		bool UpdateFriend(Friend friend);
		Friend GetFriendById(int friendId);
	}
}

using AtWebAsp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtWebAsp.Helpers
{
	public class FriendsHelpers
	{
		public List<Friend> ListToDayBirthday(List<Friend> friends)
		{
			List<Friend> toDayBirthday = new List<Friend>();

			DateTime toDay = DateTime.Now;

			foreach (var friend in friends)
			{
				{
					if ((friend.FriendBirthDate.Day == toDay.Day) && (friend.FriendBirthDate.Month == toDay.Month))
						toDayBirthday.Add(friend);
				}
			}

			return toDayBirthday;
		}

		public List<Friend> OrderFriendsByBirthDate(List<Friend> friends)
		{
			return friends.OrderBy(el => el.FriendDaysToBirthDate).ToList();
		}

	}
}
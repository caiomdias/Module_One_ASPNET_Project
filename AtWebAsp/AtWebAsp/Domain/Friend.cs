using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtWebAsp.Domain
{
    public class Friend
    {
        public Friend()
        {
        }

        public Friend(int friendId, string friendName, string friendLastName, DateTime friendBirthDate, int friendDaysToBirthDate)
        {
            FriendId = friendId;
            FriendName = friendName;
            FriendLastName = friendLastName;
            FriendBirthDate = friendBirthDate.Date;
			FriendDaysToBirthDate = friendDaysToBirthDate;


		}

        public int FriendId { get; set; }
        public string FriendName { get; set; }
        public string FriendLastName { get; set; }
        public DateTime FriendBirthDate { get; set; }
		public int FriendDaysToBirthDate { get; set; }


		public int GetDaysToBirthDate()
		{
			var finalDate = DateTime.Now;
			var initialDate = new DateTime(DateTime.Now.Year +1, FriendBirthDate.Month, FriendBirthDate.Day);

			TimeSpan response = initialDate - finalDate;

			return response.Days;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtWebAsp.Models
{
    public class FriendViewModel
    {
        public int FriendId { get; set; }
        public string FriendName { get; set; }
        public string FriendLastName { get; set; }
        public DateTime FriendBirthDate { get; set; }
		public int FriendDaysToBirthDate { get; set; }
	}
}
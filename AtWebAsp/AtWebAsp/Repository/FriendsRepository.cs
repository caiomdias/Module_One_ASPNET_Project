using AtWebAsp.Domain;
using AtWebAsp.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AtWebAsp.Repository
{
    public class FriendsRepository : IFriend
    {
		string DB_CONNECT = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = '" + AppDomain.CurrentDomain.BaseDirectory + "App_Data\\Database1.mdf'; Integrated Security = True";
		//string DB_CONNECT = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\caiom\source\repos\AtWebAsp\AtWebAsp\App_Data\Database1.mdf;Integrated Security=True";

        public List<Friend> ListFriends()
        {

            using (var connection = new SqlConnection(DB_CONNECT))
            {
                var comamd = "SELECT * FROM Friend";
                var SELECT = new SqlCommand(comamd, connection);
                var friends = new List<Friend>();

                try
                {
                    connection.Open();

                    using (var response = SELECT.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (response.Read())
                        {
                            Friend friend = new Friend();

                            friend.FriendId = (int)response["FriendId"];
                            friend.FriendName = response["Friendname"].ToString();
                            friend.FriendLastName = response["FriendLastName"].ToString();
                            friend.FriendBirthDate = (DateTime)response["FriendBirthDate"];
							friend.FriendDaysToBirthDate = (int)response["FriendDaysToBirthDate"];

							friends.Add(friend);
                        }
                    }

                    
                }
                finally
                {
                    connection.Close();
                }

                return friends;
            }
        }

        public bool AddFriend(Friend friend)
        {
            bool ret = false;

            var conn = new SqlConnection(DB_CONNECT);

			string command = "INSERT INTO Friend (FriendName, FriendLastName, FriendBirthDate, FriendDaysToBirthDate) " +
				"VALUES (" +
							"'" + friend.FriendName + "', " +
							"'" + friend.FriendLastName + "', " +
							"'" + friend.FriendBirthDate.ToString("yyyy-MM-dd") + "', " +
							"'" + friend.FriendDaysToBirthDate + "')";

			var insertCommand = new SqlCommand(command, conn);

            try
            {
                conn.Open();
                insertCommand.ExecuteReader(CommandBehavior.CloseConnection);
                ret = true;
            }
            finally
            {
                conn.Close();
            }

            return ret;
        }

		public bool DeleteFriend(int FriendID)
		{
			bool ret = false;

			var connection = new SqlConnection(DB_CONNECT);

			string command = $"DELETE FROM Friend WHERE FriendId = {FriendID}";

			var deleteCommand = new SqlCommand(command, connection);

			try
			{
				connection.Open();
				deleteCommand.ExecuteReader(CommandBehavior.CloseConnection);
				ret = true;
			}
			finally
			{
				connection.Close();
			}

			return ret;
		}

		public bool UpdateFriend(Friend friend)
		{
			bool ret = false;

			var connection = new SqlConnection(DB_CONNECT);

			string command = $"UPDATE Friend SET FriendName = '{friend.FriendName}', FriendLastName = '{friend.FriendLastName}', FriendBirthDate = '{friend.FriendBirthDate.ToString("yyyy-MM-dd")}', FriendDaysToBirthDate = '{friend.FriendDaysToBirthDate}' WHERE FriendID = {friend.FriendId}";

			var updateCommand = new SqlCommand(command, connection);

			try
			{
				connection.Open();

				updateCommand.ExecuteReader(CommandBehavior.CloseConnection);

				ret = true;
			}
			finally
			{
				connection.Close();
			}

			return ret;
		}
		
		public Friend GetFriendById(int friendId)
		{
			using (var connection = new SqlConnection(DB_CONNECT))
			{
				var comamd = $"SELECT * FROM Friend WHERE FriendId={(int)friendId}";

				var SELECTBYID = new SqlCommand(comamd, connection);

				var friend = new Friend();

				try
				{
					connection.Open();

					using (var response = SELECTBYID.ExecuteReader(CommandBehavior.CloseConnection))
					{
						if (response.Read())
						{
							friend.FriendId = (int)response["FriendId"];
							friend.FriendName = response["FriendName"].ToString();
							friend.FriendLastName = response["FriendLastName"].ToString();
							friend.FriendBirthDate = (DateTime)response["FriendBirthDate"];
							friend.FriendDaysToBirthDate = (int)response["FriendDaysToBirthDate"];

						}
					}
				}
				finally
				{
					connection.Close();
				}

				return friend;
			}
		}
	}
}
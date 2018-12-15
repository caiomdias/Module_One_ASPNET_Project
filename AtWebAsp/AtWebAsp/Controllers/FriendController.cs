using AtWebAsp.Domain;
using AtWebAsp.Helpers;
using AtWebAsp.Models;
using AtWebAsp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtWebAsp.Controllers
{
    public class FriendController : Controller
    {
		FriendsRepository repository = new FriendsRepository();
		FriendsHelpers friendsHelpers = new FriendsHelpers();

		// INDEX: Friend
		public ActionResult Index()
        {
			List<Friend> friends = repository.ListFriends();

			List<Friend> todayBirthDay = friendsHelpers.ListToDayBirthday(friends);

			ViewBag.FriendsOrderedByBirthDate = friendsHelpers.OrderFriendsByBirthDate(friends);

			ViewBag.dateNow = DateTime.Now;

			return View(
                todayBirthDay.Select(el => new FriendViewModel
                {
                    FriendId = el.FriendId,
                    FriendName = el.FriendName,
                    FriendLastName = el.FriendLastName,
                    FriendBirthDate = el.FriendBirthDate,
                }));
        }

		public ActionResult ListView()
		{

			var friends = repository.ListFriends();

			return View(
				friends.Select(el => new FriendViewModel
				{
					FriendId = el.FriendId,
					FriendName = el.FriendName,
					FriendLastName = el.FriendLastName,
					FriendBirthDate = el.FriendBirthDate,
					FriendDaysToBirthDate = el.FriendDaysToBirthDate,
				}));
		}

		// GET: Friend/Details/{id}
		public ActionResult Details(int id)
        {
			var friendDetails = repository.GetFriendById(id);

			return View(new FriendViewModel
			{
				FriendId = friendDetails.FriendId,
				FriendName = friendDetails.FriendName,
				FriendLastName = friendDetails.FriendLastName,
				FriendBirthDate = friendDetails.FriendBirthDate,
				FriendDaysToBirthDate = friendDetails.FriendDaysToBirthDate,
			});
		}

        // GET: Friend/CreateView
        public ActionResult CreateView()
        {
            return View("Create");
        }

        // POST: Friend/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var id = 0;

                if (string.IsNullOrWhiteSpace(collection["FriendId"].ToString()))
                {
                    id = 0;
                }
                else
                {
                    id = int.Parse(collection["FriendId"].ToString());
                }


				string tempName = collection["FriendName"].ToString();
				string tempLastName = collection["FriendLastName"].ToString();
				DateTime tempBirthDate = DateTime.Parse(collection["FriendBirthDate"].ToString());

				Friend friend = new Friend(id, tempName, tempLastName, tempBirthDate, 0);

				friend.FriendDaysToBirthDate = friend.GetDaysToBirthDate();

				repository.AddFriend(friend);


				return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: Friend/Edit/Edit
        public ActionResult Edit(int id)
        {
			var friendToUpdate = repository.GetFriendById(id);

			return View(new FriendViewModel
			   {
				   FriendId = friendToUpdate.FriendId,
				   FriendName = friendToUpdate.FriendName,
				   FriendLastName = friendToUpdate.FriendLastName,
				   FriendBirthDate = friendToUpdate.FriendBirthDate,
			   });
		}

        // POST: Friend/Edit/{FriendId}
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
				var tempId = int.Parse(collection["FriendId"].ToString());
				var tempName = collection["FriendName"].ToString();
				var tempLastName = collection["FriendLastName"].ToString();
				var tempBirthDate = DateTime.Parse(collection["FriendBirthDate"].ToString());

				Friend friend = new Friend(tempId, tempName, tempLastName, tempBirthDate, 0);

				friend.FriendDaysToBirthDate = friend.GetDaysToBirthDate();

				repository.UpdateFriend(friend);

				return RedirectToAction("Index");
            }
            catch (Exception e)
            {
				throw e;
            }
        }

		// POST: Friend/Delete/{FriendId}
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

				repository.DeleteFriend(id);

                return RedirectToAction("ListView");
            }
            catch
            {
                return View();
            }
        }
    }
}

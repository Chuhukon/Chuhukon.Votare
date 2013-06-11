using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Chuhukon.Votare.Model;

namespace Chuhukon.Votare
{
	public class VotareHub : Hub
	{
		private static List<Attendee> _attendees;
		protected List<Attendee> Attendees
		{
			get
			{
				if (_attendees == null)
					_attendees = new List<Attendee>();

				return _attendees;
			}
		}

		public void Attend(string userId)
		{
			if (!string.IsNullOrWhiteSpace(userId))
			{
				var existingAttendee = Attendees.Find(a => a.UserId == userId);

				if (existingAttendee == null)
				{
					Attendees.Add(new Attendee
					{
						UserId = userId,
						Like = true //default;
					});
				}

				SendColor();
			}
		}

		public void Vote(string userId, bool like)
		{
			if (!string.IsNullOrWhiteSpace(userId))
			{
				var existingAttendee = Attendees.Find(a => a.UserId == userId);

				if (existingAttendee != null)
				{
					existingAttendee.Like = like;
					SendColor();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void Start()
		{
			_attendees = new List<Attendee>();
			//todo: send event started to all clients...
		}

		/// <summary>
		/// Set like status back to default values
		/// </summary>
		public void Reset()
		{
			foreach (var attendee in Attendees)
			{
				attendee.Like = true;
			}

			SendColor();
		}

		protected void SendColor()
		{
			var dislikes = Attendees.Count(a => !a.Like);

			if (dislikes > (Attendees.Count / 2))
				Clients.All.changeColor("#DA4F49");
			else
				Clients.All.changeColor("#5BB75B");
		}

		#region Connection Management
		public override System.Threading.Tasks.Task OnConnected()
		{
			return base.OnConnected();
		}

		public override System.Threading.Tasks.Task OnReconnected()
		{
			return base.OnReconnected();
		}

		public override System.Threading.Tasks.Task OnDisconnected()
		{
 			 return base.OnDisconnected();
		}
		#endregion
	}
}
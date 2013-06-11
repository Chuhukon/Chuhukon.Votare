using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chuhukon.Votare.Common;

namespace Chuhukon.Votare
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Page.User.Identity.IsAuthenticated)
			{
				var identity = Page.User.Identity as System.Security.Claims.ClaimsIdentity;
				if (identity != null)
				{
					var id = string.Empty;
					foreach(var claim in identity.Claims)
					{
						id += claim.Value;
					}

					userId.Value = id.ToMd5Hash();
				}
			}
			else
				Response.Redirect("NotAuthenticated.html");
		}
	}
}
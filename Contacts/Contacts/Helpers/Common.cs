using System;
using Contacts.Views;
using Xamarin.Forms;

namespace Contacts
{
	public static class Common
	{
		public static void Logout(Page page)
		{
			var loginPage = new NavigationPage(new LoginView());
			NavigationPage.SetHasNavigationBar(loginPage, false); 
			page.Navigation.PushModalAsync (loginPage);
		}
	}
}


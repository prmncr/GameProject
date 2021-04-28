using System;

namespace GameProject.MainMenu
{
	public class MainMenuController : Controller
	{
		public MainMenuController()
		{
			ViewAbstract = new MainMenuView();
		}
	}
}
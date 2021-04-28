using System.Collections.Generic;
using GameProject.GameControl;
using GameProject.MainMenu;

namespace GameProject
{
	public enum Page
	{
		MainMenu, Game
	}

	internal static class PagesExtensions
	{
		public static Controller GetInstance(this Page page)
		{
			return page switch
			{
				Page.MainMenu => new MainMenuController(),
				Page.Game => new GameController(),
				_ => null,
			};
		}
	}
}
using System.Collections.Generic;
using GameProject.GameControl;
using GameProject.Levels;
using GameProject.LevelSelector;
using GameProject.MainMenu;

namespace GameProject
{
	public enum Page
	{
		MainMenu, Game, Selector
	}

	internal static class PagesExtensions
	{
		public static Controller GetInstance(this Page page, ILevel level)
		{
			return page switch
			{
				Page.MainMenu => new MainMenuController(),
				Page.Game => new GameController(level),
				Page.Selector => new SelectorController(),
				_ => null,
			};
		}
	}
}
using System;
using System.Windows.Forms;

namespace GameProject
{
	public enum Page
	{
		MainMenu,
		Game,
		Selector,
		GameEnd
	}

	internal static class PagesExtensions
	{
		public static Control GetInstance(this Page page, Type level)
		{
			return page switch
			{
				Page.Game => new Game(level),
				Page.Selector => new LevelSelector(),
				Page.GameEnd => new GameEnd(),
				_ => null
			};
		}
	}
}
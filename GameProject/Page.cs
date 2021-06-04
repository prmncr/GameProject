using GameProject.Levels;
using unvell.D2DLib.WinForm;

namespace GameProject
{
	public enum Page
	{
		MainMenu,
		Game,
		Selector
	}

	internal static class PagesExtensions
	{
		public static D2DControl GetInstance(this Page page, Level level)
		{
			return page switch
			{
				Page.MainMenu => new MainMenu(),
				Page.Game => new Game(level),
				Page.Selector => new LevelSelector(),
				_ => null
			};
		}
	}
}
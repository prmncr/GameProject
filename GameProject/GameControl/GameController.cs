using System;

namespace GameProject.GameControl
{
	class GameController : Controller
	{
		public GameModel Model { get; private init; }
		public GameView View { get; private init; }

		public GameController()
		{
			var view = new GameView(this);
			View = view;
			ViewAbstract = view;
			Model = new GameModel();

			Model.Redraw += map => View.Redraw(map);
		}
	}
}
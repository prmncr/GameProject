using System.Windows.Forms;
using GameProject.Levels;

namespace GameProject.GameControl
{
	internal class GameController : Controller
	{
		public GameModel Model { get; }
		public GameView View { get; }

		public GameController(ILevel level)
		{
			Model = new GameModel(level);
			var view = new GameView(this) {Map = Model.Map};

			View = view;
			ViewAbstract = view;
		}

		public void KeyDown(Keys eKeyCode)
		{
			Model.KeyDown(eKeyCode);
		}

		public void KeyUp(Keys eKeyCode)
		{
			Model.KeyUp(eKeyCode);
		}

		public void RequestUpdate()
		{
			Model.Update();
		}
	}
}
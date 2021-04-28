using System.Windows.Forms;

namespace GameProject.GameControl
{
	internal class GameController : Controller
	{
		public GameModel Model { get; }
		public GameView View { get; }

		public GameController()
		{
			Model = new GameModel();
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
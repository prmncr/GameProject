using System.Drawing;
using System.Windows.Forms;
using GameProject.MainMenu;

namespace GameProject.GameControl
{
	class GameView : View
	{
		private Graphics _graphics;

		public GameController Controller { get; protected init; }

		public GameView(Controller controller)
		{
			DoubleBuffered = true;
			Controller = controller as GameController;
			_graphics = CreateGraphics();
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			Controller.Model.KeyDown(e.KeyCode);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			Controller.Model.KeyUp(e.KeyCode);
		}

		public void Redraw(GameMap map)
		{
			_graphics.DrawRectangle(new Pen(Color.Black), (Rectangle) map.Player);
		}
	}
}
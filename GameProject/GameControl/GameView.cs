using System.Drawing;
using System.Windows.Forms;
using unvell.D2DLib;

namespace GameProject.GameControl
{
	internal class GameView : View
	{
		public GameController Controller { get; }
		public GameMap Map { get; set; }

		public GameView(Controller controller)
		{
			Controller = controller as GameController;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			Controller.KeyDown(e.KeyCode);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			Controller.KeyUp(e.KeyCode);
		}

		protected override void OnRender(D2DGraphics g)
		{
			Controller.RequestUpdate();
			g.DrawRectangle((RectangleF) Map.Player, D2DColor.Black);
			Invalidate();
		}
	}
}
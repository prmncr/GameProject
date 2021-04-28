using System.Windows.Forms;
using GameProject.GameObjects;

namespace GameProject.GameControl
{
	internal class GameModel
	{
		private bool _left, _right, _up, _down;

		private readonly Player _player = new();

		public GameMap Map { get; set; }

		public GameModel()
		{
			Map = new GameMap(_player);
		}

		public void KeyDown(Keys eKeyCode)
		{
			switch (eKeyCode)
			{
				case Keys.W:
					_up = true;
					break;
				case Keys.A:
					_left = true;
					break;
				case Keys.S:
					_down = true;
					break;
				case Keys.D:
					_right = true;
					break;
			}
		}

		public void KeyUp(Keys eKeyCode)
		{
			switch (eKeyCode)
			{
				case Keys.W:
					_up = false;
					break;
				case Keys.A:
					_left = false;
					break;
				case Keys.S:
					_down = false;
					break;
				case Keys.D:
					_right = false;
					break;
			}
		}

		public void Update()
		{
			_player.Move(_left, _right, _up, _down);
		}
	}
}
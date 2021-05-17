using System.Windows.Forms;
using GameProject.GameObjects;
using GameProject.Levels;

namespace GameProject.GameControl
{
	internal class GameModel
	{
		private bool _left, _right, _up, _down;
		private readonly Player _player;
		public GameMap Map { get; }

		public GameModel(ILevel level)
		{
			Map = new GameMap(level, out var player);
			_player = player;
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
			Map.MoveEnemies();
		}
	}
}
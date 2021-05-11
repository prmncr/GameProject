using System.Numerics;
using System.Windows.Forms;
using GameProject.GameObjects;
using GameProject.Levels;

namespace GameProject.GameControl
{
	internal class GameModel
	{
		private bool _left, _right, _up, _down;
		private readonly Player _player;
		private float _scaling;
		public GameMap Map { get; set; }

		public GameModel(ILevel level)
		{
			_scaling = 50;
			Map = new GameMap(_scaling, level, out var player);
			_player = player;
			Map.Player = _player; //todo: get lvl name by files in directory
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
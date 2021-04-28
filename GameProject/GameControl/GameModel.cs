using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.GameObjects;

namespace GameProject.GameControl
{
	class GameModel
	{
		private Timer _timer = new();

		private bool _left, _right, _up, _down;

		private Player _player = new();

		private GameMap _map;

		public event Action<GameMap> Redraw;

		public GameModel()
		{
			_map = new GameMap(_player);
			_timer.Interval = 25;
			_timer.Tick += _timerTick;
			_timer.Start();
		}

		private void _timerTick(object sender, EventArgs e)
		{
			_player.Move(_left, _right, _up, _down);
			Redraw?.Invoke(_map);
		}

		public void KeyDown(Keys eKeyCode)
		{
			switch (eKeyCode)
			{
				case Keys.W: _up = true; break;
				case Keys.A: _left = true; break;
				case Keys.S: _down = true; break;
				case Keys.D: _right = true; break;
			}
		}

		public void KeyUp(Keys eKeyCode)
		{
			switch (eKeyCode)
			{
				case Keys.W: _up = false; break;
				case Keys.A: _left = false; break;
				case Keys.S: _down = false; break;
				case Keys.D: _right = false; break;
			}
		}
	}
}

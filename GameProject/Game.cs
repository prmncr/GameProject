using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using GameProject.GameObjects;
using GameProject.Levels;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace GameProject
{
	public class Game : D2DControl
	{
		private readonly IStaticGameObject[][] _levelMap;
		private readonly List<Enemy> _enemies = new();
		private readonly int _levelWidth, _levelHeight;
		private readonly float _scaling;
		private bool _left, _right, _up, _down;
		public readonly ILevel Level;
		private readonly Player _player;

		public Game(ILevel level)
		{
			Level = level;
			_scaling = level.BlockScaling;
			var levelMap = level.Map;
			_levelHeight = levelMap!.Length;
			_levelWidth = levelMap[0].Length;
			_levelMap = new IStaticGameObject[_levelHeight][];
			Player player = default;
			for (var i = 0; i < _levelHeight; i++)
				_levelMap[i] = new IStaticGameObject[_levelWidth];
			for (var y = 0; y < _levelHeight; y++)
			for (var x = 0; x < _levelWidth; x++)
				switch (levelMap[y][x])
				{
					case 0:
						_levelMap[y][x] = new Floor(new Vector2(x, y), _scaling);
						break;
					case 1:
						_levelMap[y][x] = new Wall(new Vector2(x, y), _scaling);
						break;
					case 2:
						_levelMap[y][x] = new Floor(new Vector2(x, y), _scaling);
						player = new Player(_scaling, level.PlayerSize, new Vector2(x * _scaling, y * _scaling), this);
						break;
					case 3:
						_levelMap[y][x] = new Floor(new Vector2(x, y), _scaling);
						_enemies.Add(new Enemy(_scaling, level.EnemySize,
							new Vector2(x * _scaling, y * _scaling), this, null));
						break;
					default:
						_levelMap[y][x] = null;
						break;
				}
			_player = player;
			foreach (var enemy in _enemies) enemy.Player = player;
		}
		
		#region view
		
		protected override void OnRender(D2DGraphics g)
		{
			UpdateMap();
			
			for (var y = 0; y < _levelHeight; y++)
			for (var x = 0; x < _levelWidth; x++)
				_levelMap[y][x].Draw(g, _player.Position, Width, Height, _player.Size);
			var sb = new StringBuilder();
			foreach (var enemy in _enemies)
			{
				enemy.Draw(g, Width, Height, _player.Position, _player.Size);
#if DEBUG
				sb.Append($"ENEMY: x: {enemy.Position.X}, y: {enemy.Position.Y}, vision: {enemy.PlayerInVision}\n");
#endif
			}
#if DEBUG
			g.DrawText(sb.ToString(), D2DColor.Black, "Consolas", 14, 0, 30);
#endif
			_player.Draw(g, Width, Height);
			
			
			Invalidate();
		}

		#endregion
		
		#region controller

		protected override void OnKeyDown(KeyEventArgs e)
		{
			switch (e.KeyCode)
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

		protected override void OnKeyUp(KeyEventArgs e)
		{
			switch (e.KeyCode)
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

		#endregion

		#region model

		private void UpdateMap()
		{
			_player.Move(_left, _right, _up, _down);
			MoveEnemies();
		}

		private void MoveEnemies()
		{
			foreach (var enemy in _enemies) enemy.MakeMove();
		}
		
		public IStaticGameObject GetCell(int x, int y)
		{
			return _levelMap[y][x];
		}

		public IStaticGameObject GetCell(Vector2 posInScaling)
		{
			return _levelMap[(int) MathF.Floor(posInScaling.Y / _scaling)][(int) MathF.Floor(posInScaling.X / _scaling)];
		}

		#endregion
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using GameProject.GameObjects;
using GameProject.Levels;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace GameProject
{
	public class Game : D2DControl
	{
		private readonly List<Entity> _customEntities = new();
		private readonly List<Enemy> _enemies = new();
		private readonly IBuilding[][] _levelMap;
		private readonly int _levelWidth, _levelHeight;
		private readonly Player _player;
		private readonly float _scaling;
		private bool _left, _right, _up, _down;

		public Game(Level level)
		{
			Level = level;
			_scaling = level.BlockScaling;
			var levelMap = level.Map;
			_levelHeight = levelMap!.Length;
			_levelWidth = levelMap[0].Length;
			_levelMap = new IBuilding[_levelHeight][];
			Player player = default;
			for (var i = 0; i < _levelHeight; i++)
				_levelMap[i] = new IBuilding[_levelWidth];
			for (var y = 0; y < _levelHeight; y++)
			for (var x = 0; x < _levelWidth; x++)
				switch (levelMap[y][x])
				{
					case 'f':
						_levelMap[y][x] = new Floor(new Vector2(x, y), _scaling);
						break;
					case 'w':
						_levelMap[y][x] = new Wall(new Vector2(x, y), _scaling);
						break;
					case 'p':
						_levelMap[y][x] = new Floor(new Vector2(x, y), _scaling);
						player = new Player(new Vector2(x * _scaling, y * _scaling), this, _enemies, _customEntities);
						break;
					case 'F':
						_levelMap[y][x] = new Floor(new Vector2(x, y), _scaling);
						_enemies.Add(new Fighter(new Vector2(x * _scaling, y * _scaling), this, _enemies));
						break;
					case 'S':
						_levelMap[y][x] = new Floor(new Vector2(x, y), _scaling);
						_enemies.Add(new Shooter(new Vector2(x * _scaling, y * _scaling), this, level.ShootingRange,
							level.ShootingCooldown, _enemies, _customEntities));
						break;
					default:
						_levelMap[y][x] = null;
						break;
				}

			_player = player;
			foreach (var enemy in _enemies) enemy.Player = player;
		}

		public Level Level { get; }

		#region view

		protected override void OnRender(D2DGraphics g)
		{
			//updates
			_player.Update();
			UpdateMap();
			foreach (var enemy in _enemies)
				enemy.Update();
			foreach (var entity in _customEntities.ToList())
				entity.Update();

			//map redrawing
			for (var y = 0; y < _levelHeight; y++)
			for (var x = 0; x < _levelWidth; x++)
				_levelMap[y][x].Draw(g, _player.Position, Width, Height, _player.Size);

			//enemies redrawing
			foreach (var enemy in _enemies)
				enemy.Draw(g, Width, Height);

			//custom entities redrawing
			foreach (var entity in _customEntities)
				entity.Draw(g, Width, Height);

			//player redrawing
			_player.Draw(g, Width, Height);

			//health redrawing
			g.FillRectangle(Width - 300, 0, 3 * _player.Health, 30, D2DColor.Red);

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

		protected override void OnMouseUp(MouseEventArgs e)
		{
			_player.Shoot(PointToClient(e.Location));
		}

		#endregion

		#region model

		private void UpdateMap()
		{
			_player.Move(_left, _right, _up, _down);
			MoveEnemies();
			DamagePlayer();
		}

		private void DamagePlayer()
		{
			foreach (var enemy in _enemies) enemy.DamagePlayer();
		}

		private void MoveEnemies()
		{
			foreach (var enemy in _enemies) enemy.MakeMove();
		}

		private IBuilding GetCell(float x, float y)
		{
			return _levelMap[(int) MathF.Floor(y / _scaling)][(int) MathF.Floor(x / _scaling)];
		}

		public IBuilding GetCell(Vector2 posInScaling)
		{
			return GetCell(posInScaling.X, posInScaling.Y);
		}

		public float FloorToCell(float x)
		{
			return (int) MathF.Floor(x / _scaling) * _scaling;
		}

		public float CeilingToCell(float x)
		{
			return (int) MathF.Ceiling(x / _scaling) * _scaling;
		}

		public bool IsWallOn((float, float) pos1, (float, float) pos2)
		{
			return GetCell(pos1.Item1, pos1.Item2) is Wall || GetCell(pos2.Item1,
				pos2.Item2) is Wall;
		}

		#endregion
	}
}
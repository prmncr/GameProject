using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using GameProject.GameObjects;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameControl
{
	public class GameMap
	{
		private Player Player { get; }
		private readonly IStaticGameObject[][] _level;
		private readonly List<Enemy> _enemies = new();
		private readonly int _levelWidth, _levelHeight;
		private readonly float _scaling;

		public GameMap(ILevel level, out Player player)
		{
			_scaling = level.BlockScaling;

			var levelMap = level.Map;
			_levelHeight = levelMap!.Length;
			_levelWidth = levelMap[0].Length;
			_level = new IStaticGameObject[_levelHeight][];
			for (var i = 0; i < _levelHeight; i++)
				_level[i] = new IStaticGameObject[_levelWidth];
			Player player1 = default;
			for (var y = 0; y < _levelHeight; y++)
			for (var x = 0; x < _levelWidth; x++)
				switch (levelMap[y][x])
				{
					case 1:
						_level[y][x] = new Wall(new Vector2(x, y), _scaling);
						break;
					case 0:
						_level[y][x] = new Floor(new Vector2(x, y), _scaling);
						break;
					case 2:
						_level[y][x] = new Floor(new Vector2(x, y), _scaling);
						player1 = new Player(_scaling, level.PlayerSize, new Vector2(x * _scaling, y * _scaling), this);
						break;
					case 3:
						_level[y][x] = new Floor(new Vector2(x, y), _scaling);
						_enemies.Add(new Enemy(_scaling, level.EnemySize,
							new Vector2(x * _scaling, y * _scaling), this, null));
						break;
					default:
						_level[y][x] = null;
						break;
				}

			player = player1;
			Player = player;
			foreach (var enemy in _enemies) enemy.Player = player;
		}

		public IStaticGameObject GetCell(int x, int y)
		{
			return _level[y][x];
		}

		public IStaticGameObject GetCell(Vector2 posInScaling)
		{
			return _level[(int) MathF.Floor(posInScaling.Y / _scaling)][(int) MathF.Floor(posInScaling.X / _scaling)];
		}

		public void Draw(D2DGraphics g, float width, float height)
		{
			for (var y = 0; y < _levelHeight; y++)
			for (var x = 0; x < _levelWidth; x++)
				_level[y][x].Draw(g, Player.Position, width, height, Player.Size);
			var sb = new StringBuilder();
			foreach (var enemy in _enemies)
			{
				enemy.Draw(g, width, height, Player.Position, Player.Size);
#if DEBUG
				sb.Append($"ENEMY: x: {enemy.Position.X}, y: {enemy.Position.Y}, vision: {enemy.PlayerInVision}\n");
#endif
			}
#if DEBUG
			g.DrawText(sb.ToString(), D2DColor.Black, "Consolas", 14, 0, 30);
#endif
			Player.Draw(g, width, height);
		}

		public void MoveEnemies()
		{
			foreach (var enemy in _enemies) enemy.MakeMove();
		}
	}
}
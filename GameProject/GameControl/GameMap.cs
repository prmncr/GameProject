using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using GameProject.GameObjects;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameControl
{
	public class GameMap
	{
		public Player Player { get; set; }
		private readonly IStaticGameObject[][] _level;
		private List<Enemy> _enemies = new();
		private readonly int _levelWidth, _levelHeight;
		private readonly float _scaling;
		
		public GameMap(float scaling, ILevel level, out Player player)
		{
			_scaling = scaling;
			
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
						_level[y][x] = new Wall(new Vector2(x, y), scaling);
						break;
					case 0:
						_level[y][x] = new Floor(new Vector2(x, y), scaling);
						break;
					case 2:
						_level[y][x] = new Floor(new Vector2(x, y), scaling);
						player1 = new Player(scaling, new Vector2(50, 50), new Vector2(x * scaling,y * scaling), this);
						break;
					case 3:
						_level[y][x] = new Floor(new Vector2(x, y), scaling);
						_enemies.Add((new Enemy(scaling, new Vector2(50, 50), new Vector2(x * scaling,y * scaling), this, player1)));
						break;
					default:
						_level[y][x] = null;
						break;
				}

			player = player1;
		}

		public IStaticGameObject GetCell(int x, int y)
		{
			return _level[y][x];
		}

		public void Draw(D2DGraphics g, float width, float height)
		{
			for (var y = 0; y < _levelHeight; y++)
			for (var x = 0; x < _levelWidth; x++)
				_level[y][x].Draw(g, Player.Position, width, height, Player.Size);
			foreach (var enemy in _enemies)
			{
				enemy.Draw(g, width, height, Player.Position, Player.Size);
			}
			Player.Draw(g, width, height);
		}
	}
}
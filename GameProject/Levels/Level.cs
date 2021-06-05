using System;
using System.Numerics;
using GameProject.GameObjects;

namespace GameProject.Levels
{
	public abstract record Level
	{
		protected string[] StringMap { get; init; }
		public float BlockScale { get; protected init; }
		public Vector2 PlayerSize { get; protected init; }
		public float PlayerSpeed { get; protected init; }
		public Vector2 EnemySize { get; protected init; }
		public float EnemySpeed { get; protected init; }
		public float EnemyVisionDistance { get; protected init; }
		public float ShootingRange { get; protected init; }
		public int ShootingCooldown { get; protected init; }
		public Vector2 BulletSize { get; protected init; }
		public int BulletSpeed { get; protected init; }
		public IBuilding[][] BuiltMap { get; private set; }
		public Player Player { get; private set; }
		public ExitDoor Exit { get; private set; }

		public void Initialize()
		{
			BuiltMap = new IBuilding[StringMap.Length][];
			for (var i = 0; i < StringMap.Length; i++)
				BuiltMap[i] = new IBuilding[StringMap[0].Length];
			for (var y = 0; y < BuiltMap.Length; y++)
			for (var x = 0; x < BuiltMap[0].Length; x++)
				switch (StringMap[y][x])
				{
					case 'f':
						CreateFloor(x, y);
						break;
					case 'w':
						CreateWall(x, y);
						break;
					case 'p':
						CreatePlayer(x, y);
						break;
					case 'F':
						CreateFighter(x, y);
						break;
					case 'S':
						CreateShooter(x, y);
						break;
					case 'E':
						CreateExit(x, y);
						break;
					default:
						BuiltMap[y][x] = null;
						break;
				}
		}

		private void CreateWall(int x, int y)
		{
			BuiltMap[y][x] = new Wall(new Vector2(x, y));
		}

		private void CreateFloor(int x, int y)
		{
			BuiltMap[y][x] = new Floor(new Vector2(x, y));
		}

		private void CreatePlayer(int x, int y)
		{
			CreateFloor(x, y);
			Player = new Player(new Vector2(x * BlockScale, y * BlockScale));
		}

		private void CreateFighter(int x, int y)
		{
			CreateFloor(x, y);
			LevelController.Enemies.Add(new Fighter(new Vector2(x * BlockScale, y * BlockScale)));
		}

		private void CreateShooter(int x, int y)
		{
			CreateFloor(x, y);
			LevelController.Enemies.Add(new Shooter(new Vector2(x * BlockScale, y * BlockScale)));
		}

		private void CreateExit(int x, int y)
		{
			var exit = new ExitDoor(new Vector2(x, y));
			BuiltMap[y][x] = exit;
			Exit = exit;
		}

		private IBuilding GetCell(float x, float y)
		{
			try
			{
				return BuiltMap[(int) MathF.Floor(y / BlockScale)][(int) MathF.Floor(x / BlockScale)];
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return default;
			}
		}

		public IBuilding GetCell(Vector2 posInScaling)
		{
			return GetCell(posInScaling.X, posInScaling.Y);
		}

		public float FloorToCell(float x)
		{
			return (int) MathF.Floor(x / BlockScale) * BlockScale;
		}

		public float CeilingToCell(float x)
		{
			return (int) MathF.Ceiling(x / BlockScale) * BlockScale;
		}

		public bool IsWallOn((float, float) pos1, (float, float) pos2)
		{
			return GetCell(pos1.Item1, pos1.Item2) is Wall || GetCell(pos2.Item1, pos2.Item2) is Wall ||
			       GetCell(pos1.Item1, pos1.Item2) is ExitDoor && !LevelController.Level.Exit.IsOpen ||
			       GetCell(pos2.Item1, pos2.Item2) is ExitDoor && !LevelController.Level.Exit.IsOpen;
		}
	}
}
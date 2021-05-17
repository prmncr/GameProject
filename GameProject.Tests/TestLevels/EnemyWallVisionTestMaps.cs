using System.Numerics;
using GameProject.Levels;

namespace GameProject.Tests.TestLevels
{
	public class TestLevel1 : ILevel
	{
		public int[][] Map => new[]
		{
			new[] {1, 1, 1, 1, 1, 1},
			new[] {1, 2, 0, 0, 0, 1},
			new[] {1, 0, 0, 0, 0, 1},
			new[] {1, 0, 0, 0, 3, 1},
			new[] {1, 1, 1, 1, 1, 1}
		};

		public float BlockScaling => 10;
		public Vector2 PlayerSize => new(10, 10);
		public Vector2 EnemySize  => new(10, 10);
	}
	
	public class TestLevel2 : ILevel
	{
		public int[][] Map => new[]
		{
			new[] {1, 1, 1, 1, 1, 1, 1},
			new[] {1, 2, 0, 1, 0, 0, 1},
			new[] {1, 0, 0, 1, 0, 0, 1},
			new[] {1, 0, 0, 1, 0, 3, 1},
			new[] {1, 1, 1, 1, 1, 1, 1}
		};

		public float BlockScaling => 10;
		public Vector2 PlayerSize => new(10, 10);
		public Vector2 EnemySize  => new(10, 10);
	}
}
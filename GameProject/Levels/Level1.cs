using System.Numerics;
using GameProject.GameObjects;

namespace GameProject.Levels
{
	public class Level1 : ILevel
	{
		public int[][] Map => new []
		{
			new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 2, 0, 1, 1, 1, 1, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 3, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1},
			new[] {1, 3, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
		};

		public float BlockScaling => 50;
		public Vector2 PlayerSize => new(40, 40);
		public float PlayerSpeed => 5;
		public Vector2 EnemySize => new(40, 40);
		public float EnemySpeed => 4;
		public float EnemyVisionDistance => 250;
	}
}
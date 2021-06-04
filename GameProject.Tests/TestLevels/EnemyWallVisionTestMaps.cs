using System.Numerics;
using GameProject.Levels;

namespace GameProject.Tests.TestLevels
{
	public record TestLevel1 : Level
	{
		public TestLevel1()
		{
			Map = new[]
			{
				"wwwwww",
				"wpfffw",
				"wffffw",
				"wfffFw",
				"wwwwww"
			};
			BlockScaling = 10;
			PlayerSize = new Vector2(10, 10);
			PlayerSpeed = 5;
			EnemySize = new Vector2(10, 10);
			EnemySpeed = 4;
			EnemyVisionDistance = 1000;
		}
	}

	public record TestLevel2 : Level
	{
		public TestLevel2()
		{
			Map = new[]
			{
				"wwwwwww",
				"wpfwffw",
				"wffwffw",
				"wffwfSw",
				"wwwwwww"
			};

			BlockScaling = 10;
			PlayerSize = new Vector2(10, 10);
			PlayerSpeed = 5;
			EnemySize = new Vector2(10, 10);
			EnemySpeed = 5;
			EnemyVisionDistance = 1000;
		}
	}
}
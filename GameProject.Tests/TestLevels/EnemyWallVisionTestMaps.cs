using System.Numerics;
using GameProject.Levels;

namespace GameProject.Tests.TestLevels
{
	public record TestLevel1 : Level
	{
		public TestLevel1()
		{
			StringMap = new[]
			{
				"wwwwww",
				"wpfffw",
				"wffffw",
				"wfffFw",
				"wwwwww"
			};
			BlockScale = 10;
			PlayerSize = new Vector2(10, 10);
			EnemySize = new Vector2(10, 10);
			EnemyVisionDistance = 1000;
		}
	}

	public record TestLevel2 : Level
	{
		public TestLevel2()
		{
			StringMap = new[]
			{
				"wwwwwww",
				"wpfwffw",
				"wffwffw",
				"wffwfSw",
				"wwwwwww"
			};

			BlockScale = 10;
			PlayerSize = new Vector2(10, 10);
			EnemySize = new Vector2(10, 10);
			EnemyVisionDistance = 1000;
		}
	}
}
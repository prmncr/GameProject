using System.Numerics;

namespace GameProject.Levels
{
	[Level]
	public record Level4 : Level
	{
		public Level4()
		{
			StringMap = new[]
			{
				"wwwwwwwwwwwwwwww",
				"wffffffpfffffffw",
				"wffffffffffffffw",
				"wfffwwwwwwwwfffw",
				"wfffw      wfffw",
				"wfffw      wfffw",
				"wfffw      wfffw",
				"wfffw      wfffw",
				"wFFFw      wFFFw",
				"wSSSwwwwwwwwSSSw",
				"wffffffffffffffw",
				"wffffffffffffffw",
				"wwwwwwwEwwwwwwww"
			};
			BlockScale = 50;
			PlayerSize = new Vector2(40, 40);
			PlayerSpeed = 5;
			EnemySize = new Vector2(40, 40);
			EnemySpeed = 4;
			EnemyVisionDistance = 400;
			ShootingRange = 200;
			ShootingCooldown = 120;
			BulletSize = new Vector2(10, 10);
			BulletSpeed = 20;
		}

#pragma warning disable 108,114
		private static string Name => "Level 4";
#pragma warning restore 108,114
	}
}
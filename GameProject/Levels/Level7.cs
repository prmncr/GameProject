using System.Numerics;

namespace GameProject.Levels
{
	[Level]
	public record Level7 : Level
	{
		public Level7()
		{
			StringMap = new[]
			{
				"wwwEw",
				"wpfFw",
				"wffFw",
				"wffFw",
				"wwwww"
			};
			BlockScale = 300;
			PlayerSize = new Vector2(200, 200);
			PlayerSpeed = 5;
			EnemySize = new Vector2(50, 50);
			EnemySpeed = 10;
			EnemyVisionDistance = 1000;
			ShootingRange = 200;
			ShootingCooldown = 120;
			BulletSize = new Vector2(20, 20);
			BulletSpeed = 40;
		}

#pragma warning disable 108,114
		private static string Name => "Experimental 7";
#pragma warning restore 108,114
	}
}
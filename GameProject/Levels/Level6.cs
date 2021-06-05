using System.Numerics;

namespace GameProject.Levels
{
	[Level]
	public record Level6 : Level
	{
		public Level6()
		{
			StringMap = new[]
			{
				"wwwEw",
				"wpwfw",
				"wffFw",
				"wffFw",
				"wwwww"
			};
			BlockScale = 200;
			PlayerSize = new Vector2(50, 50);
			PlayerSpeed = 20;
			EnemySize = new Vector2(150, 150);
			EnemySpeed = 5;
			EnemyVisionDistance = 1000;
			ShootingRange = 200;
			ShootingCooldown = 120;
			BulletSize = new Vector2(10, 10);
			BulletSpeed = 20;
		}

#pragma warning disable 108,114
		private static string Name => "Experimental 6";
#pragma warning restore 108,114
	}
}
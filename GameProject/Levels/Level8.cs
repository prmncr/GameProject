using System.Numerics;

namespace GameProject.Levels
{
	[Level]
	public record Level8 : Level
	{
		public Level8()
		{
			StringMap = new[]
			{
				"wwwwwwwwwwwwwwwwEw",
				"wpfFFFFFFFFFFFFFFw",
				"wffFFFFFFFFFFFFFFw",
				"wffFFFFFFFFFFFFFFw",
				"wwwwwwwwwwwwwwwwww"
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
		private static string Name => "Experimental 8";
#pragma warning restore 108,114
	}
}
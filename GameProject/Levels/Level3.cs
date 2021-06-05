using System.Numerics;

namespace GameProject.Levels
{
	[Level]
	public record Level3 : Level
	{
		public Level3()
		{
			StringMap = new[]
			{
				"wwwwwwwwwwwwwwwwwwww",
				"wfffffffwffffffffffw",
				"wfffffffwfffffwffffw",
				"wfffwfffwfffffwffffw",
				"wfffwfffwfffffwffffw",
				"wfffwfffwfffffwffffw",
				"wfffwfffwfffffwffffw",
				"wffSwfffwfffSfwffffw",
				"wffFwfffwfffFfwffffw",
				"wfffwffFwfffffwffffw",
				"wfffwffSwfffffwffffw",
				"wfffwfffwfffffwfffFw",
				"wfffwfffwfffffwfffSw",
				"wfffwfffwfffffwffffw",
				"wfffwfffwfffffwffffw",
				"wfffwfffwfffffwffffw",
				"wfpfwfffffffffwffffw",
				"wwwwwwwwwwwwwwwwEwww"
			};
			BlockScale = 50;
			PlayerSize = new Vector2(40, 40);
			PlayerSpeed = 5;
			EnemySize = new Vector2(40, 40);
			EnemySpeed = 4;
			EnemyVisionDistance = 1000;
			ShootingRange = 1000;
			ShootingCooldown = 120;
			BulletSize = new Vector2(10, 10);
			BulletSpeed = 20;
		}

#pragma warning disable 108,114
		private static string Name => "Level 3";
#pragma warning restore 108,114
	}
}
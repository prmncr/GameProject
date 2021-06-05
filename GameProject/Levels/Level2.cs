using System.Numerics;

namespace GameProject.Levels
{
	[Level]
	public record Level2 : Level
	{
		public Level2()
		{
			StringMap = new[]
			{
				"wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww",
				"wSwffffffffffffffffffffwffffffffffffffffffw",
				"wffwfffffffffffffffffffwffffffffffffffffffw",
				"wfffwffffffffffffffffffwffffffffffffffSfffw",
				"wffffwfffffffffffffffffwffffffffffffffffffw",
				"wfffffwffffffffffffffffwffffffffffffffffffw",
				"wffffffwfwwwwwwwwwwwwwwwffffffffffffffffffw",
				"wfffffffffffffffffffffffffffffffffffffffffw",
				"wfffffffffffffffffffffffffffffffffffffffffw",
				"wffffffffffffffffffwwwwwwfffffffffffffffffw",
				"wffffffffffffffffffwfffwwfffffffffffffffffw",
				"wffffffffffffffffffwfffwwffffpffffffffffffw",
				"wffffffffffffffffffwfffwwfffffffffffffffffw",
				"wffffffffffffffffffwfffwwfffffffffffffffffw",
				"wffffffffffffffffffwfffwwfffffffffffffffffw",
				"wffffffffffffffffffwfffwwfffffffffffffffffw",
				"wffffffffffffffffffwfSfwwfffffffffffffffffw",
				"wffffffffffffffffffwfffwwffffffffffffFffffw",
				"wffffffffffffffffffwwwwwwfffffffffffffffffw",
				"wfffffFfffffffffffffffffffffffffffffffffffw",
				"wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww"
			};
			BlockScale = 50;
			PlayerSize = new Vector2(40, 40);
			PlayerSpeed = 5;
			EnemySize = new Vector2(40, 40);
			EnemySpeed = 4;
			EnemyVisionDistance = 300;
			ShootingRange = 200;
			ShootingCooldown = 120;
			BulletSize = new Vector2(10, 10);
			BulletSpeed = 20;
		}

#pragma warning disable 108,114
		private static string Name => "Level 2";
#pragma warning restore 108,114
	}
}
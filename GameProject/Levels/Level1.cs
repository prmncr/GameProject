using System.Numerics;

namespace GameProject.Levels
{
	public record Level1 : Level
	{
		public Level1()
		{
			Map = new[]
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
			BlockScaling = 50;
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
	}
}
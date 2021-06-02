using System.Numerics;

namespace GameProject.Levels
{
	public class Level1 : ILevel
	{
		public string[] Map => new[]
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
		public float BlockScaling => 50;
		public Vector2 PlayerSize => new(40, 40);
		public float PlayerSpeed => 5;
		public Vector2 EnemySize => new(40, 40);
		public float EnemySpeed => 4;
		public float EnemyVisionDistance => 300;
		public float ShootingRange => 200;
		public int ShootingCooldown => 120;
		public Vector2 BulletSize => new(10, 10);
		public int BulletSpeed => 20;
	}
}
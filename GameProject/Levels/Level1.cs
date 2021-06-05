using System.Numerics;

namespace GameProject.Levels
{
	[Level]
	public record Level1 : Level
	{
		public Level1()
		{
			StringMap = new[]
			{
				"wwwwwww",
				"wFwpwFw",
				"wfwfwfw",
				"wfwfwfw",
				"wfffffw",
				"wfwfwfw",
				"wfwfwfw",
				"wFwfwFw",
				"wwwwwww"
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
		private static string Name => "Level 1";
#pragma warning restore 108,114
	}
}
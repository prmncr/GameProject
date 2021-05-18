using System.Numerics;
using GameProject.Levels;

namespace GameProject.Tests.TestLevels
{
	public class TestLevel1 : ILevel
	{
		public string[] Map => new[]
		{
			"wwwwww",
			"wpfffw",
			"wffffw",
			"wfffFw",
			"wwwwww"
		};

		public float BlockScaling => 10;
		public Vector2 PlayerSize => new(10, 10);
		public float PlayerSpeed => 5;
		public Vector2 EnemySize  => new(10, 10);
		public float EnemySpeed => 4;
		public float EnemyVisionDistance => 1000;
		
		//unused
		public float ShootingRange { get; }
		public int ShootingCooldown { get; }
		public Vector2 BulletSize { get; }
		public int BulletSpeed { get; }
	}
	
	public class TestLevel2 : ILevel
	{
		public string[] Map => new[]
		{
			"wwwwwww",
			"wpfwffw",
			"wffwffw",
			"wffwfFw",
			"wwwwwww"
		};

		public float BlockScaling => 10;
		public Vector2 PlayerSize => new(10, 10);
		public float PlayerSpeed => 5;
		public Vector2 EnemySize  => new(10, 10);
		public float EnemySpeed => 5;
		public float EnemyVisionDistance => 1000;
		
		//unused
		public float ShootingRange { get; }
		public int ShootingCooldown { get; }
		public Vector2 BulletSize { get; }
		public int BulletSpeed { get; }
	}
}
using System.Numerics;

namespace GameProject.Levels
{
	public abstract record Level
	{
		public string[] Map { get; protected init; }
		public float BlockScaling { get; protected init; }
		public Vector2 PlayerSize { get; protected init; }
		public float PlayerSpeed { get; protected init; }
		public Vector2 EnemySize { get; protected init; }
		public float EnemySpeed { get; protected init; }
		public float EnemyVisionDistance { get; protected init; }
		public float ShootingRange { get; protected init; }
		public int ShootingCooldown { get; protected init; }
		public Vector2 BulletSize { get; protected init; }
		public int BulletSpeed { get; protected init; }
	}
}
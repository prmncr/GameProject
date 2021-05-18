using System.Numerics;

namespace GameProject.Levels
{
	public interface ILevel
	{
		string[] Map { get; }
		float BlockScaling { get; }
		Vector2 PlayerSize { get; }
		float PlayerSpeed { get; }
		Vector2 EnemySize { get; }
		float EnemySpeed { get; }
		float EnemyVisionDistance { get; }
		float ShootingRange { get; }
		int ShootingCooldown { get; }
		Vector2 BulletSize { get; }
		int BulletSpeed { get; }
	}
}
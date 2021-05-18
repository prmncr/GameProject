using System.Numerics;

namespace GameProject.Levels
{
	public interface ILevel
	{
		public int[][] Map { get; }
		public float BlockScaling { get; }
		public Vector2 PlayerSize { get; }
		public float PlayerSpeed { get; }
		public Vector2 EnemySize { get; }
		public float EnemySpeed { get; }
		public float EnemyVisionDistance { get; }
	}
}
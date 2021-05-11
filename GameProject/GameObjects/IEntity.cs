using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public interface IEntity
	{
		public float Speed { get; set; }
		public Vector2 Position { get; set; }
		public Vector2 Size { get; set; }
	
		void Draw(D2DGraphics g, float width, float height, Vector2 offset = default, Vector2 playerSize = default);
	}
}
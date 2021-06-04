using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public interface IBuilding
	{
		public Vector2 Position { get; }
		public float Scaling { get; }

		void Draw(D2DGraphics g, Vector2 playerPos, float width, float height, Vector2 playerSize);
	}
}
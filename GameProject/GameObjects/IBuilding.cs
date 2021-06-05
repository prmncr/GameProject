using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public interface IBuilding
	{
		public Vector2 Position { get; }

		void Redraw(D2DGraphics g, float width, float height);
	}
}
using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public interface IEntity
	{
		public void Draw(D2DGraphics g, float width, float height);

		public void Update();
	}
}
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public abstract class Entity
	{
		protected bool IsResistance;
		protected int Resist;
		public float Health { get; protected set; } = 100;

		public abstract void Redraw(D2DGraphics g, float width, float height);

		public abstract void UpdateCounters();
	}
}
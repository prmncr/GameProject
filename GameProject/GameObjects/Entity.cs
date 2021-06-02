using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public abstract class Entity
	{
		public bool IsResistance { get; protected set; }
        public int Resist { get; protected set; }
        public float Health { get; protected set; }
		
		public abstract void Draw(D2DGraphics g, float width, float height);

		public abstract void Update();
	}
}
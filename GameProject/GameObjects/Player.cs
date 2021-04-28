using System.Drawing;
using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Player
	{
		public Vector2 Position { get; private set; } = new(100, 100);
		public Vector2 Size { get; private set; } = new(30, 30);

		public void Move(bool left, bool right, bool up, bool down)
		{
			if (down) Position += new Vector2(0, 5);
			if (up) Position += new Vector2(0, -5);
			if (left) Position += new Vector2(-5, 0);
			if (right) Position += new Vector2(5, 0);
		}

		public static explicit operator RectangleF(Player player) =>
			new(player.Position.X, player.Position.Y, player.Size.X, player.Size.Y);

		public static explicit operator Rectangle(Player player) =>
			new((int) player.Position.X, (int) player.Position.Y, (int) player.Size.X, (int) player.Size.Y);
	}
}
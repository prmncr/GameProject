using System;
using System.Drawing;
using System.Numerics;
using GameProject.GameControl;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Player : IEntity
	{
		public float Speed { get; set; } = 5;
		public Vector2 Position { get; set; }
		public Vector2 Size { get; set; }

		private GameMap _map;
		private float Top => Position.Y;
		private float Bottom => Position.Y + Size.Y;
		private float Left => Position.X;
		private float Right => Position.X + Size.X;
		private float _scaling;

		public Player(float scaling, Vector2 size, Vector2 startPos, GameMap map)
		{
			Size = size;
			_scaling = scaling;
			Position = startPos;
			_map = map;
		}

		public void Move(bool left, bool right, bool up, bool down)
		{
			if (down)
			{
				var predictedY = (int) MathF.Floor((Bottom + Speed) / _scaling);
				if (_map.GetCell((int) MathF.Floor(Left / _scaling), predictedY) is Wall ||
				    _map.GetCell((int) MathF.Floor((Right - 1) / _scaling), predictedY) is Wall)
					Position += new Vector2(0, predictedY * _scaling - Bottom);
				else
					Position += new Vector2(0, Speed / (left || right ? MathF.Sqrt(2) : 1));
			}
			if (up)
			{
				var predictedY = (int) MathF.Floor((Top - Speed) / _scaling);
				if (_map.GetCell((int) MathF.Floor(Left / _scaling), predictedY) is Wall ||
				    _map.GetCell((int) MathF.Floor((Right - 1) / _scaling), predictedY) is Wall)
					Position += new Vector2(0, (predictedY + 1) * _scaling - Top);
				else
					Position += new Vector2(0, -Speed / (left || right ? MathF.Sqrt(2) : 1));
			}
			if (left)
			{
				var predictedX = (int) MathF.Floor((Left - Speed) / _scaling);
				if (_map.GetCell(predictedX, (int) MathF.Floor(Top / _scaling)) is Wall ||
				    _map.GetCell(predictedX, (int) MathF.Floor((Bottom - 1) / _scaling)) is Wall)
					Position += new Vector2((predictedX + 1) * _scaling - Left, 0);
				else
					Position += new Vector2(-Speed / (up || down ? MathF.Sqrt(2) : 1), 0);
			}
			if (right)
			{
				var predictedX = (int) MathF.Floor((Right + Speed) / _scaling);
				if (_map.GetCell(predictedX, (int) MathF.Floor(Top / _scaling)) is Wall ||
				    _map.GetCell(predictedX, (int) MathF.Floor((Bottom - 1) / _scaling)) is Wall)
					Position += new Vector2(predictedX * _scaling - Right, 0);
				else
					Position += new Vector2(Speed / (up || down ? MathF.Sqrt(2) : 1), 0);
			}
		}
		
		public void Draw(D2DGraphics g, float width, float height, Vector2 offset = default, Vector2 playerSize = default)
		{
			g.FillRectangle(MathF.Ceiling((width - Size.X) / 2), MathF.Ceiling((height - Size.Y) / 2), Size.X, Size.Y, D2DColor.LightGreen);
			#if DEBUG
			g.DrawText($"MODEL: x: {Position.X}, y: {Position.Y}, scale: {_scaling}", D2DColor.Black, "Consolas", 14, 0, 0);
			g.DrawText(
				$" VIEW: x: {MathF.Ceiling((width - Size.X) / 2)}, y: {MathF.Ceiling((height - Size.Y) / 2)}, scale: {_scaling}",
				D2DColor.Black, "Consolas", 14, 0, 15);
			#endif
		}

		public static explicit operator RectangleF(Player player) =>
			new(player.Position.X, player.Position.Y, player.Size.X, player.Size.Y);
	}
}
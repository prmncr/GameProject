using System;
using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Player
	{
		public Vector2 Position => _position;
		public Vector2 Size => _size;
		
		private float Top => _position.Y;
		private float Bottom => _position.Y + _size.Y;
		private float Left => _position.X;
		private float Right => _position.X + _size.X;
		
		private Vector2 _position;
		private readonly float _scaling;
		private readonly float _speed;
		private readonly Vector2 _size;
		private readonly Game _map;
		
		public Player(float scaling, Vector2 size, Vector2 startPos, Game map)
		{
			_size = size;
			_scaling = scaling;
			_position = startPos;
			_map = map;
			_speed = map.Level.PlayerSpeed;
		}
		
		public Player(float scaling, Vector2 size, Vector2 startPos, float speed)
		{
			_size = size;
			_scaling = scaling;
			_position = startPos;
			_speed = speed;
		}

		public void Move(bool left, bool right, bool up, bool down)
		{
			if (down)
			{
				var predictedY = (int) MathF.Floor((Bottom + _speed) / _scaling);
				if (_map.GetCell((int) MathF.Floor(Left / _scaling), predictedY) is Wall ||
				    _map.GetCell((int) MathF.Floor((Right - 1) / _scaling), predictedY) is Wall)
					_position += new Vector2(0, predictedY * _scaling - Bottom);
				else
					_position += new Vector2(0, _speed / (left || right ? MathF.Sqrt(2) : 1));
			}
			if (up)
			{
				var predictedY = (int) MathF.Floor((Top - _speed) / _scaling);
				if (_map.GetCell((int) MathF.Floor(Left / _scaling), predictedY) is Wall ||
				    _map.GetCell((int) MathF.Floor((Right - 1) / _scaling), predictedY) is Wall)
					_position += new Vector2(0, (predictedY + 1) * _scaling - Top);
				else
					_position += new Vector2(0, -_speed / (left || right ? MathF.Sqrt(2) : 1));
			}
			if (left)
			{
				var predictedX = (int) MathF.Floor((Left - _speed) / _scaling);
				if (_map.GetCell(predictedX, (int) MathF.Floor(Top / _scaling)) is Wall ||
				    _map.GetCell(predictedX, (int) MathF.Floor((Bottom - 1) / _scaling)) is Wall)
					_position += new Vector2((predictedX + 1) * _scaling - Left, 0);
				else
					_position += new Vector2(-_speed / (up || down ? MathF.Sqrt(2) : 1), 0);
			}
			if (right)
			{
				var predictedX = (int) MathF.Floor((Right + _speed) / _scaling);
				if (_map.GetCell(predictedX, (int) MathF.Floor(Top / _scaling)) is Wall ||
				    _map.GetCell(predictedX, (int) MathF.Floor((Bottom - 1) / _scaling)) is Wall)
					_position += new Vector2(predictedX * _scaling - Right, 0);
				else
					_position += new Vector2(_speed / (up || down ? MathF.Sqrt(2) : 1), 0);
			}
		}
		
		public void Draw(D2DGraphics g, float width, float height, Vector2 offset = default, Vector2 playerSize = default)
		{
			g.FillRectangle(MathF.Ceiling((width - _size.X) / 2), MathF.Ceiling((height - _size.Y) / 2), _size.X, _size.Y, D2DColor.LightGreen);
			#if DEBUG
			g.DrawText($"MODEL: x: {_position.X}, y: {_position.Y}, scale: {_scaling}", D2DColor.Black, "Consolas", 14, 0, 0);
			g.DrawText(
				$" VIEW: x: {MathF.Ceiling((width - _size.X) / 2)}, y: {MathF.Ceiling((height - _size.Y) / 2)}, scale: {_scaling}",
				D2DColor.Black, "Consolas", 14, 0, 15);
			#endif
		}
	}
}
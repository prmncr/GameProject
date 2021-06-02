using System;
using System.Drawing;
using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Player : Entity
	{
		private readonly Game _map;
		private readonly float _scaling;
		private readonly Vector2 _size;
		private readonly float _speed;
		private Vector2 _position;

		public Player(Vector2 startPos, Game map)
		{
			_map = map;
			_size = map.Level.PlayerSize;
			_scaling = map.Level.BlockScaling;
			_position = startPos;

			_speed = map.Level.PlayerSpeed;
		}
		
		public Vector2 Position => _position;
		public Vector2 Size => _size;

		private float Top => _position.Y;
		private float Bottom => _position.Y + _size.Y;
		private float Left => _position.X;
		private float Right => _position.X + _size.X;

		public override void Draw(D2DGraphics g, float width, float height)
		{
			g.FillRectangle(
				MathF.Ceiling((width - _size.X) / 2),
				MathF.Ceiling((height - _size.Y) / 2),
				_size.X,
				_size.Y,
				IsResistance ? D2DColor.DarkGreen : D2DColor.LightGreen);
		}

		public override void Update()
		{
			if (Resist > 0)
			{
				Resist--;
				IsResistance = true;
			}
			else
			{
				IsResistance = false;
			}
		}

		public void Move(bool left, bool right, bool up, bool down)
		{
			var dx = 0f;
			var dy = 0f;
			
			var predictedBottom = Bottom + _speed;
			var bottomOnWall = _map.IsWallOnWithFloor((Left + 1, predictedBottom), (Right - 1, predictedBottom));
			
			var predictedTop = Top - _speed;
			var topOnWall = _map.IsWallOnWithFloor((Right - 1, predictedTop), (Left + 1, predictedTop));
			
			var predictedRight = Right + _speed;
			var leftOnWall = _map.IsWallOnWithFloor((predictedRight, Top + 1), (predictedRight, Bottom - 1));
			
			var predictedLeft = Left - _speed;
			var rightOnWall = _map.IsWallOnWithFloor((predictedLeft, Bottom - 1), (predictedLeft, Top + 1));
			
			if (down && !up)
				dy += bottomOnWall
					? _map.FloorToCell(predictedBottom) - Bottom
					: _speed / ((left || right) && !leftOnWall && !rightOnWall ? MathF.Sqrt(2) : 1);

			if (up && !down)
				dy += topOnWall 
					? _map.CeilingToCell(predictedTop) - Top 
					: -_speed / ((left || right) && !leftOnWall && !rightOnWall ? MathF.Sqrt(2) : 1);

			if (right && !left)
				dx += leftOnWall 
					? _map.FloorToCell(predictedRight) - Right 
					: _speed / ((up || down) && !topOnWall && !bottomOnWall ? MathF.Sqrt(2) : 1);

			if (left && !right)
				dx += rightOnWall 
					? _map.CeilingToCell(predictedLeft) - Left 
					: -_speed / ((up || down) && !topOnWall && !bottomOnWall ? MathF.Sqrt(2) : 1);
			
			_position += new Vector2(dx, dy);
		}

		public void TakeDamage(float damage, int resist)
		{
			if (Resist != 0) return;
			Health -= damage;
			Resist = resist;
		}

		public void Shoot(PointF eLocation)
		{
			
		}
	}
}
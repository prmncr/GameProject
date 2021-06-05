using System;
using System.Drawing;
using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Player : Entity
	{
		private float _controlHeight;
		private float _controlWidth;
		private Vector2 _position;

		public Player(Vector2 startPos)
		{
			_position = startPos;
		}

		public Vector2 Size => LevelController.Level.PlayerSize;
		private static float Speed => LevelController.Level.PlayerSpeed;

		public Vector2 Position => _position;
		private float Top => _position.Y;
		private float Bottom => _position.Y + Size.Y;
		private float Left => _position.X;
		private float Right => _position.X + Size.X;

		public override void Redraw(D2DGraphics g, float width, float height)
		{
			_controlWidth = width;
			_controlHeight = height;
			g.FillRectangle(
				MathF.Ceiling((width - Size.X) / 2),
				MathF.Ceiling((height - Size.Y) / 2),
				Size.X,
				Size.Y,
				IsResistance ? D2DColor.DarkGreen : D2DColor.LightGreen);
		}

		public override void UpdateCounters()
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

			var predictedBottom = Bottom + Speed;
			var bottomOnWall = LevelController.Level.IsWallOn((Left + 1, predictedBottom), (Right - 1, predictedBottom));

			var predictedTop = Top - Speed;
			var topOnWall = LevelController.Level.IsWallOn((Right - 1, predictedTop), (Left + 1, predictedTop));

			var predictedRight = Right + Speed;
			var leftOnWall = LevelController.Level.IsWallOn((predictedRight, Top + 1), (predictedRight, Bottom - 1));

			var predictedLeft = Left - Speed;
			var rightOnWall = LevelController.Level.IsWallOn((predictedLeft, Bottom - 1), (predictedLeft, Top + 1));

			if (down && !up)
				dy += bottomOnWall
					? LevelController.Level.FloorToCell(predictedBottom) - Bottom
					: Speed / ((left || right) && !leftOnWall && !rightOnWall ? MathF.Sqrt(2) : 1);

			if (up && !down)
				dy += topOnWall
					? LevelController.Level.CeilingToCell(predictedTop) - Top
					: -Speed / ((left || right) && !leftOnWall && !rightOnWall ? MathF.Sqrt(2) : 1);

			if (right && !left)
				dx += leftOnWall
					? LevelController.Level.FloorToCell(predictedRight) - Right
					: Speed / ((up || down) && !topOnWall && !bottomOnWall ? MathF.Sqrt(2) : 1);

			if (left && !right)
				dx += rightOnWall
					? LevelController.Level.CeilingToCell(predictedLeft) - Left
					: -Speed / ((up || down) && !topOnWall && !bottomOnWall ? MathF.Sqrt(2) : 1);

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
			var dir = Math.ConvertToModelPos(new Vector2(eLocation.X, eLocation.Y), _position, Size,
				new Vector2(_controlWidth, _controlHeight)) - (_position + Size / 2 - LevelController.Level.BulletSize / 2);
			LevelController.SummonedEntities.Add(new Bullet(this, _position + Size / 2 - LevelController.Level.BulletSize / 2,
				dir));
		}
	}
}
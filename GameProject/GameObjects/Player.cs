using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using GameProject.Levels;
using GameProject.Properties;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Player : Entity
	{
		private readonly Dictionary<Direction, D2DBitmap> _bitmaps = new();

		private float _controlHeight;
		private float _controlWidth;
		private Direction _direction;
		private bool _isCached;
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

		public override void Redraw(D2DGraphics g, D2DDevice device, float width, float height)
		{
			if (!_isCached)
			{
				var b1 = Resources.Player.Clone() as Bitmap;
				var b2 = Resources.Player.Clone() as Bitmap;
				var b3 = Resources.Player.Clone() as Bitmap;
				var b4 = Resources.Player.Clone() as Bitmap;
				b2?.RotateFlip(RotateFlipType.Rotate90FlipNone);
				b3?.RotateFlip(RotateFlipType.Rotate180FlipNone);
				b4?.RotateFlip(RotateFlipType.Rotate270FlipNone);

				_bitmaps.Add(Direction.Right, device.CreateBitmapFromGDIBitmap(b2));
				_bitmaps.Add(Direction.Up, device.CreateBitmapFromGDIBitmap(b1));
				_bitmaps.Add(Direction.Left, device.CreateBitmapFromGDIBitmap(b4));
				_bitmaps.Add(Direction.Down, device.CreateBitmapFromGDIBitmap(b3));
				_isCached = true;
			}

			_controlWidth = width;
			_controlHeight = height;
			var a = new D2DRect((width - Size.X) / 2, (height - Size.Y) / 2, Size.X, Size.Y);
			g.DrawBitmap(_bitmaps[_direction], a);
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
			var bottomOnWall =
				LevelController.Level.IsWallOn((Left + 1, predictedBottom), (Right - 1, predictedBottom));

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
			CalculateAngle(dx, dy);
		}

		private void CalculateAngle(float dx, float dy)
		{
			var a = new Vector2(dx, dy);
			a /= a.Length();
			var angle = (int) (MathF.Atan2(a.X, a.Y) * 180 / MathF.PI / 90);
			switch (angle)
			{
				case 0:
					_direction = Direction.Right;
					break;
				case 1:
					_direction = Direction.Up;
					break;
				case -1:
					_direction = Direction.Down;
					break;
				case 2:
				case -2:
					_direction = Direction.Left;
					break;
			}
		}

		public void TakeDamage(float damage, int resist)
		{
			if (Resist != 0) return;
			Health -= damage;
			Resist = resist;

			if (!(Health <= 0)) return;
			MainWindow.GetInstance().ChangePage(Page.GameEnd);
		}

		public void Shoot(PointF eLocation)
		{
			var dir = Math.ConvertToModelPos(new Vector2(eLocation.X, eLocation.Y), _position, Size,
				          new Vector2(_controlWidth, _controlHeight)) -
			          (_position + Size / 2 - LevelController.Level.BulletSize / 2);
			LevelController.SummonedEntities.Add(new Bullet(this,
				_position + Size / 2 - LevelController.Level.BulletSize / 2,
				dir));
		}

		private enum Direction
		{
			Up,
			Right,
			Down,
			Left
		}
	}
}
using System;
using System.Collections.Generic;
using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public abstract class Enemy : Entity
	{
		protected readonly Dictionary<Direction, D2DBitmap> Bitmaps = new();

		protected Direction CurrentDirection = Direction.Down;
		protected bool IsCached;

		protected Vector2? LastPath;
		public Vector2 Position;

		protected Enemy(Vector2 startPos)
		{
			Position = startPos;
		}

		private float _speed => LevelController.Level.EnemySpeed;
		public Vector2 Size => LevelController.Level.EnemySize;
		protected float VisionDistance => LevelController.Level.EnemyVisionDistance;

		private float Top => Position.Y;
		private float Bottom => Position.Y + Size.Y;
		private float Left => Position.X;
		private float Right => Position.X + Size.X;

		public abstract void DamagePlayer();

		public (bool, Vector2) CheckPath()
		{
			if (LevelController.Player == null) return (false, default);
			var path = LevelController.Player.Position + LevelController.Player.Size / 2 - (Position + Size / 2);
			var dx = path.X / path.Length();
			var dy = path.Y / path.Length();
			for (var i = 0; i < path.Length(); i++)
				if (LevelController.Level.GetCell(Position + Size / 2 + new Vector2(dx * i, dy * i)) is Wall)
					return (false, default);
			return (true, path);
		}

		public virtual void MakeMove()
		{
			var (pathExist, path) = CheckPath();
			if (pathExist && (LevelController.Player.Position - Position).Length() <= VisionDistance)
			{
				LastPath = path;
				var moveTo = Move(path.X > 0, path.Y > 0);
				Position += moveTo;
				LastPath -= moveTo;
			}
			else if (LastPath.HasValue)
			{
				var moveTo = Move(LastPath.Value.X > 0, LastPath.Value.Y > 0);
				Position += moveTo;
				LastPath -= moveTo;
			}
		}

		protected Vector2 Move(bool right, bool down)
		{
			var dPos = Vector2.Zero;
			if (down)
			{
				var predictedY = Bottom + _speed;
				dPos += new Vector2(0, LevelController.Level.IsWallOn((Left + 1, predictedY), (Right - 1, predictedY))
					? LevelController.Level.FloorToCell(predictedY) - Bottom
					: _speed / MathF.Sqrt(2));
			}
			else
			{
				var predictedY = Top - _speed;
				dPos += new Vector2(0, LevelController.Level.IsWallOn((Right - 1, predictedY), (Left + 1, predictedY))
					? LevelController.Level.CeilingToCell(predictedY) - Top
					: -_speed / MathF.Sqrt(2));
			}

			if (right)
			{
				var predictedX = Right + _speed;
				dPos += new Vector2(LevelController.Level.IsWallOn((predictedX, Top + 1), (predictedX, Bottom - 1))
					? LevelController.Level.FloorToCell(predictedX) - Right
					: _speed / MathF.Sqrt(2), 0);
			}
			else
			{
				var predictedX = Left - _speed;
				dPos += new Vector2(LevelController.Level.IsWallOn((predictedX, Bottom - 1), (predictedX, Top + 1))
					? LevelController.Level.CeilingToCell(predictedX) - Left
					: -_speed / MathF.Sqrt(2), 0);
			}

			CalculateAngle(dPos.X, dPos.Y);
			return dPos;
		}

		private void CalculateAngle(float dx, float dy)
		{
			var a = new Vector2(dx, dy);
			a /= a.Length();
			var angle = (int) (MathF.Atan2(a.X, a.Y) * 180 / MathF.PI / 90);
			switch (angle)
			{
				case 0:
					CurrentDirection = Direction.Right;
					break;
				case 1:
					CurrentDirection = Direction.Up;
					break;
				case -1:
					CurrentDirection = Direction.Down;
					break;
				case 2:
				case -2:
					CurrentDirection = Direction.Left;
					break;
			}
		}

		public void TakeDamage(int damage, int cd)
		{
			if (Resist != 0) return;
			Health -= damage;
			Resist = cd;
			if (Health <= 0) LevelController.Enemies.Remove(this);
		}

		protected void Redraw(D2DGraphics g, float width, float height, D2DBitmap bitmap)
		{
			var renderPos = Math.ConvertToRenderPos(Position, LevelController.Player.Position,
				LevelController.Player.Size,
				new Vector2(width, height));
			g.DrawBitmap(bitmap, new D2DRect(renderPos.X, renderPos.Y, Size.X, Size.Y));
			g.FillRectangle(renderPos.X - 5, renderPos.Y - 15, (Size.X + 10) * Health / 100, 10, D2DColor.Red);
		}

		protected enum Direction
		{
			Up,
			Right,
			Down,
			Left
		}
	}
}
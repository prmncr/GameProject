using System;
using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public abstract class Enemy : Entity
	{
		protected Vector2? LastPath;
		protected bool PlayerInVision;
		public Vector2 Position;

		protected Enemy(Vector2 startPos)
		{
			Position = startPos;
		}

		private float _speed => LevelInfo.Level.EnemySpeed;
		public Vector2 Size => LevelInfo.Level.EnemySize;
		protected float VisionDistance => LevelInfo.Level.EnemyVisionDistance;

		private float Top => Position.Y;
		private float Bottom => Position.Y + Size.Y;
		private float Left => Position.X;
		private float Right => Position.X + Size.X;

		public abstract void DamagePlayer();

		public (bool, Vector2) CheckPath()
		{
			if (LevelInfo.Player == null) return (false, default);
			var path = LevelInfo.Player.Position + LevelInfo.Player.Size / 2 - (Position + Size / 2);
			var dx = path.X / path.Length();
			var dy = path.Y / path.Length();
			for (var i = 0; i < path.Length(); i++)
				if (LevelInfo.Level.GetCell(Position + Size / 2 + new Vector2(dx * i, dy * i)) is Wall)
					return (false, default);
			return (true, path);
		}

		public virtual void MakeMove()
		{
			var (pathExist, path) = CheckPath();
			PlayerInVision = pathExist;
			if (pathExist && (LevelInfo.Player.Position - Position).Length() <= VisionDistance)
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
				dPos += new Vector2(0, LevelInfo.Level.IsWallOn((Left + 1, predictedY), (Right - 1, predictedY))
					? LevelInfo.Level.FloorToCell(predictedY) - Bottom
					: _speed / MathF.Sqrt(2));
			}
			else
			{
				var predictedY = Top - _speed;
				dPos += new Vector2(0, LevelInfo.Level.IsWallOn((Right - 1, predictedY), (Left + 1, predictedY))
					? LevelInfo.Level.CeilingToCell(predictedY) - Top
					: -_speed / MathF.Sqrt(2));
			}

			if (right)
			{
				var predictedX = Right + _speed;
				dPos += new Vector2(LevelInfo.Level.IsWallOn((predictedX, Top + 1), (predictedX, Bottom - 1))
					? LevelInfo.Level.FloorToCell(predictedX) - Right
					: _speed / MathF.Sqrt(2), 0);
			}
			else
			{
				var predictedX = Left - _speed;
				dPos += new Vector2(LevelInfo.Level.IsWallOn((predictedX, Bottom - 1), (predictedX, Top + 1))
					? LevelInfo.Level.CeilingToCell(predictedX) - Left
					: -_speed / MathF.Sqrt(2), 0);
			}

			return dPos;
		}

		public void TakeDamage(int damage, int cd)
		{
			if (Resist != 0) return;
			Health -= damage;
			Resist = cd;
			if (Health <= 0) LevelInfo.Enemies.Remove(this);
		}

		protected void Draw(D2DGraphics g, float width, float height, D2DColor color)
		{
			var renderPos = Math.ConvertToRenderPos(Position, LevelInfo.Player.Position, LevelInfo.Player.Size,
				new Vector2(width, height));
			g.FillRectangle(renderPos.X, renderPos.Y, Size.X, Size.Y, color);
		}
	}
}
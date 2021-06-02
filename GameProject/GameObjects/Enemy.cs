using System;
using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public abstract class Enemy : Entity
	{
		private readonly float _speed;
		protected readonly Game Game;
		protected readonly ILevel Level;
		protected readonly Vector2 Size;
		protected readonly float VisionDistance;
		protected Vector2? LastPath;

		protected bool PlayerInVision;
		protected Vector2 Position;

		protected Enemy(Vector2 startPos, Game game)
		{
			Game = game;
			Level = game.Level;
			Size = Level.EnemySize;
			Position = startPos;
			VisionDistance = Level.EnemyVisionDistance;
			_speed = Level.EnemySpeed;
		}

		public Player Player { get; set; }

		private float Top => Position.Y;
		private float Bottom => Position.Y + Size.Y;
		private float Left => Position.X;
		private float Right => Position.X + Size.X;

		public abstract void DamagePlayer();

		public (bool, Vector2) CheckPath()
		{
			if (Player == null) return (false, default);
			var path = Player.Position + Player.Size / 2 - (Position + Size / 2);
			var dx = path.X / path.Length();
			var dy = path.Y / path.Length();
			for (var i = 0; i < path.Length(); i++)
				if (Game.GetCell(Position + Size / 2 + new Vector2(dx * i, dy * i)) is Wall)
					return (false, default);
			return (true, path);
		}

		public virtual void MakeMove()
		{
			var (pathExist, path) = CheckPath();
			PlayerInVision = pathExist;
			if (pathExist && (Player.Position - Position).Length() <= VisionDistance)
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
				dPos += new Vector2(0, Game.IsWallOnWithFloor((Left + 1, predictedY), (Right - 1, predictedY))
					? Game.FloorToCell(predictedY) - Bottom
					: _speed / MathF.Sqrt(2));
			}
			else
			{
				var predictedY = Top - _speed;
				dPos += new Vector2(0, Game.IsWallOnWithFloor((Right - 1, predictedY), (Left + 1, predictedY))
					? Game.CeilingToCell(predictedY) - Top
					: -_speed / MathF.Sqrt(2));
			}

			if (right)
			{
				var predictedX = Right + _speed;
				dPos += new Vector2(Game.IsWallOnWithFloor((predictedX, Top + 1), (predictedX, Bottom - 1))
					? Game.FloorToCell(predictedX) - Right
					: _speed / MathF.Sqrt(2), 0);
			}
			else
			{
				var predictedX = Left - _speed;
				dPos += new Vector2(Game.IsWallOnWithFloor((predictedX, Bottom - 1), (predictedX, Top + 1))
					? Game.CeilingToCell(predictedX) - Left
					: -_speed / MathF.Sqrt(2), 0);
			}

			return dPos;
		}
	}
}
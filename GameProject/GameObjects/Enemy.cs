using System;
using System.Drawing;
using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public abstract class Enemy : IEntity
	{
		public Vector2 Position => _position;
		public Player Player { get; set; }

		private float Top => _position.Y;
		private float Bottom => _position.Y + Size.Y;
		private float Left => _position.X;
		private float Right => _position.X + Size.X;

		public bool PlayerInVision;
		protected readonly ILevel Level;
		protected Vector2 _position;
		protected Vector2? LastPath;
		protected readonly float VisionDistance;
		protected readonly Vector2 Size;
		protected readonly Game _map;
		private readonly float _scaling;
		private readonly float _speed;
		
		protected Enemy(Vector2 startPos, Game map)
		{
			_map = map;
			Level = map.Level;
			Size = Level.EnemySize;
			_scaling = Level.BlockScaling;
			_position = startPos;
			VisionDistance = Level.EnemyVisionDistance;
			_speed = Level.EnemySpeed;
		}

		public abstract void Draw(D2DGraphics g, float width, float height);
		
		public abstract void Update();

		public (bool, Vector2) CheckPath()
		{
			if (Player == null) return (false, default);
			var path = Player.Position + Player.Size / 2 - (_position + Size / 2);
			var dx = path.X / path.Length();
			var dy = path.Y / path.Length();
			for (var i = 0; i < path.Length(); i++)
				if (_map.GetCell(_position + Size / 2 + new Vector2(dx * i, dy * i)) is Wall)
					return (false, default);
			return (true, path);
		}
		
		public virtual void MakeMove()
		{
			var (pathExist, path) = CheckPath();
			PlayerInVision = pathExist;
			if (pathExist && (Player.Position - _position).Length() <= VisionDistance)
			{
				LastPath = path;
				var moveTo = Move(path.X > 0, path.Y > 0);
				_position += moveTo;
				LastPath -= moveTo;
			}
			else if (LastPath.HasValue)
			{
				var moveTo = Move(LastPath.Value.X > 0, LastPath.Value.Y > 0);
				_position += moveTo;
				LastPath -= moveTo;
			}
		}

		protected Vector2 Move(bool right, bool down)
		{
			var dPos = Vector2.Zero;
			if (down)
			{
				var predictedY = (int) MathF.Floor((Bottom + _speed) / _scaling);
				if (_map.GetCell((int) MathF.Floor(Left / _scaling), predictedY) is Wall ||
				    _map.GetCell((int) MathF.Floor((Right - 1) / _scaling), predictedY) is Wall)
					dPos += new Vector2(0, predictedY * _scaling - Bottom);
				else
					dPos += new Vector2(0, _speed / MathF.Sqrt(2));
			}
			else
			{
				var predictedY = (int) MathF.Floor((Top - _speed) / _scaling);
				if (_map.GetCell((int) MathF.Floor(Left / _scaling), predictedY) is Wall ||
				    _map.GetCell((int) MathF.Floor((Right - 1) / _scaling), predictedY) is Wall)
					dPos += new Vector2(0, (predictedY + 1) * _scaling - Top);
				else
					dPos += new Vector2(0, -_speed / MathF.Sqrt(2));
			}
			if (right)
			{
				var predictedX = (int) MathF.Floor((Right + _speed) / _scaling);
				if (_map.GetCell(predictedX, (int) MathF.Floor(Top / _scaling)) is Wall ||
				    _map.GetCell(predictedX, (int) MathF.Floor((Bottom - 1) / _scaling)) is Wall)
					dPos += new Vector2(predictedX * _scaling - Right, 0);
				else
					dPos += new Vector2(_speed /MathF.Sqrt(2), 0);
			}
			else
			{
				var predictedX = (int) MathF.Floor((Left - _speed) / _scaling);
				if (_map.GetCell(predictedX, (int) MathF.Floor(Top / _scaling)) is Wall ||
				    _map.GetCell(predictedX, (int) MathF.Floor((Bottom - 1) / _scaling)) is Wall)
					dPos += new Vector2((predictedX + 1) * _scaling - Left, 0);
				else
					dPos += new Vector2(-_speed / MathF.Sqrt(2), 0);
			}
			return dPos;
		}

		public virtual void DamagePlayer() { }
	}
}
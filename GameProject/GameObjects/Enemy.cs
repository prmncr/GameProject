using System;
using System.Drawing;
using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Enemy
	{
		public Vector2 Position => _position;
		public Player Player { get; set; }
		
		private float Top => _position.Y;
		private float Bottom => _position.Y + _size.Y;
		private float Left => _position.X;
		private float Right => _position.X + _size.X;
		
		public bool PlayerInVision;
		private Vector2 _position;
		private Vector2? _lastPath;
		private readonly Vector2 _size;
		private readonly float _scaling;
		private readonly float _visionDistance;
		private readonly float _speed;
		private readonly Game _map;
		
		public Enemy(float scaling, Vector2 size, Vector2 startPos, Game map, Player player)
		{
			_size = size;
			_scaling = scaling;
			_position = startPos;
			_map = map;
			Player = player;
			_visionDistance = map.Level.EnemyVisionDistance;
			_speed = map.Level.EnemySpeed;
		}
		
		public void Draw(D2DGraphics g, float width, float height, Vector2 offset = default, Vector2 playerSize = default)
		{
			var gEnemyPosX = MathF.Ceiling(width / 2) + _position.X - MathF.Ceiling(playerSize.X / 2) - offset.X;
			var gEnemyPosY = MathF.Ceiling(height / 2) + _position.Y - MathF.Ceiling(playerSize.Y / 2) - offset.Y;
			g.FillRectangle(gEnemyPosX, gEnemyPosY, _size.X, _size.Y, PlayerInVision ? D2DColor.Gold : D2DColor.Black);
#if DEBUG
			var gEnemyCenterX = gEnemyPosX + _size.X / 2;
			var gEnemyCenterY = gEnemyPosY + _size.Y / 2;
			g.DrawLine(gEnemyCenterX, gEnemyCenterY, (width - _size.X) / 2 + playerSize.X / 2,
				(height - _size.Y) / 2 + playerSize.Y / 2, PlayerInVision ? D2DColor.Gold : D2DColor.Black);
			if (_lastPath.HasValue)
				g.DrawLine(gEnemyCenterX, gEnemyCenterY, gEnemyCenterX + _lastPath.Value.X, gEnemyCenterY + _lastPath.Value.Y, D2DColor.Blue);
			g.FillEllipse(
				new D2DEllipse(new D2DPoint(gEnemyCenterX, gEnemyCenterY), new D2DSize(_visionDistance, _visionDistance)),
				D2DColor.FromGDIColor(Color.FromArgb(20, 0, 255, 0)));
#endif
		}

		public (bool, Vector2) CheckPath()
		{
			if (Player == null) return (false, default);
			var path = Player.Position + Player.Size / 2 - (_position + _size / 2);
			var dx = path.X / path.Length();
			var dy = path.Y / path.Length();
			for (var i = 0; i < path.Length(); i++)
				if (_map.GetCell(_position + _size / 2 + new Vector2(dx * i, dy * i)) is Wall)
					return (false, default);
			return (true, path);
		}
		
		public void MakeMove()
		{
			var (pathExist, path) = CheckPath();
			PlayerInVision = pathExist;
			if (pathExist && (Player.Position - _position).Length() <= _visionDistance)
			{
				_lastPath = path;
				var moveTo = Move(path.X > 0, path.Y > 0);
				_position += moveTo;
				_lastPath -= moveTo;
			}
			else if (_lastPath.HasValue)
			{
				var moveTo = Move(_lastPath.Value.X > 0, _lastPath.Value.Y > 0);
				_position += moveTo;
				_lastPath -= moveTo;
			}
		}

		private Vector2 Move(bool right, bool down)
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
	}
}
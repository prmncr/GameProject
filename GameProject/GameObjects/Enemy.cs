using System;
using System.Drawing;
using System.Numerics;
using GameProject.GameControl;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Enemy : IEntity
	{
		public float Speed { get; set; } = 4;
		public Vector2 Position { get; set; }
		public Vector2 Size { get; set; }
		public Player Player { get; set; }
		
		private Vector2? _lastPath;
		private float _visionDistance = 250;
		public bool PlayerInVision;
		private readonly GameMap _map;
		private readonly float _scaling;
		
		private float Top => Position.Y;
		private float Bottom => Position.Y + Size.Y;
		private float Left => Position.X;
		private float Right => Position.X + Size.X;
		
		public Enemy(float scaling, Vector2 size, Vector2 startPos, GameMap map, Player player)
		{
			Size = size;
			_scaling = scaling;
			Position = startPos;
			_map = map;
			Player = player;
		}
		
		public void Draw(D2DGraphics g, float width, float height, Vector2 offset = default, Vector2 playerSize = default)
		{
			var gEnemyPosX = MathF.Ceiling(width / 2) + Position.X - MathF.Ceiling(playerSize.X / 2) - offset.X;
			var gEnemyPosY = MathF.Ceiling(height / 2) + Position.Y - MathF.Ceiling(playerSize.Y / 2) - offset.Y;
			g.FillRectangle(gEnemyPosX, gEnemyPosY, Size.X, Size.Y, PlayerInVision ? D2DColor.Gold : D2DColor.Black);
#if DEBUG
			var gEnemyCenterX = gEnemyPosX + Size.X / 2;
			var gEnemyCenterY = gEnemyPosY + Size.Y / 2;
			g.DrawLine(gEnemyCenterX, gEnemyCenterY, (width - Size.X) / 2 + playerSize.X / 2,
				(height - Size.Y) / 2 + playerSize.Y / 2, PlayerInVision ? D2DColor.Gold : D2DColor.Black);
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
			var path = Player.Position + Player.Size / 2 - (Position + Size / 2);
			var dx = path.X / path.Length();
			var dy = path.Y / path.Length();
			for (var i = 0; i < path.Length(); i++)
				if (_map.GetCell(Position + Size / 2 + new Vector2(dx * i, dy * i)) is Wall)
					return (false, default);
			return (true, path);
		}
		
		public void MakeMove()
		{
			var (pathExist, path) = CheckPath();
			PlayerInVision = pathExist;
			if (pathExist && (Player.Position - Position).Length() <= _visionDistance)
			{
				_lastPath = path;
				var moveTo = Move(path.X > 0, path.Y > 0);
				Position += moveTo;
				_lastPath -= moveTo;
			}
			else if (_lastPath.HasValue)
			{
				var moveTo = Move(_lastPath.Value.X > 0, _lastPath.Value.Y > 0);
				Position += moveTo;
				_lastPath -= moveTo;
			}
		}

		private Vector2 Move(bool right, bool down)
		{
			var dPos = Vector2.Zero;
			if (down)
			{
				var predictedY = (int) MathF.Floor((Bottom + Speed) / _scaling);
				if (_map.GetCell((int) MathF.Floor(Left / _scaling), predictedY) is Wall ||
				    _map.GetCell((int) MathF.Floor((Right - 1) / _scaling), predictedY) is Wall)
					dPos += new Vector2(0, predictedY * _scaling - Bottom);
				else
					dPos += new Vector2(0, Speed / MathF.Sqrt(2));
			}
			else
			{
				var predictedY = (int) MathF.Floor((Top - Speed) / _scaling);
				if (_map.GetCell((int) MathF.Floor(Left / _scaling), predictedY) is Wall ||
				    _map.GetCell((int) MathF.Floor((Right - 1) / _scaling), predictedY) is Wall)
					dPos += new Vector2(0, (predictedY + 1) * _scaling - Top);
				else
					dPos += new Vector2(0, -Speed / MathF.Sqrt(2));
			}
			if (right)
			{
				var predictedX = (int) MathF.Floor((Right + Speed) / _scaling);
				if (_map.GetCell(predictedX, (int) MathF.Floor(Top / _scaling)) is Wall ||
				    _map.GetCell(predictedX, (int) MathF.Floor((Bottom - 1) / _scaling)) is Wall)
					dPos += new Vector2(predictedX * _scaling - Right, 0);
				else
					dPos += new Vector2(Speed /MathF.Sqrt(2), 0);
			}
			else
			{
				var predictedX = (int) MathF.Floor((Left - Speed) / _scaling);
				if (_map.GetCell(predictedX, (int) MathF.Floor(Top / _scaling)) is Wall ||
				    _map.GetCell(predictedX, (int) MathF.Floor((Bottom - 1) / _scaling)) is Wall)
					dPos += new Vector2((predictedX + 1) * _scaling - Left, 0);
				else
					dPos += new Vector2(-Speed / MathF.Sqrt(2), 0);
			}
			return dPos;
		}
	}
}
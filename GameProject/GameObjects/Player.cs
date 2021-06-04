using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Player : Entity
	{
		private readonly List<Entity> _customEntities;
		private readonly List<Enemy> _enemies;
		private readonly Game _map;
		private readonly float _scaling;
		private readonly int _shootingCooldownMax;
		private readonly Vector2 _size;
		private readonly float _speed;
		private float _controlHeight;
		private float _controlWidth;
		private Vector2 _position;
		private int _shootingCooldown;


		public Player(Vector2 startPos, Game map, List<Enemy> enemies, List<Entity> bullets)
		{
			_map = map;
			_position = startPos;
			_customEntities = bullets;
			_enemies = enemies;

			_size = map.Level.PlayerSize;
			_scaling = map.Level.BlockScaling;
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
			_controlWidth = width;
			_controlHeight = height;
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
			var bottomOnWall = _map.IsWallOn((Left + 1, predictedBottom), (Right - 1, predictedBottom));

			var predictedTop = Top - _speed;
			var topOnWall = _map.IsWallOn((Right - 1, predictedTop), (Left + 1, predictedTop));

			var predictedRight = Right + _speed;
			var leftOnWall = _map.IsWallOn((predictedRight, Top + 1), (predictedRight, Bottom - 1));

			var predictedLeft = Left - _speed;
			var rightOnWall = _map.IsWallOn((predictedLeft, Bottom - 1), (predictedLeft, Top + 1));

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
			if (_shootingCooldown != 0) return;
			var dir = Math.ConvertToModelPos(new Vector2(eLocation.X, eLocation.Y), _position, _size,
				new Vector2(_controlWidth, _controlHeight)) - (_position + _size / 2 - _map.Level.BulletSize / 2);
			_customEntities.Add(new Bullet(this,
				_position + _size / 2 - _map.Level.BulletSize / 2,
				dir,
				_map,
				_enemies,
				_customEntities,
				this));
			_shootingCooldown = _shootingCooldownMax;
		}
	}
}
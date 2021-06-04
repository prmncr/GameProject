using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Bullet : Entity
	{
		private readonly List<Entity> _customEntities;
		private readonly Vector2 _dPos;
		private readonly List<Enemy> _enemies;
		private readonly Level _level;
		private readonly Game _map;
		private readonly Vector2 _path;

		private readonly Player _player;
		private readonly Vector2 _size;
		private readonly int _speed;
		private readonly bool _fromPlayer;
		private Vector2 _position;

		public Bullet(Entity sender, Vector2 position, Vector2 path, Game map, List<Enemy> enemies,
			List<Entity> customEntities, Player player)
		{
			_map = map;
			_level = map.Level;
			_player = player;
			_position = position;
			_path = path;
			_size = _level.BulletSize;
			_speed = _level.BulletSpeed;
			_dPos = _path / _path.Length() * _speed;
			_customEntities = customEntities;
			_enemies = enemies;
			if (sender is Player) _fromPlayer = true;
		}

		public override void Draw(D2DGraphics g, float width, float height)
		{
			var renderPos =
				Math.ConvertToRenderPos(_position, _player.Position, _player.Size, new Vector2(width, height));
			g.FillRectangle(renderPos.X, renderPos.Y, _size.X, _size.Y, D2DColor.Gold);
		}

		public override void Update()
		{
			_position += _dPos;
			if (_map.GetCell(_position + _dPos - _size / 2) is Wall) _customEntities.Remove(this);
			if (!_fromPlayer) DamagePlayer();
			if (_fromPlayer) DamageEnemies();
		}

		private void DamagePlayer()
		{
			if (!AreIntersected(
				new RectangleF(_player.Position.X, _player.Position.Y, _player.Size.X, _player.Size.Y),
				new RectangleF(_position.X, _position.Y, _size.X, _size.Y))) return;
			_player.TakeDamage(20, 60);
			_customEntities.Remove(this);
		}

		private void DamageEnemies()
		{
			if (!_fromPlayer) return;
			var hits = _enemies.Where(enemy => AreIntersected(
				new RectangleF(enemy.Position.X, enemy.Position.Y, enemy.Size.X, enemy.Size.Y),
				new RectangleF(_position.X, _position.Y, _size.X, _size.Y))).ToList();
			if (!hits.Any()) return;
			{
				foreach (var enemy in hits)
					enemy.TakeDamage(30, 15);
				_customEntities.Remove(this);
			}
		}

		private static bool AreIntersected(RectangleF r0, RectangleF r1)
		{
			return MathF.Min(r0.Right, r1.Right) - MathF.Max(r0.Left, r1.Left) >= 0 &&
			       MathF.Min(r0.Bottom, r1.Bottom) - MathF.Max(r0.Top, r1.Top) >= 0;
		}
	}
}
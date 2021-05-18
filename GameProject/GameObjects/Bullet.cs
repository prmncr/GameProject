using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Bullet : IEntity
	{
		public Vector2 Position => _position;
		
		private Player _player;
		private Vector2 _position;
		private Vector2 _path;
		private int _speed;
		private Vector2 _dPos;
		private readonly ILevel _level;
		private readonly Vector2 _size;
		private readonly Game _map;
		private List<IEntity> _customs;
		
		public Bullet(Player player, Vector2 position, Vector2 path, Game map, List<IEntity> customs)
		{
			_map = map;
			_level = map.Level;
			_player = player;
			_position = position;
			_path = path;
			_size = _level.BulletSize;
			_speed = _level.BulletSpeed;
			_dPos = _path / _path.Length() * _speed;
			_customs = customs;
		}
		
		public void Draw(D2DGraphics g, float width, float height)
		{
			var gEnemyPosX = MathF.Ceiling(width / 2) + _position.X - MathF.Ceiling(_level.PlayerSize.X / 2) - _player.Position.X;
			var gEnemyPosY = MathF.Ceiling(height / 2) + _position.Y - MathF.Ceiling(_level.PlayerSize.Y / 2) - _player.Position.Y;
			g.FillRectangle(gEnemyPosX, gEnemyPosY, _size.X, _size.Y, D2DColor.Gold);
		}

		public void Update()
		{
			_position += _dPos;
			if (_map.GetCell(_position + _dPos - _size / 2) is Wall) _customs.Remove(this);
			DamagePlayer();
		}

		private void DamagePlayer()
		{
			if (AreIntersected(new RectangleF(_player.Position.X, _player.Position.Y, _player.Size.X, _player.Size.Y),
				new RectangleF(Position.X, Position.Y, _size.X, _size.Y))) _player.TakeDamage(20, 60);
		}

		private static bool AreIntersected(RectangleF r0, RectangleF r1)
		{
			return MathF.Min(r0.Right, r1.Right) - MathF.Max(r0.Left, r1.Left) >= 0 &&
			       MathF.Min(r0.Bottom, r1.Bottom) - MathF.Max(r0.Top, r1.Top) >= 0;
		}
	}
}
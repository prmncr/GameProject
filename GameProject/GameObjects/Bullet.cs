using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Bullet : Entity
	{
		private readonly Vector2 _dPos;
		private readonly bool _fromPlayer;
		private Vector2 _position;

		public Bullet(Entity sender, Vector2 position, Vector2 path)
		{
			_position = position;
			_dPos = path / path.Length() * Speed;
			if (sender is Player) _fromPlayer = true;
		}

		private static Vector2 Size => LevelInfo.Level.BulletSize;
		private static int Speed => LevelInfo.Level.BulletSpeed;

		public override void Draw(D2DGraphics g, float width, float height)
		{
			var renderPos = Math.ConvertToRenderPos(_position, LevelInfo.Player.Position, LevelInfo.Player.Size,
				new Vector2(width, height));
			g.FillRectangle(renderPos.X, renderPos.Y, Size.X, Size.Y, D2DColor.Gold);
		}

		public override void Update()
		{
			_position += _dPos;
			if (LevelInfo.Level.GetCell(_position + _dPos - Size / 2) is Wall) LevelInfo.SummonedEntities.Remove(this);
			switch (_fromPlayer)
			{
				case false:
					DamagePlayer();
					break;
				case true:
					DamageEnemies();
					break;
			}
		}

		private void DamagePlayer()
		{
			if (!AreIntersected(
				new RectangleF(LevelInfo.Player.Position.X, LevelInfo.Player.Position.Y, LevelInfo.Player.Size.X,
					LevelInfo.Player.Size.Y),
				new RectangleF(_position.X, _position.Y, Size.X, Size.Y))) return;
			LevelInfo.Player.TakeDamage(20, 60);
			LevelInfo.SummonedEntities.Remove(this);
		}

		private void DamageEnemies()
		{
			if (!_fromPlayer) return;
			var hits = LevelInfo.Enemies.Where(enemy => AreIntersected(
				new RectangleF(enemy.Position.X, enemy.Position.Y, enemy.Size.X, enemy.Size.Y),
				new RectangleF(_position.X, _position.Y, Size.X, Size.Y))).ToList();
			if (!hits.Any()) return;
			{
				foreach (var enemy in hits)
					enemy.TakeDamage(30, 15);
				LevelInfo.SummonedEntities.Remove(this);
			}
		}

		private static bool AreIntersected(RectangleF r0, RectangleF r1)
		{
			return MathF.Min(r0.Right, r1.Right) - MathF.Max(r0.Left, r1.Left) >= 0 &&
			       MathF.Min(r0.Bottom, r1.Bottom) - MathF.Max(r0.Top, r1.Top) >= 0;
		}
	}
}
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

		private static Vector2 Size => LevelController.Level.BulletSize;
		private static int Speed => LevelController.Level.BulletSpeed;

		public override void Redraw(D2DGraphics g, D2DDevice device, float width, float height)
		{
			var renderPos = Math.ConvertToRenderPos(_position, LevelController.Player.Position,
				LevelController.Player.Size,
				new Vector2(width, height));
			g.FillRectangle(renderPos.X, renderPos.Y, Size.X, Size.Y, D2DColor.Gold);
		}

		public override void UpdateCounters()
		{
			_position += _dPos;
			if (LevelController.Level.GetCell(_position + _dPos - Size / 2) is Wall)
				LevelController.SummonedEntities.Remove(this);
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
			if (!Math.AreIntersected(
				new RectangleF(LevelController.Player.Position.X, LevelController.Player.Position.Y,
					LevelController.Player.Size.X,
					LevelController.Player.Size.Y),
				new RectangleF(_position.X, _position.Y, Size.X, Size.Y))) return;
			LevelController.Player.TakeDamage(20, 60);
			LevelController.SummonedEntities.Remove(this);
		}

		private void DamageEnemies()
		{
			if (!_fromPlayer) return;
			var enemy = LevelController.Enemies.FirstOrDefault(enemy1 =>
				new RectangleF(enemy1.Position.X, enemy1.Position.Y, enemy1.Size.X, enemy1.Size.Y)
					.IntersectsWith(new RectangleF(_position.X, _position.Y, Size.X, Size.Y)));
			if (enemy == null) return;
			enemy.TakeDamage(30, 15);
			LevelController.SummonedEntities.Remove(this);
		}
	}
}
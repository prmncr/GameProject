using System.Collections.Generic;
using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Shooter : Enemy
	{
		private readonly List<Entity> _customEntities;
		private readonly int _shootingCooldownMax;

		private readonly float _shootRange;
		private int _shootingCooldown;

		public Shooter(Vector2 startPos, Game level, float shootRange, int shootingCooldown, List<Enemy> enemies,
			List<Entity> customEntities) : base(startPos, level, enemies)
		{
			_shootRange = shootRange;
			_shootingCooldown = shootingCooldown;
			_shootingCooldownMax = shootingCooldown;
			_customEntities = customEntities;
		}

		public override void Draw(D2DGraphics g, float width, float height)
		{
			base.Draw(g, width, height, D2DColor.Blue);
		}

		public override void Update()
		{
			if (_shootingCooldown > 0)
				_shootingCooldown--;
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

		public override void DamagePlayer()
		{
		}

		public override void MakeMove()
		{
			var (pathExist, path) = CheckPath();
			PlayerInVision = pathExist;
			var distance = (Player.Position - Position).Length();
			if (pathExist && distance <= VisionDistance)
			{
				if (distance <= _shootRange) Shoot(path);
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

		private void Shoot(Vector2 path)
		{
			if (_shootingCooldown != 0) return;
			_customEntities.Add(new Bullet(this, Position + Size / 2, path, Game, null, _customEntities, Player));
			_shootingCooldown = _shootingCooldownMax;
		}
	}
}
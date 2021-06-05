using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Shooter : Enemy
	{
		private int _shootingCooldown;

		public Shooter(Vector2 startPos) : base(startPos)
		{
			_shootingCooldown = LevelInfo.Level.ShootingCooldown;
		}

		private int _shootingCooldownMax => LevelInfo.Level.ShootingCooldown;

		private float _shootRange => LevelInfo.Level.ShootingRange;

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
			var distance = (LevelInfo.Player.Position - Position).Length();
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
			LevelInfo.SummonedEntities.Add(new Bullet(this, Position + Size / 2, path));
			_shootingCooldown = _shootingCooldownMax;
		}
	}
}
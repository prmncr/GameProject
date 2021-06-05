using System.Drawing;
using System.Numerics;
using GameProject.Levels;
using GameProject.Properties;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Shooter : Enemy
	{
		private readonly Bitmap _bitmap = Resources.Shooter;
		private D2DBitmap _cachedSprite;
		private bool _isCached;
		private int _shootingCooldown;

		public Shooter(Vector2 startPos) : base(startPos)
		{
			_shootingCooldown = LevelController.Level.ShootingCooldown;
		}

		private int _shootingCooldownMax => LevelController.Level.ShootingCooldown;

		private float _shootRange => LevelController.Level.ShootingRange;

		public override void Redraw(D2DGraphics g, D2DDevice device, float width, float height)
		{
			if (!_isCached)
			{
				_bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
				_cachedSprite = device.CreateBitmapFromGDIBitmap(_bitmap);
				_isCached = true;
			}

			base.Redraw(g, width, height, _cachedSprite);
		}

		public override void UpdateCounters()
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
			var distance = (LevelController.Player.Position - Position).Length();
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
			LevelController.SummonedEntities.Add(new Bullet(this, Position + Size / 2, path));
			_shootingCooldown = _shootingCooldownMax;
		}
	}
}
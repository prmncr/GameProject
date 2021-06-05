using System;
using System.Drawing;
using System.Numerics;
using GameProject.Levels;
using GameProject.Properties;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Fighter : Enemy
	{
		private readonly Bitmap _bitmap = Resources.Fighter;
		private D2DBitmap _cachedSprite;
		private bool _isCached;

		public Fighter(Vector2 startPos) : base(startPos)
		{
		}

		public override void Redraw(D2DGraphics g, D2DDevice device, float width, float height)
		{
			if (!_isCached)
			{
				_bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
				_cachedSprite = device.CreateBitmapFromGDIBitmap(_bitmap);
				_isCached = true;
			}

			Redraw(g, width, height, _cachedSprite);
		}

		public override void UpdateCounters()
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

		public override void MakeMove()
		{
			var (pathExist, path) = CheckPath();
			PlayerInVision = pathExist;
			if (pathExist && (LevelController.Player.Position - Position).Length() <= VisionDistance)
			{
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

		public override void DamagePlayer()
		{
			if (AreIntersected(
				new RectangleF(LevelController.Player.Position.X, LevelController.Player.Position.Y,
					LevelController.Player.Size.X,
					LevelController.Player.Size.Y),
				new RectangleF(Position.X, Position.Y, Size.X, Size.Y))) LevelController.Player.TakeDamage(10, 60);
		}

		private static bool AreIntersected(RectangleF r0, RectangleF r1)
		{
			return MathF.Min(r0.Right, r1.Right) - MathF.Max(r0.Left, r1.Left) >= 0 &&
			       MathF.Min(r0.Bottom, r1.Bottom) - MathF.Max(r0.Top, r1.Top) >= 0;
		}
	}
}
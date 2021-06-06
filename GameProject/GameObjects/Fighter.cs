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
		public Fighter(Vector2 startPos) : base(startPos)
		{
		}

		public override void Redraw(D2DGraphics g, D2DDevice device, float width, float height)
		{
			if (!IsCached)
			{
				var b1 = Resources.Fighter.Clone() as Bitmap;
				var b2 = Resources.Fighter.Clone() as Bitmap;
				var b3 = Resources.Fighter.Clone() as Bitmap;
				var b4 = Resources.Fighter.Clone() as Bitmap;
				b2?.RotateFlip(RotateFlipType.Rotate90FlipNone);
				b3?.RotateFlip(RotateFlipType.Rotate180FlipNone);
				b4?.RotateFlip(RotateFlipType.Rotate270FlipNone);

				Bitmaps.Add(Direction.Right, device.CreateBitmapFromGDIBitmap(b2));
				Bitmaps.Add(Direction.Up, device.CreateBitmapFromGDIBitmap(b1));
				Bitmaps.Add(Direction.Left, device.CreateBitmapFromGDIBitmap(b4));
				Bitmaps.Add(Direction.Down, device.CreateBitmapFromGDIBitmap(b3));
				IsCached = true;
			}

			Redraw(g, width, height, Bitmaps[CurrentDirection]);
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
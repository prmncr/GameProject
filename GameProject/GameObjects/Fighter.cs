using System;
using System.Drawing;
using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Fighter : Enemy
	{
		public Fighter(Vector2 startPos) : base(startPos)
		{
		}

		public override void Draw(D2DGraphics g, float width, float height)
		{
			base.Draw(g, width, height, D2DColor.Green);
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

		public override void MakeMove()
		{
			var (pathExist, path) = CheckPath();
			PlayerInVision = pathExist;
			if (pathExist && (LevelInfo.Player.Position - Position).Length() <= VisionDistance)
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
				new RectangleF(LevelInfo.Player.Position.X, LevelInfo.Player.Position.Y, LevelInfo.Player.Size.X,
					LevelInfo.Player.Size.Y),
				new RectangleF(Position.X, Position.Y, Size.X, Size.Y))) LevelInfo.Player.TakeDamage(10, 60);
		}

		private static bool AreIntersected(RectangleF r0, RectangleF r1)
		{
			return MathF.Min(r0.Right, r1.Right) - MathF.Max(r0.Left, r1.Left) >= 0 &&
			       MathF.Min(r0.Bottom, r1.Bottom) - MathF.Max(r0.Top, r1.Top) >= 0;
		}
	}
}
using System;
using System.Drawing;
using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Fighter : Enemy
	{
		public Fighter(Vector2 startPos, Game level) : base(startPos, level) { }
		
		public override void Draw(D2DGraphics g, float width, float height)
		{
			var gEnemyPosX = MathF.Ceiling(width / 2) + _position.X - MathF.Ceiling(Level.PlayerSize.X / 2) - Player.Position.X;
			var gEnemyPosY = MathF.Ceiling(height / 2) + _position.Y - MathF.Ceiling(Level.PlayerSize.Y / 2) - Player.Position.Y;
			g.FillRectangle(gEnemyPosX, gEnemyPosY, Size.X, Size.Y, D2DColor.Green);
#if DEBUG
			var gEnemyCenterX = gEnemyPosX + Size.X / 2;
			var gEnemyCenterY = gEnemyPosY + Size.Y / 2;
			g.DrawLine(gEnemyCenterX, gEnemyCenterY, (width - Size.X) / 2 + Level.PlayerSize.X / 2,
				(height - Size.Y) / 2 + Level.PlayerSize.Y / 2, PlayerInVision ? D2DColor.LightGreen : D2DColor.Green);
			if (LastPath.HasValue)
				g.DrawLine(gEnemyCenterX, gEnemyCenterY, gEnemyCenterX + LastPath.Value.X, gEnemyCenterY + LastPath.Value.Y, D2DColor.Orange);
			g.FillEllipse(
				new D2DEllipse(new D2DPoint(gEnemyCenterX, gEnemyCenterY), new D2DSize(VisionDistance, VisionDistance)),
				D2DColor.FromGDIColor(Color.FromArgb(20, 0, 255, 0)));
#endif
		}
		
		public override void Update() { }
		
		public override void MakeMove()
        {
        	var (pathExist, path) = CheckPath();
        	PlayerInVision = pathExist;
        	if (pathExist && (Player.Position - _position).Length() <= VisionDistance)
        	{
        		LastPath = path;
        		var moveTo = Move(path.X > 0, path.Y > 0);
        		_position += moveTo;
        		LastPath -= moveTo;
        	}
        	else if (LastPath.HasValue)
        	{
        		var moveTo = Move(LastPath.Value.X > 0, LastPath.Value.Y > 0);
        		_position += moveTo;
        		LastPath -= moveTo;
        	}
        }

		public override void DamagePlayer()
		{
			if (AreIntersected(new RectangleF(Player.Position.X, Player.Position.Y, Player.Size.X, Player.Size.Y),
				new RectangleF(Position.X, Position.Y, Size.X, Size.Y))) Player.TakeDamage(10, 60);
		}

		private static bool AreIntersected(RectangleF r0, RectangleF r1)
		{
			return MathF.Min(r0.Right, r1.Right) - MathF.Max(r0.Left, r1.Left) >= 0 &&
			       MathF.Min(r0.Bottom, r1.Bottom) - MathF.Max(r0.Top, r1.Top) >= 0;
		}
	}
}
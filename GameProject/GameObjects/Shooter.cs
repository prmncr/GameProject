using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Shooter : Enemy, IEntitySpawner
	{
		private readonly int _shootingCooldownMax;

		private readonly float _shootRange;
		private int _shootingCooldown;

		public Shooter(Vector2 startPos, Game level, float shootRange, int shootingCooldown,
			List<Entity> customEntities) : base(startPos, level)
		{
			_shootRange = shootRange;
			_shootingCooldown = shootingCooldown;
			_shootingCooldownMax = shootingCooldown;
			CustomEntities = customEntities;
		}

		public List<Entity> CustomEntities { get; }

		public override void Draw(D2DGraphics g, float width, float height)
		{
			var gEnemyPosX = MathF.Ceiling(width / 2) + Position.X - MathF.Ceiling(Level.PlayerSize.X / 2) -
			                 Player.Position.X;
			var gEnemyPosY = MathF.Ceiling(height / 2) + Position.Y - MathF.Ceiling(Level.PlayerSize.Y / 2) -
			                 Player.Position.Y;
			g.FillRectangle(gEnemyPosX, gEnemyPosY, Size.X, Size.Y, D2DColor.Blue);
#if DEBUG
			var gEnemyCenterX = gEnemyPosX + Size.X / 2;
			var gEnemyCenterY = gEnemyPosY + Size.Y / 2;
			g.DrawLine(gEnemyCenterX, gEnemyCenterY, (width - Size.X) / 2 + Level.PlayerSize.X / 2,
				(height - Size.Y) / 2 + Level.PlayerSize.Y / 2, PlayerInVision ? D2DColor.LightBlue : D2DColor.Blue);
			if (LastPath.HasValue)
				g.DrawLine(gEnemyCenterX, gEnemyCenterY, gEnemyCenterX + LastPath.Value.X,
					gEnemyCenterY + LastPath.Value.Y, D2DColor.Orange);
			g.FillEllipse(
				new D2DEllipse(new D2DPoint(gEnemyCenterX, gEnemyCenterY), new D2DSize(VisionDistance, VisionDistance)),
				D2DColor.FromGDIColor(Color.FromArgb(20, 0, 0, 255)));
			g.FillEllipse(
				new D2DEllipse(new D2DPoint(gEnemyCenterX, gEnemyCenterY), new D2DSize(_shootRange, _shootRange)),
				D2DColor.FromGDIColor(Color.FromArgb(20, 255, 0, 0)));
#endif
		}

		public override void Update()
		{
			if (_shootingCooldown > 0)
				_shootingCooldown--;
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
			CustomEntities.Add(new Bullet(Player, Position + Size / 2, path, Game, CustomEntities));
			_shootingCooldown = _shootingCooldownMax;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Shooter : Enemy, IEntitySpawner
	{
		public List<IEntity> CustomEntities { get; }
		
		private readonly float _shootRange;
		private int _shootingCooldown;
		private readonly int _shootingCooldownMax;
		
		public Shooter(Vector2 startPos, Game level, float shootRange, int shootingCooldown,
			List<IEntity> customEntities) : base(startPos, level)
		{
			_shootRange = shootRange;
			_shootingCooldown = shootingCooldown;
			_shootingCooldownMax = shootingCooldown;
			CustomEntities = customEntities;
		}
		
		public override void Draw(D2DGraphics g, float width, float height)
		{
			var gEnemyPosX = MathF.Ceiling(width / 2) + _position.X - MathF.Ceiling(Level.PlayerSize.X / 2) - Player.Position.X;
			var gEnemyPosY = MathF.Ceiling(height / 2) + _position.Y - MathF.Ceiling(Level.PlayerSize.Y / 2) - Player.Position.Y;
			g.FillRectangle(gEnemyPosX, gEnemyPosY, Size.X, Size.Y, D2DColor.Blue);
#if DEBUG
			var gEnemyCenterX = gEnemyPosX + Size.X / 2;
			var gEnemyCenterY = gEnemyPosY + Size.Y / 2;
			g.DrawLine(gEnemyCenterX, gEnemyCenterY, (width - Size.X) / 2 + Level.PlayerSize.X / 2,
				(height - Size.Y) / 2 + Level.PlayerSize.Y / 2, PlayerInVision ? D2DColor.LightBlue : D2DColor.Blue);
			if (LastPath.HasValue)
				g.DrawLine(gEnemyCenterX, gEnemyCenterY, gEnemyCenterX + LastPath.Value.X, gEnemyCenterY + LastPath.Value.Y, D2DColor.Orange);
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
			if(_shootingCooldown > 0)
				_shootingCooldown--;
		}
		
		public override void MakeMove()
		{
			var (pathExist, path) = CheckPath();
			PlayerInVision = pathExist;
			var distance = (Player.Position - _position).Length();
			if (pathExist && distance <= VisionDistance)
			{
				if (distance <= _shootRange) Shoot(path);
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

		private void Shoot(Vector2 path)
		{
			if (_shootingCooldown != 0) return;
			CustomEntities.Add(new Bullet(Player, Position + Size / 2, path, _map, CustomEntities));
			_shootingCooldown = _shootingCooldownMax;
		}
	}
}
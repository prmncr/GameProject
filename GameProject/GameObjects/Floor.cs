using System;
using System.Numerics;
using GameProject.Levels;
using GameProject.Properties;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Floor : IBuilding
	{
		private D2DBitmap _spriteCache;
		private bool _spriteCreated;

		public Floor(Vector2 cellPos)
		{
			Position = cellPos * LevelController.Level.BlockScale;
		}

		public Vector2 Position { get; }

		public void Redraw(D2DGraphics g, D2DDevice device, float width, float height)
		{
			if (!_spriteCreated)
			{
				_spriteCache = device.CreateBitmapFromGDIBitmap(Resources.Floor);
				_spriteCreated = true;
			}

			g.DrawBitmap(_spriteCache,
				new D2DRect(
					MathF.Ceiling(width / 2) + Position.X - MathF.Ceiling(LevelController.Player.Size.X / 2) -
					MathF.Ceiling(LevelController.Player.Position.X),
					MathF.Ceiling(height / 2) + Position.Y - MathF.Ceiling(LevelController.Player.Size.Y / 2) -
					MathF.Ceiling(LevelController.Player.Position.Y), LevelController.Level.BlockScale,
					LevelController.Level.BlockScale));
		}
	}
}
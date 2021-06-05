using System;
using System.Drawing;
using System.Numerics;
using GameProject.Levels;
using GameProject.Properties;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class ExitDoor : IBuilding
	{
		private D2DBitmap _spriteClosedCache;
		private bool _spriteCreated;
		private D2DBitmap _spriteOpenedCache;
		public bool IsOpen = false;

		public ExitDoor(Vector2 cellPos)
		{
			Position = cellPos * LevelController.Level.BlockScale;
		}

		public Vector2 Position { get; }

		public void Redraw(D2DGraphics g, D2DDevice device, float width, float height)
		{
			if (!_spriteCreated)
			{
				_spriteOpenedCache = device.CreateBitmapFromGDIBitmap(Resources.DoorOpened);
				_spriteClosedCache = device.CreateBitmapFromGDIBitmap(Resources.DoorClosed);
				_spriteCreated = true;
			}

			g.DrawBitmap(IsOpen ? _spriteOpenedCache : _spriteClosedCache,
				new D2DRect(
					MathF.Ceiling(width / 2) + Position.X - MathF.Ceiling(LevelController.Player.Size.X / 2) -
					MathF.Ceiling(LevelController.Player.Position.X),
					MathF.Ceiling(height / 2) + Position.Y - MathF.Ceiling(LevelController.Player.Size.Y / 2) -
					MathF.Ceiling(LevelController.Player.Position.Y), LevelController.Level.BlockScale,
					LevelController.Level.BlockScale));
		}

		public static implicit operator RectangleF(ExitDoor exit)
		{
			return new(exit.Position.X, exit.Position.Y, LevelController.Level.BlockScale,
				LevelController.Level.BlockScale);
		}
	}
}
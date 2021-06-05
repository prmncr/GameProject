using System;
using System.Drawing;
using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class ExitDoor : IBuilding
	{
		public ExitDoor(Vector2 cellPos)
		{
			Position = cellPos * LevelController.Level.BlockScale;
		}

		public Vector2 Position { get; }
		public bool IsOpen = false;

		public void Redraw(D2DGraphics g, float width, float height)
		{
			g.FillRectangle(
				MathF.Ceiling(width / 2) + Position.X - MathF.Ceiling(LevelController.Player.Size.X / 2) -
				MathF.Ceiling(LevelController.Player.Position.X),
				MathF.Ceiling(height / 2) + Position.Y - MathF.Ceiling(LevelController.Player.Size.Y / 2) -
				MathF.Ceiling(LevelController.Player.Position.Y), LevelController.Level.BlockScale,
				LevelController.Level.BlockScale, IsOpen ?
				D2DColor.Yellow : D2DColor.Orange);
		}

		public static implicit operator RectangleF(ExitDoor exit)
		{
			return new(exit.Position.X, exit.Position.Y, LevelController.Level.BlockScale,
				LevelController.Level.BlockScale);
		}
	}
}
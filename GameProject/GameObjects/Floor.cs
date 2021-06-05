using System;
using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Floor : IBuilding
	{
		public Floor(Vector2 cellPos)
		{
			Position = cellPos * LevelController.Level.BlockScale;
		}

		public Vector2 Position { get; }

		public void Redraw(D2DGraphics g, float width, float height)
		{
			g.FillRectangle(
				MathF.Ceiling(width / 2) + Position.X - MathF.Ceiling(LevelController.Player.Size.X / 2) -
				MathF.Ceiling(LevelController.Player.Position.X),
				MathF.Ceiling(height / 2) + Position.Y - MathF.Ceiling(LevelController.Player.Size.Y / 2) -
				MathF.Ceiling(LevelController.Player.Position.Y), LevelController.Level.BlockScale, LevelController.Level.BlockScale,
				D2DColor.Gray);
		}
	}
}
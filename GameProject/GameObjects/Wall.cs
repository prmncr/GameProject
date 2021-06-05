using System;
using System.Numerics;
using GameProject.Levels;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Wall : IBuilding
	{
		public Wall(Vector2 cellPos, float scaling)
		{
			Position = cellPos * scaling;
			Scaling = scaling;
		}

		public float Scaling { get; }

		public Vector2 Position { get; }

		public void Redraw(D2DGraphics g, float width, float height)
		{
			g.FillRectangle(
				MathF.Ceiling(width / 2) + Position.X - MathF.Ceiling(LevelInfo.Player.Size.X / 2) -
				MathF.Ceiling(LevelInfo.Player.Position.X),
				MathF.Ceiling(height / 2) + Position.Y - MathF.Ceiling(LevelInfo.Player.Size.Y / 2) -
				MathF.Ceiling(LevelInfo.Player.Position.Y), Scaling, Scaling, D2DColor.DarkRed);
		}
	}
}
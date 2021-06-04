﻿using System;
using System.Numerics;
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

		public Vector2 Position { get; }
		public float Scaling { get; }

		public void Draw(D2DGraphics g, Vector2 playerPos, float width, float height, Vector2 playerSize)
		{
			g.FillRectangle(
				MathF.Ceiling(width / 2) + Position.X - MathF.Ceiling(playerSize.X / 2) - MathF.Ceiling(playerPos.X),
				MathF.Ceiling(height / 2) + Position.Y - MathF.Ceiling(playerSize.Y / 2) - MathF.Ceiling(playerPos.Y),
				Scaling,
				Scaling,
				D2DColor.DarkRed);
		}
	}
}
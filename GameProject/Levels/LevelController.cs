using System;
using System.Collections.Generic;
using GameProject.GameObjects;
using unvell.D2DLib;

namespace GameProject.Levels
{
	public static class LevelController
	{
		public static Level Level { get; private set; }
		public static Player Player => Level.Player;

		public static List<Entity> SummonedEntities { get; private set; } = new();
		public static List<Enemy> Enemies { get; private set; } = new();

		public static void Restart(Type level)
		{
			SummonedEntities = new List<Entity>();
			Enemies = new List<Enemy>();
			Level = Activator.CreateInstance(level) as Level;
			Level!.Initialize();
		}

		public static void Redraw(D2DGraphics g, D2DDevice device, int width, int height)
		{
			foreach (var buildings in Level.BuiltMap)
			foreach (var building in buildings)
				building?.Redraw(g, device, width, height);
		}
	}
}
﻿namespace GameProject.Levels
{
	public class Level1 : ILevel
	{
		public int[][] Map => new []
		{
			new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 2, 0, 1, 1, 1, 1, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
			new[] {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 3, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1},
			new[] {1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1},
			new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
		};
	}
}
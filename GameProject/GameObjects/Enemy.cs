using System;
using System.Numerics;
using GameProject.GameControl;
using unvell.D2DLib;

namespace GameProject.GameObjects
{
	public class Enemy : IEntity
	{
		public float Speed { get; set; } = 5;
		public Vector2 Position { get; set; }
		public Vector2 Size { get; set; }

		private GameMap _map;
		private float Top => Position.Y;
		private float Bottom => Position.Y + Size.Y;
		private float Left => Position.X;
		private float Right => Position.X + Size.X;
		private float _scaling;
		private Player _player;

		public Enemy(float scaling, Vector2 size, Vector2 startPos, GameMap map, Player player)
		{
			Size = size;
			_scaling = scaling;
			Position = startPos;
			_map = map;
			_player = player;
		}
		
		public void Draw(D2DGraphics g, float width, float height, Vector2 offset = default, Vector2 playerSize = default)
		{
			g.FillRectangle(MathF.Ceiling(width / 2) + Position.X - MathF.Ceiling(playerSize.X / 2) - offset.X,
				MathF.Ceiling(height / 2) + Position.Y - MathF.Ceiling(playerSize.Y / 2) - offset.Y, _scaling,
				_scaling, D2DColor.Black);
		}
	}
}
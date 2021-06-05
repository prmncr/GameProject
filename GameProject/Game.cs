using System;
using System.Linq;
using System.Windows.Forms;
using GameProject.Levels;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace GameProject
{
	public class Game : D2DControl
	{
		private bool _left, _right, _up, _down;

		public Game(Type level)
		{
			LevelInfo.Restart(level);
		}

		#region view

		protected override void OnRender(D2DGraphics g)
		{
			//updates
			LevelInfo.Player.Update();
			UpdateMap();
			foreach (var enemy in LevelInfo.Enemies)
				enemy.Update();
			foreach (var entity in LevelInfo.SummonedEntities.ToList())
				entity.Update();

			//map redrawing
			LevelInfo.Redraw(g, Width, Height);

			//enemies redrawing
			foreach (var enemy in LevelInfo.Enemies)
				enemy.Draw(g, Width, Height);

			//custom entities redrawing
			foreach (var entity in LevelInfo.SummonedEntities)
				entity.Draw(g, Width, Height);

			//player redrawing
			LevelInfo.Player.Draw(g, Width, Height);

			//health redrawing
			g.FillRectangle(Width - 300, 0, 3 * LevelInfo.Player.Health, 30, D2DColor.Red);

			Invalidate();
		}

		#endregion

		#region controller

		protected override void OnKeyDown(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.W:
					_up = true;
					break;
				case Keys.A:
					_left = true;
					break;
				case Keys.S:
					_down = true;
					break;
				case Keys.D:
					_right = true;
					break;
			}
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.W:
					_up = false;
					break;
				case Keys.A:
					_left = false;
					break;
				case Keys.S:
					_down = false;
					break;
				case Keys.D:
					_right = false;
					break;
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			LevelInfo.Player.Shoot(PointToClient(e.Location));
		}

		#endregion

		#region model

		private void UpdateMap()
		{
			LevelInfo.Player.Move(_left, _right, _up, _down);
			MoveEnemies();
			DamagePlayer();
		}

		private void DamagePlayer()
		{
			foreach (var enemy in LevelInfo.Enemies) enemy.DamagePlayer();
		}

		private void MoveEnemies()
		{
			foreach (var enemy in LevelInfo.Enemies) enemy.MakeMove();
		}

		#endregion
	}
}
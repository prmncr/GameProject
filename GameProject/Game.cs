using System;
using System.Drawing;
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
			BackColor = Color.Black;
			LevelController.Restart(level);
		}

		protected override void OnRender(D2DGraphics g)
		{
			#region math model updates

			// player step
			LevelController.Player.Move(_left, _right, _up, _down);
			LevelController.Player.UpdateCounters();
			if (Math.AreIntersected(new RectangleF(LevelController.Player.Position.X, LevelController.Player.Position.Y,
					LevelController.Player.Size.X, LevelController.Player.Size.Y),
				LevelController.Level.Exit) && LevelController.Level.Exit.IsOpen)
				MainWindow.GetInstance().ChangePage(Page.Selector);

			// enemies step
			foreach (var enemy in LevelController.Enemies)
			{
				enemy.MakeMove();
				enemy.UpdateCounters();
				enemy.DamagePlayer();
			}

			// summmons step
			foreach (var entity in LevelController.SummonedEntities.ToList())
				entity.UpdateCounters();

			if (!LevelController.Enemies.Any())
				LevelController.Level.Exit.IsOpen = true;

			#endregion

			#region view updates

			LevelController.Redraw(g, Device, Width, Height);

			foreach (var enemy in LevelController.Enemies)
				enemy.Redraw(g, Device, Width, Height);

			foreach (var entity in LevelController.SummonedEntities)
				entity.Redraw(g, Device, Width, Height);

			LevelController.Player.Redraw(g, Device, Width, Height);
			g.FillRectangle(Width - 300, 0, 3 * LevelController.Player.Health, 30, D2DColor.Red);

			Invalidate();

			#endregion
		}

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
				case Keys.Escape:
					MainWindow.GetInstance().ChangePage(Page.Selector);
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
			LevelController.Player.Shoot(PointToClient(e.Location));
		}
	}
}
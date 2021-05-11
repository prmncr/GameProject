﻿using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.GameControl;

namespace GameProject.MainMenu
{
	public class MainMenuView : View
	{
		public MainMenuView()
		{
			RenderMenu();
		}

		private async void RenderMenu()
		{
			await Task.Run(() => Thread.Sleep(5));
			BackColor = Color.Gray;
			Button btn = new()
			{
				Location = new Point((Width - 100) / 2, (Height - 50) / 2),
				Size = new Size(100, 50), 
				Text = "Play"
			};
			btn.Click += (_, _) => WindowController.Model.ChangePage(Page.Selector);
			Controls.Add(btn);
		}
	}
}
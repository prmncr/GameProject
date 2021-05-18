using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Levels;
using unvell.D2DLib.WinForm;

namespace GameProject
{
	public class LevelSelector : D2DControl
	{
		public LevelSelector()
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
				Text = "Level 1"
			};
			btn.Click += (_, _) => MainWindow.GetInstance().ChangePage(Page.Game, new Level1());
			Controls.Add(btn);
		}
	}
}
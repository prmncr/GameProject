using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Properties;

namespace GameProject
{
	public class GameEnd : Control
	{
		public GameEnd()
		{
			RenderMenu();
		}

		private async void RenderMenu()
		{
			await Task.Run(() => Thread.Sleep(5));
			BackgroundImage = Resources.YouDied;

			Button btn = new()
			{
				Size = new Size(200, 50),
				Text = "Menu"
			};
			btn.Click += (_, _) => MainWindow.GetInstance().ChangePage(Page.Selector);
			Controls.Add(btn);
		}
	}
}
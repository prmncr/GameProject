using System.Drawing;
using System.Windows.Forms;
using GameProject.GameControl;

namespace GameProject.MainMenu
{
	public class MainMenuView : View
	{
		public MainMenuView()
		{
			BackColor = Color.Blue;
			Button btn = new() {Location = new Point(100, 100), Text = "to red"};
			btn.Click += (_, _) => WindowController.Model.ChangePage(Page.Game);
			Controls.Add(btn);
		}
	}
}
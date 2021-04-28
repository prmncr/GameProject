using System.Windows.Forms;
using GameProject.MainMenu;
using unvell.D2DLib.WinForm;
using System.Reflection;

namespace GameProject
{
	public partial class WindowView : Form
	{
		public WindowView()
		{
			InitializeComponent();
			Controls.Add(new MainMenuView {Dock = DockStyle.Fill});
		}

		public void ChangePage(Controller pageController)
		{
			Controls.Clear();
			var view = pageController?.ViewAbstract;
			if (view == null) return;
			view.Dock = DockStyle.Fill;
			Controls.Add(view);
		}
	}
}
using System;
using System.Windows.Forms;
using unvell.D2DLib.WinForm;

namespace GameProject
{
	public partial class MainWindow : D2DForm
	{
		private static MainWindow _instance;

		public MainWindow()
		{
			InitializeComponent();
			_instance = this;
			Controls.Add(new MainMenu {Dock = DockStyle.Fill});
		}

		public static MainWindow GetInstance()
		{
			return _instance ?? new MainWindow();
		}

		public void ChangePage(Page page, Type level = null)
		{
			var view = page.GetInstance(level);
			if (view == null) return;
			Controls.Clear();
			view.Dock = DockStyle.Fill;
			Controls.Add(view);
			view.Focus();
		}
	}
}
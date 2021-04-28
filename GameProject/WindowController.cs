using System;
using System.Windows.Forms;
using System.Reflection;
using unvell.D2DLib.WinForm;

namespace GameProject
{
	public static class WindowController
	{
		public static WindowView View { get; private set; }

		public static WindowModel Model { get; private set; }

		public static void Start()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			View = new WindowView();
			Model = new WindowModel(View);
			Model.PageChanged += controller => View.ChangePage(controller);
			Application.Run(View);
		}
	}
}
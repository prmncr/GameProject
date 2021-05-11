using System;
using GameProject.Levels;
using unvell.D2DLib.WinForm;

namespace GameProject
{
	public class WindowModel
	{
		private WindowView _view;
		private Controller _currentPageControl;

		public event Action<Controller> PageChanged;

		public WindowModel(WindowView view)
		{
			_view = view;
		}

		public void ChangePage(Page controller, ILevel level = null)
		{
			PageChanged?.Invoke(controller.GetInstance(level));
		}
	}
}
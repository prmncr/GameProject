using System.Drawing;
using System.Linq;
using System.Reflection;
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
			var group = new FlowLayoutPanel
			{
				Width = 250, Height = 1000, Left = Width / 2 - 250 / 2, Top = Height / 2 - 400 / 2,
				FlowDirection = FlowDirection.TopDown
			};
			var types = Assembly
				.GetExecutingAssembly()
				.GetTypes();
			var levelTypes = types.Where(t => t.CustomAttributes.Any(x => x.AttributeType == typeof(LevelAttribute)));

			foreach (var levelType in levelTypes)
			{
				var name =
					levelType.GetProperty("Name", BindingFlags.NonPublic | BindingFlags.Static)
						?.GetValue(null) as string;
				Button btn = new()
				{
					Size = new Size(200, 50),
					Text = name
				};
				btn.Click += (_, _) => MainWindow.GetInstance().ChangePage(Page.Game, levelType);
				group.Controls.Add(btn);
			}

			Controls.Add(group);
			var nameText = new Label
			{
				Width = 200,
				Height = 100,
				Text = "DrobashGame",
				Anchor = AnchorStyles.Top,
				ForeColor = Color.White,
				TextAlign = ContentAlignment.MiddleCenter,
				Font = new Font("Consolas", 20)
			};
			nameText.Left = Width / 2 - nameText.Width / 2;
			Controls.Add(nameText);
			var text = new Label
			{
				Width = 1000,
				Height = 200,
				Text = "W, A, S, D - move\nLMB - shoot\nESC - exit\nGoal: overcome all enemies and find escape.",
				TextAlign = ContentAlignment.MiddleCenter,
				Anchor = AnchorStyles.Top,
				Top = 150,
				ForeColor = Color.White,
				Font = new Font("Arial", 16)
			};
			text.Left = Width / 2 - text.Width / 2;
			Controls.Add(text);
			Button quit = new()
			{
				Size = new Size(200, 50),
				Text = "Quit"
			};
			quit.Click += (_, _) => Application.Exit();
			group.Controls.Add(quit);
		}
	}
}
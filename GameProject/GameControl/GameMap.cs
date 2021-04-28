using GameProject.GameObjects;

namespace GameProject.GameControl
{
	public class GameMap
	{
		public Player Player { get; }

		public GameMap(Player player)
		{
			Player = player;
		}
	}
}
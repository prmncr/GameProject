using System.Collections.Generic;

namespace GameProject.GameObjects
{
	public interface IEntitySpawner
	{
		List<Entity> CustomEntities { get; }
	}
}